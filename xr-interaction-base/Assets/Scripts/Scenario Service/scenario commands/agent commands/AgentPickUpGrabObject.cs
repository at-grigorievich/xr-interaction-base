using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace ATG.Scenario
{
    public sealed class AgentPickUpGrabObject : ScenarioCommand
    {
        [SerializeField] private XRGrabInteractable grabObject;
        [SerializeField] private OutlineWithAnimation grabObjectOutline;

        public override void Execute()
        {
            base.Execute();
            
            if(grabObject.isSelected == true)
            {
                Complete();
                return;
            }
            
            grabObjectOutline.SetActive(true);
            grabObject.selectEntered.AddListener(OnGrabInteractableSelected);
        }

        public override void Exit()
        {
            grabObject.selectEntered.RemoveListener(OnGrabInteractableSelected);
            grabObjectOutline.SetActive(false);
        }

        protected override void Complete()
        {
            grabObjectOutline.SetActive(false);
            base.Complete();
        }

        private void OnGrabInteractableSelected(SelectEnterEventArgs _)
        {
            grabObject.selectEntered.RemoveListener(OnGrabInteractableSelected);
            Complete();
        }
    }
}