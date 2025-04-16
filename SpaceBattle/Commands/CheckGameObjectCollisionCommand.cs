using SpaceBattle.Interfaces;

namespace SpaceBattle.Commands
{
    public class CheckGameObjectCollisionCommand : ICommand
    {
        IMovingObject _movable;
        IPlayfield _playfield;

        public CheckGameObjectCollisionCommand(IMovingObject obj, IPlayfield field) { _movable = obj; _playfield = field; }

        public void Execute()
        {
            var position = _movable.GetLocation();

            var newSectors = _playfield.GetSectorsOfPosition(position);

            var cmds = new List<ICommand>();

            foreach (var sector in newSectors)
            {
                var gameObjectsBySector = _playfield.GetGameObjectsBySector(sector);
                foreach (var gameObject in gameObjectsBySector)
                    cmds.Add(new СheckСollisionsCommand(gameObject, _movable));
            }

            new LinearMacrocommand(cmds.ToArray()).Execute();

            _playfield.UpdateSectorsForGameObject(_movable);
        }
    }
}
