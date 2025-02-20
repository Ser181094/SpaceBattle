using SpaceBattle.Exceptions;
using SpaceBattle.Interfaces;

namespace SpaceBattle.Commands
{
    public class CheckFuelCommand : ICommand
    {
        private readonly IFuel _fuel;

        public CheckFuelCommand(IFuel fuel)
        {
            _fuel = fuel;
        }
        public void Execute()
        {
            if (_fuel.GetFuel() < _fuel.GetFuelConsumed())
            {
                throw new CommandException("Not enough fuel");
            } 
        }

    }
}
