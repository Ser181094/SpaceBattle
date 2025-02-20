using System.Numerics;
using SpaceBattle.Interfaces;

namespace SpaceBattle
{
    public class MovingObjectAdapter : IMovingObject
    {
        private IUObject _uObject;

        public MovingObjectAdapter(IUObject uObject)
        {
            _uObject = uObject;
        }

        public Vector2 GetLocation()
        {
            return (Vector2)_uObject.GetProperty("Location");
        }

        public Vector2 GetVelocity()
        {
            int d = (int)_uObject.GetProperty("Direction");
            int n = (int)_uObject.GetProperty("DirectionsNumber");
            int v = (int)_uObject.GetProperty("Velocity");
            return new Vector2(
                v * (float)Math.Cos(d / 360 * n),
                v * (float)Math.Sin(d / 360 * n)
            );
        }

        public void SetLocation(Vector2 newValue)
        {
            _uObject.SetProperty("Location", newValue);
        }
    }
}