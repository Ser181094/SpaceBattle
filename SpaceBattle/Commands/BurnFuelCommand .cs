using SpaceBattle.Interfaces;

namespace SpaceBattle.Commands
{
    public class BurnFuelCommand : ICommand
    {
        private IFuel _fuel;

        public BurnFuelCommand(IFuel fuel)
        {
            _fuel = fuel;
        }
        public void Execute()
        {
            _fuel.SetFuel(_fuel.GetFuel() - _fuel.GetFuelConsumed());
        }
    }
}
