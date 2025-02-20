using System.Collections.Concurrent;

namespace SpaceBattle.Interfaces;

public interface IRunner
{
    BlockingCollection<ICommand> Queue { get; }
    public void Run();
}
