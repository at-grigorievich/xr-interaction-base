using System;
using System.Collections.Generic;

namespace ATG.StateMachine
{
	/// <summary>
	/// Работа с постоянными состояниями обьекта. Все стейты добавляются при создании StateMachine.
	/// При помощи типа T можно менять текущее состояние обьекта.
	/// Существует функционал для запуска, остановки, бэкапа состояния обьекта.
	/// </summary>
	public sealed class StateMachine: IStateSwitcher
	{
		private readonly HashSet<Statement> _statemens;

		private readonly Action<string> _logger;

		private Statement _currentState = null;
		private Statement _previousState = null;

		public Statement CurrentState => _currentState;

		public StateMachine (Action<string> logger = null, params Statement[] statements)
		{
			_logger = logger;
			_statemens = new HashSet<Statement>(statements);
		}

		/// <summary>
		/// Adds the statements range.
		/// </summary>
		/// <param name="statements">Statements.</param>
		public void AddStatementsRange(params Statement[] statements)
		{
			foreach(var s in statements)
			{
				if(_statemens.Contains(s) == true)
				{
					_logger?.Invoke($"statemachine already exist {s.GetType().Name} state");
				} 

				_statemens.Add(s);
			}
		}

		/// <summary>
		/// Starts the or continue state machine.
		/// </summary>
		public void StartOrContinueMachine()
		{
			if (_currentState == null && _previousState != null) 
			{
				_currentState = _previousState;
			}

			if (_currentState == null) return;

			_currentState.Enter();
		}

		/// <summary>
		/// Executes the state machine (usually invoke in UpdateInfo()).
		/// </summary>
		public void ExecuteMachine()
		{
			if (_currentState == null) return;

			_currentState.Execute();
		}

		/// <summary>
		/// Pauses the state machine.
		/// </summary>
		public void PauseMachine()
		{
			if (_currentState == null) return;
			_currentState.Exit();

			_previousState = _currentState;
			_currentState = null;
		}

		public void GoToPreviousState()
		{
			if (_previousState == null)	return;

			if (_currentState != null) 
			{
				_currentState.Exit();
			}

			_currentState = null;

			StartOrContinueMachine();
		}


		public void SwitchState<T>() where T: Statement
		{
			if (_currentState != null) 
			{
				_currentState.Exit();
			}

			foreach (var statement in _statemens) 
			{
				if(statement.GetType() == typeof(T))
				{
					_previousState = _currentState;
					_currentState = statement;

					break;
				}
			}

			if (_currentState == null)
				throw new Exception ("Statements not contains " + typeof(T).ToString ());

			StartOrContinueMachine();
		}
        public void SwitchState<T, K>(K data) where T : StatementWithData<K>
        {
            if (_currentState != null)
            {
                _currentState.Exit();
            }

            foreach (var statement in _statemens)
            {
                if (statement.GetType() == typeof(T))
                {
                    _previousState = _currentState;

                    var dataStatement = (StatementWithData<K>)statement;
                    dataStatement.SetupData(data);

                    _currentState = statement;

                    break;
                }
            }

            if (_currentState == null)
                throw new Exception("Statements not contains " + typeof(T).ToString());

            StartOrContinueMachine();
        }


        public T GetState<T>() where T : Statement
        {
            foreach (var statement in _statemens)
            {
                if (statement.GetType() == typeof(T))
                {
                    return statement as T;
                }
            }
            throw new Exception ("Statement with type " + typeof(T).ToString () + " can't find");
        }

        public T GetState<T>(Type type) where T : Statement
        {
            foreach (var statement in _statemens)
            {
                if (statement.GetType() == type)
                {
                    return statement as T;
                }
            }
			throw new Exception ("Statement with type " + type.ToString() + " can't find");
        }
    }
}

