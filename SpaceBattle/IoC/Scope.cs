namespace SpaceBattle.IoC
{
    public class Scope
    {
        internal IDictionary<string, Func<object[], object>> Dependencies { get; set; }

        internal Scope? Parent { get; set; }

        public Scope(IDictionary<string, Func<object[], object>> dependencies, Scope? parent)
        {
            Dependencies = dependencies;
            Parent = parent;
        }

        public object Resolve(string key, object[] args)
        {
            if (Dependencies.TryGetValue(key, out Func<object[], object>? dependencyResolver))
            {
                return dependencyResolver!(args);
            }
            else
            {
                return Parent!.Resolve(key, args);
            }
        }
    }
}
