using System.Numerics;

namespace SpaceBattle.Interfaces
{
    public interface IPlayfield
    {
        public List<ISector> GetSectorsOfPosition(Vector2 position);
        public List<ISector> GetSectorsOfGameObject(IMovingObject obj);

        public void UpdateSectorsForGameObject(IMovingObject obj);

        public List<IMovingObject> GetGameObjectsBySector(ISector sector);
    }
}
