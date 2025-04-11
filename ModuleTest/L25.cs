using SpaceBattle;
using SpaceBattle.Commands;
using SpaceBattle.Interfaces;
using SpaceBattle.IoC;
using System.Collections.Concurrent;
using static SpaceBattle.CommandState;

namespace ModuleTest
{
    [TestClass]
    public class L25
    {
        [TestInitialize]
        public void TestInitialize()
        {
            new InitScopesCommand().Execute();
        }

        [TestMethod]
        public void Test1()
        {
            BlockingCollection<ICommand> q = new BlockingCollection<ICommand>(100);
            var st = new ServerThread(q);

            q.Add(new EmptyCommand());
            q.Add(new EmptyCommand());
            q.Add(new HardStopCommand(st));
            q.Add(new EmptyCommand());
            q.Add(new EmptyCommand());

            st.Start();
            st.Join();

            Assert.AreEqual(st._q.Count, 2);
        }

        [TestMethod]
        public void Test2()
        {
            BlockingCollection<ICommand> q = new BlockingCollection<ICommand>(100);
            var st = new ServerThread(q);

            q.Add(new EmptyCommand());
            q.Add(new EmptyCommand());
            q.Add(new MoveToCommand());

            st.Start();
            var task = Task.Run(() => st.Join());
            task.Wait(new TimeSpan(0, 0, 5));

            Assert.IsTrue(st.commandState.currentState.GetType() == typeof(MoveToState));
        }


        [TestMethod]
        public void Test3()
        {
            BlockingCollection<ICommand> q = new BlockingCollection<ICommand>(100);
            var st = new ServerThread(q);

            q.Add(new EmptyCommand());
            q.Add(new EmptyCommand());
            q.Add(new MoveToCommand());
            q.Add(new RunCommand());

            st.Start();
            var task = Task.Run(() => st.Join());
            task.Wait(new TimeSpan(0, 0, 15));

            Assert.IsTrue(st.commandState.currentState.GetType() == typeof(DefaultState));
        }
    }
}
