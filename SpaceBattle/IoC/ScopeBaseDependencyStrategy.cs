namespace SpaceBattle.IoC
{
    public class ScopeBaseDependencyStrategy
    {
        internal static Scope? Root { get; set; }

        internal static Func<object> DefaultScope = () => Root;

        internal static ThreadLocal<Scope> CurrentScope { get; private set; } = new ThreadLocal<Scope>();

        public static object Resolve(string key, object[] args)
        {
            if (key == "Scopes.Root")
            {
                return Root!;
            }
            else
            {
                var scope = CurrentScope.Value;

                if (scope == null)
                {
                    scope = (Scope)DefaultScope();
                }

                return scope!.Resolve(key, args);
            }
        }

    }
}
