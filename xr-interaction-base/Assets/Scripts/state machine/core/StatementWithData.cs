namespace ATG.StateMachine
{
    public abstract class StatementWithData<T>: Statement
    {
        protected T Data;

        public StatementWithData(IStateSwitcher sw): base(sw) {}
        
        public virtual void SetupData(T data)
        {
            Data = data;
        }
    }
}
