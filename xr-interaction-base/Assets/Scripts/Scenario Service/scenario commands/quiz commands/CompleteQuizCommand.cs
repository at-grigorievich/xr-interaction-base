using ATG.UI;
using UnityEngine;

namespace ATG.Scenario
{
    public sealed class CompleteQuizCommand: ScenarioCommand
    {
        [SerializeField] private QuizView quizView;

        public override void Execute()
        {
            base.Execute();
            quizView.OnComplete += OnQuizComplete;
        }

        public override void Exit()
        {
            quizView.OnComplete -= OnQuizComplete;
        }

        private void OnQuizComplete()
        {
            Complete();
        }
    }
}