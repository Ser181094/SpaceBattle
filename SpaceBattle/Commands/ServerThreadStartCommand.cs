using SpaceBattle.Interfaces;
using System.Collections.Concurrent;

namespace SpaceBattle.Commands
{
    public class ServerThreadStartCommand : ICommand
    {
        public void Execute() {
            BlockingCollection<ICommand> q = new BlockingCollection<ICommand>(100);
            var st = new ServerThread(q);
            st.Start();
        }
    }
}
