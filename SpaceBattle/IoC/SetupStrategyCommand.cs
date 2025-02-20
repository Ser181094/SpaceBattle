using SpaceBattle.Interfaces;

namespace SpaceBattle.IoC
{
    public class SetupStrategyCommand : ICommand
    {
        private readonly Func<string, object[], object> newStrategy;

        public SetupStrategyCommand(Func<string, object[], object> newStrategy) => this.newStrategy = newStrategy;
        public void Execute()
        {
            IoC.Strategy = this.newStrategy;
        }
    }
}
