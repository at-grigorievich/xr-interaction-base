using ATG.MVC;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace ATG.Scenario
{
    public sealed class ShowInputInfoAgentCommand : ScenarioCommand
    {
        [SerializeField, TextArea] private string text;
        [SerializeField] private InputActionReference[] completedActions;

        private VirtualAgentController _virtualAgentController;

        [Inject]
        public void Constructor(VirtualAgentController virtualAgentController)
        {
            _virtualAgentController = virtualAgentController;
        }

        public override void Execute()
        {
            base.Execute();
            
            _virtualAgentController.ShowTutorialInfo(text);

            foreach(var act in completedActions)
            {
                act.action.performed += OnActionPerformed;
            }
        }

        public override void Exit()
        {
            _virtualAgentController.HideTutorialInfo();
            
            foreach(var act in completedActions)
            {
                act.action.performed -= OnActionPerformed;
            }
        }

        private void OnActionPerformed(InputAction.CallbackContext context)
        {
            Complete();
        }

        protected override void Complete()
        {
            Exit();
            base.Complete();
        }
    }
}