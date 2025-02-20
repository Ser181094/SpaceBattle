using Moq;
using SpaceBattle;
using SpaceBattle.Commands;
using SpaceBattle.Interfaces;
using SpaceBattle.IoC;
using System.ComponentModel;
using System.Numerics;

namespace ModuleTest
{
    [TestClass]
    public class L13
    {
        [TestInitialize]
        public void TestInitialize()
        {
            new InitScopesCommand().Execute();
        }

        [TestMethod]
        public void Test1()
        {
            var adapter = AdapterGenerator.CreateAdapter(typeof(IMovingObject));

            IoC.Resolve<ICommand>("IoC.Register", "IMovingObject.SetLocation", ((object[] args) => {
                return new SetupPropertyCommand((ObjectImpl)args[0], "Location", args[1]);
            })).Execute();
            IoC.Resolve<ICommand>("IoC.Register", "IMovingObject.GetLocation", ((object[] args) => {
                var res = ((ObjectImpl)args[0]).GetProperty("Location");               
                return res;
            })).Execute();
            IoC.Resolve<ICommand>("IoC.Register", "IMovingObject.GetVelocity", ((object[] args) => {  
                int d = 5;
                int n = 1;
                int v = 8;
                var res = new Vector2(
                    v * (float)Math.Cos(d / 360 * n),
                    v * (float)Math.Sin(d / 360 * n)
                );
                return (object)res;
            })).Execute();            

            var uObject = new Mock<ObjectImpl>();
            IoC.Resolve<ICommand>("IMovingObject.SetLocation", uObject.Object, new Vector2(12, 4)).Execute();

            var value = Activator.CreateInstance(adapter, uObject.Object);
            var command = new MoveCommand((IMovingObject)value);
            command.Execute();            

            var result = IoC.Resolve<Vector2>("IMovingObject.GetLocation", uObject.Object);
            var excepted = new Vector2(20, 4);

            Assert.AreEqual(result, excepted);
        }


        [TestMethod]
        public void Test2()
        {
            IoC.Resolve<ICommand>("IoC.Register", "Adapter", (object[] args) => {
                var generator = AdapterGenerator.CreateAdapter((Type)args[0]);
                return Activator.CreateInstance(generator, (IUObject)args[1]);
            }).Execute();

            IoC.Resolve<ICommand>("IoC.Register", "IMovingObject.SetLocation", ((object[] args) => {
                return new SetupPropertyCommand((ObjectImpl)args[0], "Location", args[1]);
            })).Execute();
            IoC.Resolve<ICommand>("IoC.Register", "IMovingObject.GetLocation", ((object[] args) => {
                var res = ((ObjectImpl)args[0]).GetProperty("Location");
                return res;
            })).Execute();
            IoC.Resolve<ICommand>("IoC.Register", "IMovingObject.GetVelocity", ((object[] args) => {
                int d = 5;
                int n = 1;
                int v = 8;
                var res = new Vector2(
                    v * (float)Math.Cos(d / 360 * n),
                    v * (float)Math.Sin(d / 360 * n)
                );
                return (object)res;
            })).Execute();

            var uObject = new Mock<ObjectImpl>();
            IoC.Resolve<ICommand>("IMovingObject.SetLocation", uObject.Object, new Vector2(12, 4)).Execute();

            var adapter = IoC.Resolve<IMovingObject>("Adapter", typeof(IMovingObject), uObject.Object);
            var command = new MoveCommand((IMovingObject)adapter);
            command.Execute();

            var result = IoC.Resolve<Vector2>("IMovingObject.GetLocation", uObject.Object);
            var excepted = new Vector2(20, 4);

            Assert.AreEqual(result, excepted);
        }

        [TestMethod]
        public void Test3()
        {
            IoC.Resolve<ICommand>("IoC.Register", "Adapter", (object[] args) => {
                var generator = AdapterGenerator.CreateAdapter((Type)args[0]);
                return Activator.CreateInstance(generator, (IUObject)args[1]);
            }).Execute();

            var mock = new Mock<IUObject>();
            IoC.Resolve<ICommand>("IoC.Register", "ITest.TestMethod", ((object[] args) => {
                return new EmptyCommand();
            })).Execute();

            var testAdapter = IoC.Resolve<ITest>("Adapter", typeof(ITest), mock.Object);
            testAdapter.TestMethod();
        }
    }
}