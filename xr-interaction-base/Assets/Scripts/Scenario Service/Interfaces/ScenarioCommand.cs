using System;
using ATG.StateMachine;
using UnityEngine;
using UnityEngine.Events;

namespace ATG.Scenario
{
    public abstract class ScenarioCommand: MonoBehaviour
    {
        protected Statement _currentState;

        public event Action OnComplete;

        [SerializeField] private UnityEvent onExecuteCallbacks;
        [SerializeField] private UnityEvent onCompleteCallbacks;

        [ContextMenu("Complete")]
        public void TestComplete() => Complete();
        
        public virtual void Execute()
        {
            onExecuteCallbacks?.Invoke();
        }

        public abstract void Exit();
        
        protected virtual void Complete() 
        {
            if(_currentState != null)
            {
                UnsubscribeFromStateComplete();
                _currentState = null;
            }
            
            onCompleteCallbacks?.Invoke();
            OnComplete?.Invoke();
        }

        protected void SubscribeStateToComplete<T>(IStateSwitcher sw) where T: Statement
        {
            _currentState = sw.GetState<T>(typeof(T));
            
            if(_currentState == null)
             throw new NullReferenceException($"Cant find state with:{typeof(T)} type");

            _currentState.OnComplete += Complete;
        }

        protected void UnsubscribeFromStateComplete()
        {
            if(_currentState == null) return;
            _currentState.OnComplete -= Complete;
        }
    }
}