using ATG.Animator;
using ATG.Moveable;
using ATG.MVC;
using ATG.StateMachine;
using UnityEngine;
using VContainer;

namespace ATG.Scenario
{
    public class PlaceAvatarToPointCommand : ScenarioCommand
    {
        [SerializeField] private AnimatorEnum animationInIdling;
        [SerializeField] private Transform point;

        private AvatarController _controller;
        private IStateSwitcher _sw;

        [Inject]
        public void Constructor(AvatarController controller)
        {
            _controller = controller;
            _sw = _controller.StateSwitcher;
        }

        public override void Execute()
        {
            _sw.SwitchState<AvatarIdleState, PlaceData>(new PlaceData(point, animationInIdling));

            base.Execute();
            Complete();
        }

        public override void Exit()
        {
            _sw.CurrentState?.Exit();
        }

    }
}