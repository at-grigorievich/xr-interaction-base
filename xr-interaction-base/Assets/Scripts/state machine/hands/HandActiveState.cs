using ATG.Animator;
using ATG.DTO;
using ATG.MVC;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ATG.StateMachine
{
    public sealed class HandActiveState : Statement
    {
        private readonly IAnimatorService _animatorService;

        private readonly HandActiveAmplitudeClamp _gripAmplitudeClamp;
        private readonly HandActiveAmplitudeClamp _triggerAmplitudeClamp;

        private readonly InputActionReference _gripAction;
        private readonly InputActionReference _triggerAction;

        private bool _allowRead;

        public HandActiveState(IAnimatorService animatorService, HandActionsDTO actionsDTO,
            HandActiveAmplitudeClamp gripAmplitudeClamp, HandActiveAmplitudeClamp triggerAmplitudeClamp,
            IStateSwitcher sw) : base(sw)
        {
            _animatorService = animatorService;

            _gripAction = actionsDTO.GripAction;
            _triggerAction = actionsDTO.TriggerAction;

            _gripAmplitudeClamp = gripAmplitudeClamp;
            _triggerAmplitudeClamp = triggerAmplitudeClamp;
        }

        public override void Enter()
        {
            _allowRead = true;
        }

        public override void Exit()
        {
            _allowRead = false;

            AnimateGripAction(0f);
            AnimateTriggerAction(0f);
        }

        public override void Execute()
        {
            if (_allowRead == false) return;

            float gripValue = _gripAction.action.ReadValue<float>();
            float triggerValue = _triggerAction.action.ReadValue<float>();

            gripValue = _gripAmplitudeClamp.GetClampedAmplutide(gripValue); //0.3f - for gisp
            triggerValue = _triggerAmplitudeClamp.GetClampedAmplutide(triggerValue);

            AnimateGripAction(gripValue);
            AnimateTriggerAction(triggerValue);
        }

        private void AnimateGripAction(in float gripValue) =>
            _animatorService.CrossFadeAnimate(AnimatorEnum.Grip, gripValue);

        private void AnimateTriggerAction(in float triggerValue) =>
            _animatorService.CrossFadeAnimate(AnimatorEnum.Trigger, triggerValue);
    }
}