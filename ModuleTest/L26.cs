using Moq;
using SpaceBattle;
using SpaceBattle.Commands;
using SpaceBattle.Exceptions;
using SpaceBattle.Interfaces;
using System.Numerics;

namespace ModuleTest
{
    [TestClass]
    public class L26
    {
        [TestMethod]
        public void Test1()
        {
            var testdata = new
            {
                gameObjectsBySector = new List<IMovingObject>() {
                    new SpaceShip(new Vector2(5, 10), new Vector2(7, 3), 15, 90, 100),
                    new SpaceShip(new Vector2(5, 10), new Vector2(4, 5), 10, 55, 120),
                },
                sectorsOfPosition = new List<ISector>() {
                    new Sector("Sector1", 5, 10),
                },
            };

            var playField = new Mock<IPlayfield>();
            playField.Setup(obj => obj.GetSectorsOfPosition(It.IsAny<Vector2>())).Returns(testdata.sectorsOfPosition);
            playField.Setup(obj => obj.GetGameObjectsBySector(It.IsAny<ISector>())).Returns(testdata.gameObjectsBySector);


            var gameObject = new Mock<IMovingObject>();
            gameObject.Setup(obj => obj.GetLocation()).Returns(new Vector2(5, 10));

            var command = new MoveMacroCommand(gameObject.Object, playField.Object);

            Assert.ThrowsException<CollisionException>(command.Execute);
        }

        [TestMethod]
        public void Test2()
        {
            var testdata = new
            {
                gameObjectsBySector = new List<IMovingObject>() {
                    new SpaceShip(new Vector2(6, 11), new Vector2(7, 3), 15, 90, 100),
                    new SpaceShip(new Vector2(3, 7), new Vector2(4, 5), 10, 55, 120),
                },
                sectorsOfPosition = new List<ISector>() {
                    new Sector("Sector1", 5, 10),
                },
            };

            var playField = new Mock<IPlayfield>();
            playField.Setup(obj => obj.GetSectorsOfPosition(It.IsAny<Vector2>())).Returns(testdata.sectorsOfPosition);
            playField.Setup(obj => obj.GetGameObjectsBySector(It.IsAny<ISector>())).Returns(testdata.gameObjectsBySector);


            var gameObject = new Mock<IMovingObject>();
            gameObject.Setup(obj => obj.GetLocation()).Returns(new Vector2(5, 10));

            var command = new MoveMacroCommand(gameObject.Object, playField.Object);
            command.Execute();

            playField.Verify(c => c.UpdateSectorsForGameObject(It.IsAny<IMovingObject>()), Times.Exactly(1));
        }
    }
}
