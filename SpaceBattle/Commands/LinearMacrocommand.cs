using SpaceBattle.Interfaces;

namespace SpaceBattle.Commands
{
    public class LinearMacrocommand : ICommand
    {
        private Queue<ICommand> _commands;

        public LinearMacrocommand(params ICommand[] commands)
        {
            _commands = new Queue<ICommand>(commands);
        }
        public void Execute()
        {
            while (_commands.Count > 0)
            {
                ICommand command = _commands.Dequeue();
                command.Execute();
            }
        }
    }
}
