using SpaceBattle.Interfaces;

namespace SpaceBattle.Commands
{
    public class CommandWithException : ICommand
    {
        public void Execute()
        {
            throw new Exception();
        }
    }
}
