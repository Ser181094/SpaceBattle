using SpaceBattle.Exceptions;
using SpaceBattle.Interfaces;

namespace SpaceBattle.Commands
{
    public class СheckСollisionsCommand : ICommand
    {
        IMovingObject _gameObject1;
        IMovingObject _gameObject2;

        public СheckСollisionsCommand(IMovingObject obj1, IMovingObject obj2) { _gameObject1 = obj1; _gameObject2 = obj2; }

        public void Execute()
        {
            if (_gameObject1.GetLocation() == _gameObject2.GetLocation())
                throw new CollisionException();
        }
    }
}
