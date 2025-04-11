using SpaceBattle.Commands;
using SpaceBattle.Interfaces;

namespace SpaceBattle
{ 
    partial class CommandState
    {
        public class MoveToState : IState
        {
            private CommandState state;

            public MoveToState(CommandState currentState)
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
                    case RunCommand runCommand:
                        state.currentState = new DefaultState(state);
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