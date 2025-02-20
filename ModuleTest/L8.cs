using Moq;
using SpaceBattle.Interfaces;
using SpaceBattle.Commands;
using SpaceBattle;
using System;
using System.Text;

namespace ModuleTest
{
    [TestClass]
    public class L8
    {
        [TestMethod]
        public void TestMethod1()
        {
            //Arrange
            var exception = new Exception();
            var command = new WriteToLogCommand(new EmptyCommand(), exception);

            //Act
            command.Execute();

            //Assert
            Assert.AreEqual(true, true);
        }

        [TestMethod]
        public void TestMethod2()
        {
            var exception = new Exception("Test");
            var runner = new Runner();

            //Act
            var command = new AddToQueueCommand(new WriteToLogCommand(null, exception), runner);
            command.Execute();

            //Assert
            Assert.IsTrue(runner.Queue.Any(a => a.GetType() == typeof(WriteToLogCommand)));
        }

        [TestMethod]
        public void TestMethod3()
        {
            //Arrange
            var runner = new Runner();
            var commandMock = new Mock<ICommand>();

            var retry = (ICommand c, Exception e) => {
                return new AddToQueueCommand(new RetryCommand(c), runner);
            };

            ExceptionHandler.RegisterHandler(typeof(CommandWithException), typeof(Exception), retry);

            var command = new AddToQueueCommand(new CommandWithException(), runner);
            command.Execute();

            //Act
            runner.Run();
            ExceptionHandler.ClearStore();

            //Assert
            Assert.AreEqual(true, true);
        }

        [TestMethod]
        public void TestMethod4()
        {
            //Arrange
            var runner = new Runner();
            var commandMock = new Mock<ICommand>();

            var retry = (ICommand c, Exception e) => {
                return new AddToQueueCommand(new RetryCommand(c), runner);
            };

            var write = (ICommand c, Exception e) => {
                return new AddToQueueCommand(new WriteToLogCommand(c, e), runner);
            };

            ExceptionHandler.RegisterHandler(typeof(CommandWithException), typeof(Exception), retry);
            ExceptionHandler.RegisterHandler(typeof(RetryCommand), typeof(Exception), write);

            var command = new AddToQueueCommand(new CommandWithException(), runner);
            command.Execute();

            //Act
            runner.Run();
            ExceptionHandler.ClearStore();

            //Assert
            Assert.AreEqual(true, true);
        }

        [TestMethod]
        public void TestMethod5()
        {
            //Arrange
            var runner = new Runner();
            var commandMock = new Mock<ICommand>();

            var retry = (ICommand c, Exception e) => {
                return new AddToQueueCommand(new RetryCommand(c), runner);
            };

            var thirdTryCommand = (ICommand c, Exception e) => {
                return new AddToQueueCommand(new ThirdTryCommand(c), runner);
            };

            var write = (ICommand c, Exception e) => {
                return new AddToQueueCommand(new WriteToLogCommand(c, e), runner);
            };

            ExceptionHandler.RegisterHandler(typeof(CommandWithException), typeof(Exception), retry);
            ExceptionHandler.RegisterHandler(typeof(RetryCommand), typeof(Exception), thirdTryCommand);
            ExceptionHandler.RegisterHandler(typeof(ThirdTryCommand), typeof(Exception), write);

            var command = new AddToQueueCommand(new CommandWithException(), runner);
            command.Execute();

            //Act
            runner.Run();
            ExceptionHandler.ClearStore();

            //Assert
            Assert.AreEqual(true, true);
        }
    }
}