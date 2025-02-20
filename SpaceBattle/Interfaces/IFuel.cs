namespace SpaceBattle.Interfaces
{
    public interface IFuel
    {
        public int GetFuel();

        public int GetFuelConsumed();

        public void SetFuel(int newValue);
    }
}
