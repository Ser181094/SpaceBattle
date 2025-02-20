using SpaceBattle.Interfaces;

namespace SpaceBattle.Commands
{
    public class ChangeVelocityCommand : ICommand
    {
        private IRotatingObject _object;

        public ChangeVelocityCommand(IRotatingObject o)
        {
            _object = o;
        }

        public void Execute()
        {
            _object.SetDirection(
            (_object.GetDirection() + _object.GetAngularVelocity())
                % _object.GetDirectionsNumber()
            );

        }
    }
}
