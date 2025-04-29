namespace SpaceBattle.Interfaces
{
    public interface IMessage
    {
        public string GameObjectID { get; }
        public string Action { get; }
        public IDictionary<string, object> Properties { get; }
    }
}
