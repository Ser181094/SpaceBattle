using SpaceBattle.Interfaces;

namespace SpaceBattle.Commands
{
    public class ThirdTryCommand : ICommand
    {
        private readonly ICommand _command;

        public ThirdTryCommand(ICommand command)
        {
            _command = command;
        }
        public void Execute()
        {
            _command.Execute();
        }
    }
}
