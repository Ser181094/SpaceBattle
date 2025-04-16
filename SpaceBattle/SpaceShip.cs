using SpaceBattle.Interfaces;
using System.Numerics;

namespace SpaceBattle
{
    public class SpaceShip : IMovingObject
    {
        private Vector2 _location = Vector2.Zero;
        private Vector2 _velocity = Vector2.Zero;
        private int _direction = 0;
        private int _directionNumber = 0;
        private int _angularVelocity = 0;

        public SpaceShip(SpaceShip other)
        {
            _velocity = other._velocity;
            _location = other._location;
            _directionNumber = other._directionNumber;
            _angularVelocity = other._angularVelocity;
        }

        public SpaceShip(Vector2 location, Vector2 velocity, int direction, int directionNumber, int angularVelocity)
        {
            _location = location;
            _velocity = velocity;
            _direction = direction;
            _directionNumber = directionNumber;
            _angularVelocity = angularVelocity;
        }

        public Vector2 GetLocation()
        {
            return _location;
        }

        public Vector2 GetVelocity()
        {
            return _velocity;
        }

        public void SetLocation(Vector2 newValue)
        {
            _location = newValue;
        }
    }
}
