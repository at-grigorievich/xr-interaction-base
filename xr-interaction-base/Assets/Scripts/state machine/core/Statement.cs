using System;

namespace ATG.StateMachine
{
	public abstract class Statement
	{
		protected readonly IStateSwitcher _stateSwitcher;

		public event Action OnComplete;

		public Statement(IStateSwitcher sw)
		{
			_stateSwitcher = sw;
		}

        public abstract void Enter();

        public abstract void Execute();

		public virtual void Exit(){}

        public override int GetHashCode() => GetType().GetHashCode();

        public override bool Equals(object obj) => GetHashCode() == obj.GetHashCode();

        protected virtual void Complete()
        {
            if (OnComplete == null) return;
            OnComplete?.Invoke();
        }
	}
}

