namespace SpaceBattle.Interfaces
{
    public interface IState
    {
        void Handle(ICommand command);
    }
}
