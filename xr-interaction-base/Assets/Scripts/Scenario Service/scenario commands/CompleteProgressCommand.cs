using ATG.MVC;
using UnityEngine;
using VContainer;

namespace ATG.Scenario
{
    public abstract class CompleteProgressCommand: ScenarioCommand
    {
        [SerializeField, TextArea] protected string progressInfo;
        [SerializeField] protected Vector3 progressUIPosition;
        [SerializeField] protected Vector3 progressUIRotation;

        protected VirtualAgentController _virtualAgentController;

        protected void Constructor(VirtualAgentController virtualAgentController)
        {
            _virtualAgentController = virtualAgentController;
        }

        public abstract void SubscribeToCompleteState();

        public override void Execute()
        {
            base.Execute();

            _virtualAgentController.ShowRateInfo(progressInfo, 0f, progressUIPosition, progressUIRotation);
            SubscribeToCompleteState();
        }

        public override void Exit()
        {
            _virtualAgentController.HideRateInfo();
        }

        protected override void Complete()
        {
            Exit();
            base.Complete();
        }
    }
}