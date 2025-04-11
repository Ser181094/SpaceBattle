using SpaceBattle.Commands;
using SpaceBattle.Interfaces;

namespace SpaceBattle
{
    public partial class CommandState
    {
        public IState currentState;

        public CommandState()
        {
            this.currentState = new DefaultState(this);
        }

        public void Execute(ICommand action)
        {
            currentState.Handle(action);
        }

        public class DefaultState : IState
        {

            private CommandState state;

            public DefaultState(CommandState currentState)
            {
                this.state = currentState;
            }

            public void Handle(ICommand action)
            {
                switch (action)
                {
                    case HardStopCommand hardStop:
                        state.currentState = null;
                        action.Execute();
                        break;
                    case MoveToCommand moveToCommand:
                        state.currentState = new MoveToState(state);
                        state.currentState.Handle(action);
                        break;
                    default:
                        state.currentState = this;
                        action.Execute();
                        break;
                }
            }
        }
    }
}