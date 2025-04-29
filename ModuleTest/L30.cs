using SpaceBattle;
using SpaceBattle.Commands;
using SpaceBattle.Interfaces;
using SpaceBattle.IoC;
using System.Numerics;

namespace ModuleTest
{
    [TestClass]
    public class L30
    {
        [TestInitialize]
        public void Initialize()
        {
            new InitScopesCommand().Execute();

            var objectImpl = new ObjectImpl();
            objectImpl.SetProperty("id", 548);
            objectImpl.SetProperty("velocity", 0);
            objectImpl.SetProperty("angle", (long)15);
            objectImpl.SetProperty("location", new Vector2(0, 0));

            //var IoCScope = IoC.Resolve<object>("IoC.Scope.Create");
            //IoC.Resolve<ICommand>("IoC.Scope.Current.Set", IoCScope).Execute();
            IoC.Resolve<ICommand>("IoC.Register", "CreateCommand", (object[] args) => CommandFactory.Invoke(args)).Execute();
            IoC.Resolve<ICommand>("IoC.Register", "Command.StartMove", (object[] args) => new StartMoveCommand(objectImpl, (string)args[0], args[1])).Execute();
            IoC.Resolve<ICommand>("IoC.Register", "IUObject.SetProperty", (object[] args) => {
                return new WrapperCommand(() => ((IUObject)args[0]).SetProperty((string)args[1], args[2]));
            }).Execute();
            IoC.Resolve<ICommand>("IoC.Register", "Adapter", (object[] args) =>
            {
                Type intType = (Type)args[0];
                var proxyType = AdapterGenerator.CreateAdapter(intType);
                return Activator.CreateInstance(proxyType, args[1]);
            }).Execute();
            IoC.Resolve<ICommand>("IoC.Register", "IMovingObject.SetLocation", (object[] args) => {
                return new WrapperCommand(() => ((IUObject)args[0]).SetProperty("location", args[1]));
            }).Execute();
            IoC.Resolve<ICommand>("IoC.Register", "Command.GetGameObject", (object[] args) => {
                return objectImpl;
            }).Execute();
            IoC.Resolve<ICommand>("IoC.Register", "IMovingObject.GetLocation", (object[] args) => {
                return ((IUObject)args[0]).GetProperty("location");
            }).Execute();
            IoC.Resolve<ICommand>("IoC.Register", "IMovingObject.GetVelocity", (object[] args) => {
                var velocity = (long)((IUObject)args[0]).GetProperty("velocity");
                var angle = (long)((IUObject)args[0]).GetProperty("angle");
                var x = velocity * Math.Cos(angle);
                var y = velocity * Math.Sin(angle);
                return (object)new Vector2((float)x, (float)y);
            }).Execute();
        }

        [TestMethod]
        public void Test1()
        {
            var testdata = new
            {
                message_json = "{\"id\": \"548\",\"action\": \"StartMove\",\"properties\": {\"velocity\": 2 }}"
            };

            // Arrange
            var message = Newtonsoft.Json.JsonConvert.DeserializeObject<Message>(testdata.message_json);
            var uobject = IoC.Resolve<IUObject>("Command.GetGameObject");

            var startPosition = uobject.GetProperty("location");
            var startVelocity = uobject.GetProperty("velocity");

            // Act
            var command = new InterpretationCommand(message);
            command.Execute();

            var endPosition = uobject.GetProperty("location");
            var endVelocity = uobject.GetProperty("velocity");

            // Assert
            Assert.AreNotEqual(startPosition, endPosition);
            Assert.AreNotEqual(startVelocity, endVelocity);
            Assert.AreEqual(endVelocity, (long)2);
        }

        [TestMethod]
        public void Test2()
        {
            var testdata = new
            {
                message_json = "{\"id\": \"548\",\"action\": \"StopMove\",\"properties\": {\"velocity\": 2 }}"
            };

            // Arrange
            var message = Newtonsoft.Json.JsonConvert.DeserializeObject<Message>(testdata.message_json);

            // Act
            var command = new InterpretationCommand(message);

            // Assert
            Assert.ThrowsException<Exception>(command.Execute);
        }

        [TestMethod]
        public void Test3()
        {
            var testdata = new
            {
                message_json = "{\"id\": \"549\",\"action\": \"StartMove\",\"properties\": {\"velocity\": 2 }}"
            };

            // Arrange
            var message = Newtonsoft.Json.JsonConvert.DeserializeObject<Message>(testdata.message_json);

            // Act
            var command = new InterpretationCommand(message);

            // Assert
            Assert.ThrowsException<Exception>(command.Execute);
        }
    }
}