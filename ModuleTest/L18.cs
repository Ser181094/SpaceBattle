using Moq;
using SpaceBattle;
using SpaceBattle.Commands;
using SpaceBattle.Interfaces;
using SpaceBattle.IoC;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.Diagnostics;
using System.Numerics;
using SpaceBattle.GameAgent;
using System.Text.Json;
using System;

namespace ModuleTest
{
    [TestClass]
    public class L18
    {
        /*[TestInitialize]
        public void TestInitialize()
        {
            new InitScopesCommand().Execute();
        }

        [TestMethod]
        public void Test1()
        {
            var service = new RabbitMqService();

            Random rnd = new Random();

            var commandMessage = new CommandMessage()
            {
                GameKey = rnd.Next(1, 100).ToString(),
                GameObjKey = Guid.NewGuid(),
                OperationKey = "CommandWithException",
                args = []
            };

            var json = JsonSerializer.Serialize(commandMessage);

            service.SendMessage(json);
        }

        [TestMethod]
        public void Test2()
        {
            var service = new RabbitMqService();

            Random rnd = new Random();

            var commandMessage = new CommandMessage()
            {
                GameKey = rnd.Next(1, 100).ToString(),
                GameObjKey = Guid.NewGuid(),
                OperationKey = "EmptyCommand",
                args = []
            };

            var json = JsonSerializer.Serialize(commandMessage);

            service.SendMessage(json);
        }

        [TestMethod]
        public void Test3()
        {
            var service = new RabbitMqService();

            Random rnd = new Random();

            var commandMessage = new CommandMessage()
            {
                GameKey = rnd.Next(1, 100).ToString(),
                GameObjKey = Guid.NewGuid(),
                OperationKey = "MoveCommand",
                args = []
            };

            var json = JsonSerializer.Serialize(commandMessage);

            service.SendMessage(json);
        }*/
    }
}