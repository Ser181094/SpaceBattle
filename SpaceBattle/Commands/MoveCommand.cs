using SpaceBattle.Interfaces;
using System.Numerics;

namespace SpaceBattle.Commands
{
    public class MoveCommand : ICommand
    {
        private IMovingObject _object;

        public MoveCommand(IMovingObject o)
        {
            _object = o ;
        }

        public void Execute()
        {
            _object.SetLocation(
                Vector2.Add(
                    _object.GetLocation(), _object.GetVelocity()
                ));
        }
    }
}
