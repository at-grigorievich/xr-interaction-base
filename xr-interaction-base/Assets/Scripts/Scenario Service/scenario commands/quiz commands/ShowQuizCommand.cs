using ATG.UI;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace ATG.Scenario
{
    public sealed class ShowQuizCommand : ScenarioCommand
    {
        [SerializeField] private QuizView quizView;
        [SerializeField] private TeleportationAnchor quizTransformAnchor;

        public override void Execute()
        {
            base.Execute();
            quizView.Show(this, null);
            quizTransformAnchor.gameObject.SetActive(true);

            Complete();
        }

        public override void Exit()
        {
            quizTransformAnchor.gameObject.SetActive(false);
        }
    }
}