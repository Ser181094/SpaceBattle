using SpaceBattle;
using SpaceBattle.Interfaces;
using System.Collections.Concurrent;


namespace SpaceBattle
{
    public class InetrpretCommand : ICommand
    {
        private readonly CommandMessage commandMessage;

        public InetrpretCommand(CommandMessage commandMessage)
        {
            this.commandMessage = commandMessage;
        }

        public void Execute()
        {
            var cmd = IoC.IoC.Resolve<ICommand>(commandMessage.OperationKey, commandMessage.args);

            var currentQueue = IoC.IoC.Resolve<ConcurrentQueue<ICommand>>("QueueCommand.Get");

            currentQueue.Enqueue(cmd);
        }
    }
}
