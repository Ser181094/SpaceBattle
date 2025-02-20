using SpaceBattle.Interfaces;

namespace SpaceBattle
{
    public class Rotate
    {
        private IRotatingObject _r;

        public Rotate(IRotatingObject r)
        {
            _r = r;
        }

        public void Execute()
        {
            _r.SetDirection((_r.GetDirection() + _r.GetAngularVelocity()) % _r.GetDirectionsNumber());
        }
    }
}
