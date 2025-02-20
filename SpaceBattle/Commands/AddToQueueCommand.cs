using SpaceBattle.Interfaces;

namespace SpaceBattle.Commands
{
    public class AddToQueueCommand : ICommand
    {
        private readonly IRunner _runner;
        private readonly ICommand _command;

        public AddToQueueCommand(ICommand command, IRunner r)
        {
            _runner = r;
            _command = command;
        }

        public void Execute()
        {
            _runner.Queue.Add(_command);
        }
    }
}
