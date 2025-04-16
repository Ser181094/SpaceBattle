using SpaceBattle.Interfaces;
using System.Drawing;

namespace SpaceBattle
{
    public class Sector : ISector
    {
        private List<IMovingObject> _gameObjects;
        private Point _address;
        private string _name;

        public Sector(string name, int x, int y)
        {
            _name = name;
            _address = new Point(x, y);
            _gameObjects = new List<IMovingObject>();
        }

        public List<IMovingObject> GetGameObjects() => _gameObjects.ToList();

        public void AddGameObject(IMovingObject gameObject) => _gameObjects.Add(gameObject);

        public void RemoveGameObject(IMovingObject gameObject) => _gameObjects.Remove(gameObject);
        
        public Point Address => _address;
        public string Name => _name;
    }
}
