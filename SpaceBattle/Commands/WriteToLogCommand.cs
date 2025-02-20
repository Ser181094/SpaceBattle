using NLog;
using SpaceBattle.Interfaces;

namespace SpaceBattle.Commands
{
    public class WriteToLogCommand : ICommand
    {
        private readonly Exception _exception;
        private readonly ICommand _command;

        private readonly Logger _logger = LogManager.GetCurrentClassLogger();

        public WriteToLogCommand(ICommand command, Exception exception)
        {
            _exception = exception;
            _command = command;
        }

        public void Execute()
        {
            _logger.Error(_command.ToString(), _exception.Message);
        }
    }
}
