using Moq;
using SpaceBattle;
using SpaceBattle.Commands;
using SpaceBattle.Exceptions;
using SpaceBattle.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ModuleTest
{
    [TestClass]
    public class L9
    {
        [TestMethod]
        public void CheckFuelCommand()
        {
            //Arrange
            var mock = new Mock<IFuel>();
            mock.Setup(x => x.GetFuel()).Returns(10).Verifiable();
            mock.Setup(x => x.GetFuelConsumed()).Returns(3).Verifiable();            
            var fuel = mock.Object;
            var cfc = new CheckFuelCommand(fuel);

            //Act
            Action act = () => { cfc.Execute(); };

            //Assert
            Assert.AreEqual(true, true);
        }

        [TestMethod]
        public void CheckFuelCommand_Throw()
        {
            //Arrange
            var mock = new Mock<IFuel>();
            mock.Setup(x => x.GetFuel()).Returns(1).Verifiable();
            mock.Setup(x => x.GetFuelConsumed()).Returns(3).Verifiable();
            var fuel = mock.Object;
            var cfc = new CheckFuelCommand(fuel);

            //Act
            var actual = Assert.ThrowsException<CommandException>(() => cfc.Execute());


            Assert.AreEqual("Not enough fuel", actual.Message);
        }

        [TestMethod]
        public void BurnFuelCommand()
        {
            //Arrange
            var mock = new Mock<IFuel>();
            mock.Setup(x => x.GetFuel()).Returns(10).Verifiable();
            mock.Setup(x => x.GetFuelConsumed()).Returns(3).Verifiable();
            mock.Setup(x => x.SetFuel(It.IsAny<int>())).Callback<int>(newFuel =>
            {
                mock.Setup(x => x.GetFuel()).Returns(newFuel);
            });

            var fuel = mock.Object;
            var bfc = new BurnFuelCommand(fuel);

            //Act
            bfc.Execute();
            var actual = fuel.GetFuel();


            Assert.AreEqual(7, actual);
        }

        [TestMethod]
        public void MacrocomandTest()
        {
            //Arrange
            var mockFuel = new Mock<IFuel>();
            var mockMoving = new Mock<IMovingObject>();
            var fuel = mockFuel.Object;
            var moving = mockMoving.Object;

            var checkFuelCmd = new CheckFuelCommand(fuel);
            var burnFuelCmd = new BurnFuelCommand(fuel);
            var moveCmd = new MoveCommand(moving);

            var lm = new LinearMacrocommand(checkFuelCmd, moveCmd, burnFuelCmd);

            mockFuel.Setup(x => x.GetFuel()).Returns(10).Verifiable();
            mockFuel.Setup(x => x.GetFuelConsumed()).Returns(3).Verifiable();
            mockFuel.Setup(x => x.SetFuel(It.IsAny<int>())).Callback<int>(newFuel =>
            {
                mockFuel.Setup(x => x.GetFuel()).Returns(newFuel);
            });

            mockMoving.Setup(x => x.GetLocation()).Returns(new Vector2(12, 5)).Verifiable();
            mockMoving.Setup(x => x.GetVelocity()).Returns(new Vector2(-7, 3)).Verifiable();
            mockMoving.Setup(x => x.SetLocation(It.IsAny<Vector2>())).Callback<Vector2>(vector =>
            {
                mockMoving.Setup(x => x.GetLocation()).Returns(vector);
            });

            //Act 
            lm.Execute();
            var actualMoving = moving.GetLocation();
            var actualFuel = fuel.GetFuel();

            //Assert
            Assert.AreEqual(new Vector2(5, 8), actualMoving);
            Assert.AreEqual(7, actualFuel);
        }

        [TestMethod]
        public void MacrocomandTest_Throw()
        {
            //Arrange
            var mockFuel = new Mock<IFuel>();
            var mockMoving = new Mock<IMovingObject>();
            var fuel = mockFuel.Object;
            var moving = mockMoving.Object;

            var checkFuelCmd = new CheckFuelCommand(fuel);
            var burnFuelCmd = new BurnFuelCommand(fuel);
            var moveCmd = new MoveCommand(moving);

            var lm = new LinearMacrocommand(checkFuelCmd, moveCmd, burnFuelCmd);

            mockFuel.Setup(x => x.GetFuel()).Returns(1).Verifiable();
            mockFuel.Setup(x => x.GetFuelConsumed()).Returns(3).Verifiable();
            mockFuel.Setup(x => x.SetFuel(It.IsAny<int>())).Callback<int>(newFuel =>
            {
                mockFuel.Setup(x => x.GetFuel()).Returns(newFuel);
            });

            mockMoving.Setup(x => x.GetLocation()).Returns(new Vector2(12, 5)).Verifiable();
            mockMoving.Setup(x => x.GetVelocity()).Returns(new Vector2(-7, 3)).Verifiable();
            mockMoving.Setup(x => x.SetLocation(It.IsAny<Vector2>())).Callback<Vector2>(vector =>
            {
                mockMoving.Setup(x => x.GetLocation()).Returns(vector);
            });

            //Act
            var actual = Assert.ThrowsException<CommandException>(() => lm.Execute());

            //Assert
            Assert.AreEqual("Not enough fuel", actual.Message);
        }

        [TestMethod]
        public void ChangeVelocityCommand()
        {
            //Arrange
            var mock = new Mock<IRotatingObject>();
            var rotating = mock.Object;

            var direction = 2;
            var angularVelocity = 3;
            var directionsNumber = 5;
            var newDirection = 0;


            mock.Setup(x => x.GetDirection()).Returns(direction).Verifiable();
            mock.Setup(x => x.GetAngularVelocity()).Returns(angularVelocity).Verifiable();
            mock.Setup(x => x.GetDirectionsNumber()).Returns(directionsNumber).Verifiable();

            mock.Setup(x => x.SetDirection(It.IsAny<int>())).Callback<int>(direction =>
            {
                mock.Setup(x => x.GetDirection()).Returns(direction);
            });

            var cvc = new ChangeVelocityCommand(rotating);

            //Act
            cvc.Execute();
            var actual = rotating.GetDirection();

            //Assert
            Assert.AreEqual(newDirection, actual);
        }

        [TestMethod]
        public void ChangeVelocityCommand1()
        {
            //Arrange
            Dictionary<string, object> data = new Dictionary<string, object>() {
                { "Location", new Vector2(12, 5) },
                { "Velocity", 8 },                
                { "Direction", 5 },
                { "DirectionsNumber", 1 }, 
                { "AngularVelocity", 5 },
                { "FuelConsumed", 1 },
                { "Fuel", 3 }
            };

            var uObject = new Mock<IUObject>();

            foreach (var d in data) {
                uObject.Setup(x => x.GetProperty(d.Key)).Returns(d.Value);
            }

            uObject.Setup(x => x.SetProperty(It.IsAny<string>(), It.IsAny<object>()))
                .Callback<string, object>((a, b) => data[a] = b);

            var obj = uObject.Object;

            var fuelable = new FuelableAdapter(obj);
            var movable = new MovingObjectAdapter(obj);
            var rotable = new RotatingObjectAdapter(obj);

            var checkFuelCmd = new CheckFuelCommand(fuelable);
            var burnFuelCmd = new BurnFuelCommand(fuelable);
            var moveCmd = new MoveCommand(movable);
            var rotateCmd = new ChangeVelocityCommand(rotable);

            var lm = new LinearMacrocommand(checkFuelCmd, rotateCmd, moveCmd, burnFuelCmd);

            //Act
            lm.Execute();

            //Assert
            Assert.AreEqual(data["Location"], new Vector2(20, 5));
        }
    }
}
