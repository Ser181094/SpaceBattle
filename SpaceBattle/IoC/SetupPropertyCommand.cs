using SpaceBattle.Interfaces;

namespace SpaceBattle.IoC
{
    public class SetupPropertyCommand : ICommand
    {
        private ObjectImpl obj;
        private string key;
        private object value;

        public SetupPropertyCommand(ObjectImpl obj, string key, object value)
        {
            this.obj = obj;
            this.key = key;
            this.value = value;
        }

        public void Execute()
        {
            obj.SetProperty(key,value);
        }
    }
}
