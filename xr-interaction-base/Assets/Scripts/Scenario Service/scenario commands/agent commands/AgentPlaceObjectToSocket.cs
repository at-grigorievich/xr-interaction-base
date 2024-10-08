using UnityEngine;

namespace ATG.Scenario
{
    public sealed class AgentPlaceObjectToSocket : ScenarioCommand
    {
        [SerializeField] private GameObject target;
        [SerializeField] private TriggerObject placedSocket;

        public override void Execute()
        {
            base.Execute();

            placedSocket.SetActive(true);

            placedSocket.OnTriggerEntered += OnTriggerEntered;
        }

        public override void Exit()
        {
            placedSocket.OnTriggerEntered -= OnTriggerEntered;
            placedSocket.SetActive(false);
        }

        private void OnTriggerEntered(GameObject obj)
        {
            placedSocket.OnTriggerEntered -= OnTriggerEntered;
            placedSocket.SetActive(false);
            
            if(ReferenceEquals(target, obj) == true)
            {
                Complete();
            }
        }
    }
}