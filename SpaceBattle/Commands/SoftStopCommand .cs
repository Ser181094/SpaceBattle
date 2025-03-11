using SpaceBattle.Interfaces;

namespace SpaceBattle.Commands
{
    public class SoftStopCommand : ICommand
    {
        private ServerThread _st;
        public SoftStopCommand(ServerThread st)
        {
            _st = st;
        }

        public void Execute()
        {
            Action oldBehaviour = _st._behaviour;
            _st.SetBehaviour(() => {                
                if (_st._q.Count > 0)
                {
                    oldBehaviour();
                }
                else
                {
                    _st.Stop();
                }
            });
        }
    }
}
