using SpaceBattle.Exceptions;
using SpaceBattle.Interfaces;

namespace SpaceBattle.Commands
{
    public class MoveMacroCommand : ICommand
    {
        IMovingObject _movable;
        IPlayfield _playfield;

        public MoveMacroCommand(IMovingObject obj, IPlayfield field) { _movable = obj; _playfield = field; }

        public void Execute()
        {
            new MoveCommand(_movable).Execute();

            try
            {
                new CheckGameObjectCollisionCommand(_movable, _playfield).Execute();
            }
            catch (Exception ex)
            {
                throw new CollisionException("Collision exception", ex);
            }
        }
    }
}
