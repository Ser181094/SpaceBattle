using SpaceBattle.Interfaces;

namespace SpaceBattle.Commands
{
    public class FindSectorAndCheckСollisionCommand : ICommand
    {
        IMovingObject _movable;
        public FindSectorAndCheckСollisionCommand(IMovingObject obj) { _movable = obj; }

        public void Execute() => _movable.GetLocation();
    }
}
