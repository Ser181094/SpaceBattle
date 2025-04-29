using SpaceBattle.Interfaces;

namespace SpaceBattle
{
    public class CommandFactory
    {
        public static object Invoke(params object[] args)
        {
            var message = (IMessage)args[0];

            try
            {
                var cmd = IoC.IoC.Resolve<ICommand>("Command." + message.Action, message.GameObjectID, message.Properties);
                return cmd;
            }
            catch (Exception ex)
            {
                throw new Exception("Unknown IoC dependency key: " + ex.Message);
            }
        }
    }
}
