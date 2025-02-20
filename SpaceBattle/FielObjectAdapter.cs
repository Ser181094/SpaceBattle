using SpaceBattle.Interfaces;

namespace SpaceBattle
{
    public class FuelableAdapter : IFuel
    {
        IUObject _uObject;

        public FuelableAdapter(IUObject obj)
        {
            _uObject = obj;
        }

        public int GetFuel()
        {
            return (int)_uObject.GetProperty("Fuel");
        }

        public int GetFuelConsumed()
        {
            return (int)_uObject.GetProperty("FuelConsumed");
        }

        public void SetFuel(int newValue)
        {
            _uObject.SetProperty("Fuel", newValue);
        }
    }
}
