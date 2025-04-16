namespace SpaceBattle.Interfaces
{
    public interface ISector
    {

        public List<IMovingObject> GetGameObjects();

        public void AddGameObject(IMovingObject gameObject);

        public void RemoveGameObject(IMovingObject gameObject);
    }
}
