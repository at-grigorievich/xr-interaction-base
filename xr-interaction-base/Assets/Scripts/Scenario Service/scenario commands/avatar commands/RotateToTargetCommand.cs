using ATG.Moveable;
using ATG.MVC;
using ATG.StateMachine;
using UnityEngine;
using VContainer;

namespace ATG.Scenario
{
    public sealed class RotateToTargetCommand : ScenarioCommand
    {
        [SerializeField] private Transform lookTarget;

        private AvatarController _avatarController;
        private VirtualAgentController _virtualAgentController;

        private IStateSwitcher _sw;

        [Inject]
        public void Constructor(AvatarController avatarController, VirtualAgentController virtualAgentController)
        {
            _avatarController = avatarController;
            _virtualAgentController = virtualAgentController;

            _sw = _avatarController.StateSwitcher;
        }

        public override void Execute()
        {
            SubscribeStateToComplete<AvatarRotateToTargetState>(_sw);

            _sw.SwitchState<AvatarRotateToTargetState, Transform>(lookTarget == null
                                    ? _virtualAgentController.ViewTransform
                                    : lookTarget);      
            base.Execute();
        }

        public override void Exit()
        {
        }

        protected override void Complete()
        {
            _sw.SwitchState<AvatarIdleState, PlaceData>(PlaceData.IdleWait);
            base.Complete();
        }
    }
}