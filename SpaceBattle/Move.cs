using System.Numerics;
using SpaceBattle.Interfaces;

namespace SpaceBattle
{
    public class Move
    {
        private IMovingObject obj;

        public Move(IMovingObject o)
        {
            obj = o;
        }

        public void Execute()
        {
            obj.SetLocation(
                Vector2.Add(
                    obj.GetLocation(), obj.GetVelocity()
                ));
        }
    }
}