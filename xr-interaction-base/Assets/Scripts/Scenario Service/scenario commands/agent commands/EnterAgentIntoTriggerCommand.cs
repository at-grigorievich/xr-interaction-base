using ATG.MVC;
using UnityEngine;

namespace ATG.Scenario
{
    [RequireComponent(typeof(Collider))]
    public sealed class EnterAgentIntoTriggerCommand : ScenarioCommand
    {
        private Collider _collider;

        private void Awake()
        {
            _collider = GetComponent<Collider>();
            _collider.enabled = false;
        }

        public override void Execute()
        {
            base.Execute();
            _collider.enabled = true;
        }

        public override void Exit()
        {
            _collider.enabled = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.attachedRigidbody.TryGetComponent<VirtualAgentView>(out VirtualAgentView view) == true)
            {
                _collider.enabled = false;
                Complete();
            }
        }
    }
}