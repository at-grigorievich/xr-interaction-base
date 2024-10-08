using ATG.MVC;
using UnityEngine;
using VContainer;

namespace ATG.Scenario
{
    public class PlaceSocketCommand : ScenarioCommand
    {
        [SerializeField, TextArea] private string playerInfoOutput;
        [SerializeField] private SocketView socket;

        [SerializeField] private Vector3 playerInfoPosition;
        [SerializeField] private Vector3 playerInfoRotation;

        private VirtualAgentController _virtualAgent;

        [Inject]
        public void Constructor(VirtualAgentController virtualAgent)
        {
            _virtualAgent = virtualAgent;
        }

        public override void Execute()
        {
            base.Execute();

            _virtualAgent.ShowOnlyInfo(playerInfoOutput, playerInfoPosition, playerInfoRotation);
            
            socket.OnPlaced += Complete;
            socket.SetActive(true);
        }

        public override void Exit()
        {
            _virtualAgent.HideRateInfo();
            socket.SetActive(false);
        }

        protected override void Complete()
        {
            Exit();
            base.Complete();
        }
    }
}