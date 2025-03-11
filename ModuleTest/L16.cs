using Moq;
using SpaceBattle;
using SpaceBattle.Commands;
using SpaceBattle.Interfaces;
using SpaceBattle.IoC;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics;
using System.Numerics;

namespace ModuleTest
{
    [TestClass]
    public class L16
    {
        [TestInitialize]
        public void TestInitialize()
        {
            new InitScopesCommand().Execute();
        }

        [TestMethod]
        public void Test1()
        {
            var mock = new Mock<ServerThreadStartCommand>();
            var cmd = mock.Object;

            cmd.Execute();
        }

        [TestMethod]
        public void Test2()
        {
            BlockingCollection<ICommand> q = new BlockingCollection<ICommand>(100);
            var st = new ServerThread(q);

            var cmd = new Mock<ICommand>();

            q.Add(cmd.Object);
            q.Add(cmd.Object);
            q.Add(new HardStopCommand(st));
            q.Add(cmd.Object);
            q.Add(cmd.Object);

            var result = false;

            st.SetActionAfterStop(() => {                
                result = true;
            });
            st.Start();
            st.Join();


            Assert.AreEqual(result, true);
        }

        [TestMethod]
        public void Test3()
        {
            BlockingCollection<ICommand> q = new BlockingCollection<ICommand>(100);
            var st = new ServerThread(q);

            var cmd = new Mock<ICommand>();

            q.Add(cmd.Object);
            q.Add(cmd.Object);
            q.Add(new SoftStopCommand(st));
            q.Add(cmd.Object);
            q.Add(cmd.Object);

            st.Start();
            st.Join();

            Assert.AreEqual(st._q.Count, 0);
        }
    }
}