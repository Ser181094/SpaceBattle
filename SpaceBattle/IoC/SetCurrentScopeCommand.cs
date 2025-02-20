using SpaceBattle.Interfaces;

namespace SpaceBattle.IoC
{
    public class SetCurrentScopeCommand : ICommand
    {
        private readonly Scope scope;

        public SetCurrentScopeCommand(Scope scope)
        {
            this.scope = scope;
        }

        public void Execute()
        {
            ScopeBaseDependencyStrategy.CurrentScope.Value = scope;
        }
    }
}
