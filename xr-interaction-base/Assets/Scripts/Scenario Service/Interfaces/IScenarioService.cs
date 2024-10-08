using System;

namespace ATG.Scenario
{
    public interface IScenarioService
    {
        event Action OnComplete;

        void StartOrContinue();
        void Pause();
        void Restart();
    }
}