using ATG.Moveable;
using ATG.MVC;
using ATG.StateMachine;
using UnityEngine;
using VContainer;

namespace ATG.Scenario
{
    public class MoveAvatarToTargetCommand: ScenarioCommand
    {
        [SerializeField] private Transform point;
        [SerializeField] private float stopDistance;

        private VirtualAgentController _virtualAgentController;
        private AvatarController _avatarController;
        private IStateSwitcher _sw;

        [Inject]
        public void Constructor(AvatarController controller, VirtualAgentController virtualAgentController)
        {
            _avatarController = controller;
            _sw = _avatarController.StateSwitcher;

            _virtualAgentController = virtualAgentController;

            if(stopDistance <= 0.4f) stopDistance = 0.4f;
        }

        public override void Execute()
        {
            SubscribeStateToComplete<AvatarMoveToTargetState>(_sw);

            NavTargetData data = 
                new NavTargetData(point == null ? _virtualAgentController.ViewTransform : point, stopDistance);
            
            _sw.SwitchState<AvatarMoveToTargetState, NavTargetData>(data);

            base.Execute();
        }

        public override void Exit()
        {
            UnsubscribeFromStateComplete();
            _sw.CurrentState?.Exit();
        }
    }
}