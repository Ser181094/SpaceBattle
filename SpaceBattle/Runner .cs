using System.Collections.Concurrent;
using SpaceBattle.Interfaces;

namespace SpaceBattle
{
    public class Runner : IRunner
    {
        public BlockingCollection<ICommand> Queue { get; } = new BlockingCollection<ICommand>(100);
        public void Run()
        {
            while (Queue.Count != 0)
            {
                var command = Queue.Take();
                try
                {
                    command.Execute();
                }
                catch (Exception e)
                {
                    ExceptionHandler.Handle(command, e).Execute();
                }
            }
        }
    }

}
