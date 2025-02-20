using SpaceBattle.Interfaces;

namespace SpaceBattle
{
    public class RotatingObjectAdapter : IRotatingObject
    {
        private IUObject _uObject;

        public RotatingObjectAdapter(IUObject uObject)
        {
            _uObject = uObject;
        }

        public int GetDirection()
        {
            return (int)_uObject.GetProperty("Direction");
        }

        public int GetAngularVelocity()
        {
            return (int)_uObject.GetProperty("AngularVelocity");
        }

        public int GetDirectionsNumber()
        {
            return (int)_uObject.GetProperty("DirectionsNumber");
        }

        public void SetDirection(int newDirection)
        {
            _uObject.SetProperty("Direction", newDirection);
        }
    }
}
