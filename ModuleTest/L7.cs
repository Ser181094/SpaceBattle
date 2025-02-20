using SpaceBattle;
using Moq;
using System.Numerics;
using SpaceBattle.Interfaces;

namespace ModuleTest
{
    [TestClass]
    public class L7
    {
        [TestMethod]
        public void TestMethod7_3_1()
        {
            var mock = new Mock<IMovingObject>();

            var location = new Vector2(12, 5);
            var velocity = new Vector2(-7, 3);
            var newLocation = new Vector2(5, 8);


            mock.Setup(x => x.GetLocation()).Returns(location).Verifiable();
            mock.Setup(x => x.GetVelocity()).Returns(velocity).Verifiable();
            mock.Setup(x => x.SetLocation(It.IsAny<Vector2>())).Callback<Vector2>(vector =>
            {
                mock.Setup(x => x.GetLocation()).Returns(vector);
            });


            var move = new Move(mock.Object);
            move.Execute();

            var actual = mock.Object.GetLocation();

            Assert.AreEqual(actual.X, newLocation.X);
            Assert.AreEqual(actual.Y, newLocation.Y);
        }

        [TestMethod]
        public void TestMethod7_3_2()
        {
            var mock = new Mock<IMovingObject>();

            var location = new Vector2(12, 5);
            var velocity = new Vector2(-7, 3);
            var newLocation = new Vector2(5, 8);
            var expected = new Exception("Location is empty");


            mock.Setup(x => x.GetLocation()).Throws(expected);
            mock.Setup(x => x.GetVelocity()).Returns(velocity).Verifiable();
            mock.Setup(x => x.SetLocation(It.IsAny<Vector2>())).Callback<Vector2>(vector =>
            {
                mock.Setup(x => x.GetLocation()).Returns(vector);
            });

            var move = new Move(mock.Object);
            var actual = Assert.ThrowsException<Exception>(() => move.Execute());

            Assert.AreEqual(expected.Message, actual.Message);
        }

        [TestMethod]
        public void TestMethod7_3_3()
        {
            var mock = new Mock<IMovingObject>();

            var location = new Vector2(12, 5);
            var velocity = new Vector2(-7, 3);
            var newLocation = new Vector2(5, 8);
            var expected = new Exception("Velocity is empty");


            mock.Setup(x => x.GetLocation()).Returns(location).Verifiable();
            mock.Setup(x => x.GetVelocity()).Throws(expected);
            mock.Setup(x => x.SetLocation(It.IsAny<Vector2>())).Callback<Vector2>(vector =>
            {
                mock.Setup(x => x.GetLocation()).Returns(vector);
            });

            var move = new Move(mock.Object);
            var actual = Assert.ThrowsException<Exception>(() => move.Execute());

            Assert.AreEqual(expected.Message, actual.Message);
        }

        [TestMethod]
        public void TestMethod7_3_4()
        {
            var mock = new Mock<IMovingObject>();

            var location = new Vector2(12, 5);
            var velocity = new Vector2(-7, 3);
            var newLocation = new Vector2(5, 8);
            var expected = new Exception("It is impossible to change the position");

            mock.Setup(x => x.GetLocation()).Returns(location).Verifiable();
            mock.Setup(x => x.GetVelocity()).Returns(velocity).Verifiable();
            mock.Setup(x => x.SetLocation(newLocation)).Throws(expected);

            var move = new Move(mock.Object);
            var actual = Assert.ThrowsException<Exception>(() => move.Execute());

            Assert.AreEqual(expected.Message, actual.Message);
        }

        [TestMethod]
        public void TestMethod11_3_1()
        {
            var mock = new Mock<IRotatingObject>();

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

            var rotate = new Rotate(mock.Object);
            rotate.Execute();

            var actual = mock.Object.GetDirection();

            Assert.AreEqual(actual, newDirection);
        }
    }
}