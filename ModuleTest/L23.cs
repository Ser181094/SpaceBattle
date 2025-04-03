using SpaceBattle;
using SpaceBattle.Auth;
using SpaceBattle.GameAgent;
using SpaceBattle.Interfaces;
using SpaceBattle.IoC;
using System.Text.Json;

namespace ModuleTest
{
    [TestClass]
    public class L23 
    {
        [TestInitialize]
        public void TestInitialize()
        {
            new InitScopesCommand().Execute();
        }

        [TestMethod]
        public void ValidateAuthenticationUserJwtTest1()
        {            
            var userId = 1;
            var userName = "FirstUser";
            var validationResult = true;

            var service = new AuthenticationService();

            var userAuthenticationJwt = service.GetUserAuthenticationJwt(userId, userName);

            var result = service.ValidateToken(userAuthenticationJwt);

            Assert.AreEqual(result, validationResult);
        }
        [TestMethod]
        public void ValidateAuthenticationUserJwtTest2()
        {
            var userId = 1;
            var userName = "SecondUser";
            var validationResult = false;

            var service = new AuthenticationService();

            var userAuthenticationJwt = service.GetUserAuthenticationJwt(userId, userName);

            var result = service.ValidateToken(userAuthenticationJwt);

            Assert.AreEqual(result, validationResult);
        }
       
        [TestMethod]
        public void CreateNewSpaceBattleGameForAuthenticatedUsersTest1()
        {
            var userId = 1;
            var userName = "FirstUser";
            var validationResult = true;

            var service = new AuthenticationService();

            var spaceBattleId = service.SpaceBattleRegister(
                service.GetUserAuthenticationJwt(userId, userName), new[] { 1, 3, 5 });

            Assert.AreEqual(spaceBattleId != 0, validationResult);
        }

        [TestMethod]
        public void CreateNewSpaceBattleGameForAuthenticatedUsersTest2()
        {
            var userId = 1;
            var userName = "SecondUser";
            var validationResult = false;

            var service = new AuthenticationService();

            var spaceBattleId = service.SpaceBattleRegister(
                service.GetUserAuthenticationJwt(userId, userName), new[] { 1, 3, 5 });

            Assert.AreEqual(spaceBattleId != 0, validationResult);
        }       

        [TestMethod]
        public void ValidateAuthenticationSpaceBattleGameJwtTest1()
        {
            var userId = 1;
            var userName = "FirstUser";
            var validationResult = true;

            var service = new AuthenticationService();

            var userAuthenticationToken = service.GetUserAuthenticationJwt(1, "FirstUser");

            var spaceBattleId = service.SpaceBattleRegister(userAuthenticationToken, new[] { 1, 3, 5 });

            userAuthenticationToken = service.GetUserAuthenticationJwt(userId, userName);

            var spaceBattleAuthenticationJwt =
                service.GetStarBattleAuthorizationJwt(spaceBattleId, userAuthenticationToken);

            var result = service.ValidateToken(spaceBattleAuthenticationJwt);

            Assert.AreEqual(result, validationResult);
        }

        [TestMethod]
        public void ValidateAuthenticationSpaceBattleGameJwtTest2()
        {
            var userId = 1;
            var userName = "SecondUser";
            var validationResult = false;

            var service = new AuthenticationService();

            var userAuthenticationToken = service.GetUserAuthenticationJwt(1, "FirstUser");

            var spaceBattleId = service.SpaceBattleRegister(userAuthenticationToken, new[] { 1, 3, 5 });

            userAuthenticationToken = service.GetUserAuthenticationJwt(userId, userName);

            var spaceBattleAuthenticationJwt =
                service.GetStarBattleAuthorizationJwt(spaceBattleId, userAuthenticationToken);

            var result = service.ValidateToken(spaceBattleAuthenticationJwt);

            Assert.AreEqual(result, validationResult);
        }

        /*[TestMethod]
        public void ConfirmThatGameServerExecutedAuthenticatedUsersCommandsInSpaceBattle()
        {
            var userId = 2;
            var userName = "SecondUser";
            var validationResult = false;

            var rabbitService = new RabbitMqService();            

            var service = new AuthenticationService();
            var userAuthenticationToken = service.GetUserAuthenticationJwt(userId, userName);
            var spaceBattleId = service.SpaceBattleRegister(userAuthenticationToken, new[] { 1, 2, 3, 4, 5 });
            var spaceBattleAuthenticationToken = service.GetStarBattleAuthorizationJwt(spaceBattleId, userAuthenticationToken);
            
            var commandMessage = new CommandMessage
            {
                GameKey = 1.ToString(),
                GameObjKey = Guid.NewGuid(),
                OperationKey = "EmptyCommand",
                args = [spaceBattleAuthenticationToken]
            };
            var json = JsonSerializer.Serialize(commandMessage);

            rabbitService.SendMessage(json);
        }*/
    }
}
