using System;

namespace ATG.StateMachine
{
	public interface IStateSwitcher
	{
		Statement CurrentState { get; }

		T GetState<T>(Type type) where T: Statement;
        T GetState<T>() where T : Statement;
		void SwitchState<T>() where T: Statement;
        void SwitchState<T, K>(K data) where T : StatementWithData<K>;
	}
}

