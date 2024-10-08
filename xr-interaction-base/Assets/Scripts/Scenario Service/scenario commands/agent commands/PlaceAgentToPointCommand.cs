using ATG.MVC;
using ATG.StateMachine;
using UnityEngine;
using VContainer;

namespace ATG.Scenario
{
    public class PlaceAgentToPointCommand : ScenarioCommand
    {
        [SerializeField] private Transform point;

        private VirtualAgentController _controller;
        private IStateSwitcher _sw;

        [Inject]
        public void Constructor(VirtualAgentController controller)
        {
            _controller = controller;
        }

        public override void Execute()
        {
            _controller.SetParent(null, point.position, point.rotation);

            base.Execute();
            Complete();
        }

        public override void Exit()
        {
            _sw.CurrentState?.Exit();
        }

    }
}