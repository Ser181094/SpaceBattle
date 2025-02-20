using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using SpaceBattle.Interfaces;
using System.Reflection;
using System.Runtime.Loader;

namespace SpaceBattle
{
    public class AdapterGenerator
    {

        public static Type CreateAdapter(Type type)
        {
            var prefix = type.Name.Remove(0, 1);
            var className = $"Auto{prefix}Adapter";

            var methods = type.GetMethods().Where(m => !m.Name.StartsWith("get_") && !m.Name.StartsWith("set_")).ToList();

            var classBody = GetClassBody(methods, type.Name);

            var adapterBody = String.Join(Environment.NewLine,
                            "using System;",
                            $"using {type.GetTypeInfo().Namespace};",
                            "using System.Numerics;",
                            "using SpaceBattle.IoC;",
                            "using SpaceBattle.Commands;",
                            "namespace SpaceBattle",
                            "{",
                                $"public class {className} : {type.Name}",
                                    "{",
                                        "IUObject _obj;",
                                        $"public {className}(IUObject obj)",
                                        "{",
                                            "_obj = obj;",
                                        "}",

                                        $"{classBody}",
                                "}",
                            "}");


            var compilation = CSharpCompilation.Create("a")
               .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
               .AddReferences(MetadataReference.CreateFromFile(typeof(object).GetTypeInfo().Assembly.Location))
               .AddReferences(MetadataReference.CreateFromFile(type.GetTypeInfo().Assembly.Location))
               .AddReferences(GetReferences())
               .AddSyntaxTrees(CSharpSyntaxTree.ParseText(adapterBody));

            using var assemblyLoadContext = new CollectibleAssemblyLoadContext();
            using var ms = new MemoryStream();

            var cr = compilation.Emit(ms);
            if (!cr.Success)
            {
                throw new InvalidOperationException("Error in expression: " + cr.Diagnostics.First(e =>
                    e.Severity == DiagnosticSeverity.Error).GetMessage());
            }

            ms.Seek(0, SeekOrigin.Begin);
            var assembly = assemblyLoadContext.LoadFromStream(ms);

            var outerClassType = assembly.GetType($"SpaceBattle.{className}");
            return outerClassType;
        }

        private static string GetClassBody(List<MethodInfo> methods, string typeName)
        {
            var classBody = string.Empty;            

            foreach (var method in methods)
            {
                var methodBody = string.Empty;
                var parametersBody = string.Empty;
                var parametersName = string.Empty;

                var parameters = method.GetParameters();
                foreach (var param in parameters)
                {
                    parametersName += ", " + param.Name;
                    parametersBody += ", " + param.ParameterType.Name + " " + param.Name;
                }

                parametersBody = parametersBody.TrimStart(',').TrimStart();

                string bodyBase = "IoC.IoC.Resolve<{0}>(\"" + typeName + ".{1}\", _obj {2});";
                var genericParameter = method.ReturnType.Name == "Void" ? "ICommand" : method.ReturnType.Name;
                methodBody = string.Format(bodyBase, genericParameter, method.Name, parametersName);


                if (genericParameter == "ICommand")
                {
                    methodBody = methodBody.Replace(";", ".Execute();");
                }
                else
                {
                    methodBody = $"return {methodBody}";
                }

                var returnTypeName = method.ReturnType.Name == "Void" ? "void" : method.ReturnType.Name;

                var body = String.Join(Environment.NewLine,
                   $"public {returnTypeName} {method.Name}({parametersBody})",
                   "{",
                       $"{methodBody}",
                   "}");

                classBody += Environment.NewLine + body;
            }
            return classBody;
        }

        private static List<MetadataReference> GetReferences()
        {
            string[] trustedAssembliesPaths = ((string)AppContext.GetData("TRUSTED_PLATFORM_ASSEMBLIES")).Split(Path.PathSeparator);

            var references = new List<MetadataReference>();

            foreach (var refAsm in trustedAssembliesPaths)
            {
                references.Add(MetadataReference.CreateFromFile(refAsm));
            }
            return references;
        }

        private class CollectibleAssemblyLoadContext : AssemblyLoadContext, IDisposable
        {
            public CollectibleAssemblyLoadContext() : base(true)
            { }

            protected override Assembly Load(AssemblyName assemblyName)
            {
                return null;
            }

            public void Dispose()
            {
                Unload();
            }
        }
    }
}
