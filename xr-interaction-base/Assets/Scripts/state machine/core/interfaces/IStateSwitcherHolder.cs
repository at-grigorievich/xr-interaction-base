namespace ATG.StateMachine
{
    public interface IStateSwitcherHolder
    {
        IStateSwitcher StateSwitcher { get; }
    }
}
