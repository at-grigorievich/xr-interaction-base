using System;
using ATG.Animator;
using ATG.Moveable;
using UnityEngine;

namespace ATG.StateMachine
{
    public sealed class AvatarRotateToTargetState : StatementWithData<Transform>
    {
        private readonly IAnimatorService _animatorService;
        private readonly IMoveableService _moveableService;

        public AvatarRotateToTargetState(IAnimatorService animatorService, IMoveableService moveService,
            IStateSwitcher sw) : base(sw)
        {
            _animatorService = animatorService;
            _moveableService = moveService;
        }

        public override void Enter()
        {
            _animatorService.CrossFadeAnimate(AnimatorEnum.TurnRight);
        }

        public override void Exit()
        {
            Data = null;
            _moveableService.Stop();
        }

        public override void Execute()
        {
            Vector3 targetDirection = GetTargetDirection();

            if (_moveableService.NeedRotate(targetDirection) == true)
            {
                _moveableService.Stop();
                _moveableService.Rotate(targetDirection, 1f);
            }
            else Complete();
        }

        private Vector3 GetTargetDirection()
        {
            if(Data == null) return Vector3.zero;

            Vector3 direction = Data.position - _moveableService.AgentPosition;
            return direction;
        }
    }
}