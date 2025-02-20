using SpaceBattle.Interfaces;

namespace SpaceBattle.Commands
{
    public class RetryCommand : ICommand
    {
        private readonly ICommand _command;

        public RetryCommand(ICommand command)
        {
            _command = command;
        }
        public void Execute()
        {
            _command.Execute();
        }
    }
}
