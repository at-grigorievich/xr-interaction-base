using System;
using System.Collections.Generic;
using UnityEngine;

namespace ATG.Scenario
{
    public sealed class StepByStepScenarioService : MonoBehaviour, IScenarioService
    {
        [Tooltip("commands must go in orders")]
        [SerializeField] private ScenarioCommand[] commands;

        private Queue<ScenarioCommand> _commandsQueue;

        private ScenarioCommand _currentExecuteCommand;

        public event Action OnComplete;

        private void Awake()
        {
        }

        public void StartOrContinue()
        {
            _commandsQueue = new Queue<ScenarioCommand>(commands);
            _currentExecuteCommand = null;

            if (_commandsQueue.Count == 0)
            {
                Debug.LogWarning("Scenario has not commands!");
                return;
            }
            
            Continue();
        }

        public void Pause()
        {
            if (_currentExecuteCommand != null)
            {
                _currentExecuteCommand.Exit();
            }

            _currentExecuteCommand = null;
            _commandsQueue.Clear();
        }

        private void Continue()
        {
            if (_currentExecuteCommand != null)
            {
                _currentExecuteCommand.OnComplete -= Continue;
            }

            if (_commandsQueue.Count > 0)
            {
                _currentExecuteCommand = _commandsQueue.Dequeue();
                _currentExecuteCommand.OnComplete += Continue;

                _currentExecuteCommand.Execute();
            }
            else
            {
                _currentExecuteCommand = null;
                OnComplete?.Invoke();
            }
        }

        public void Restart()
        {
            if(_currentExecuteCommand != null)
            {
                _currentExecuteCommand.Exit();
                _currentExecuteCommand.OnComplete -= Continue;
                _currentExecuteCommand = null;
            }

            _commandsQueue = new Queue<ScenarioCommand>(commands);
            StartOrContinue();
        }
    }
}