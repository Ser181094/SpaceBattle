using SpaceBattle.Interfaces;

namespace SpaceBattle.Commands
{
    public class WrapperCommand : ICommand
    {
        Action _action;
        public WrapperCommand(Action action) => _action = action;
        public void Execute() => _action();
    }
}
