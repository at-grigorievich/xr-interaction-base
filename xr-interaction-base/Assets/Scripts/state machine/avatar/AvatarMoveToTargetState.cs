using System;
using ATG.Animator;
using ATG.Moveable;
using UnityEngine;

namespace ATG.StateMachine
{
    public sealed class AvatarMoveToTargetState : StatementWithData<NavTargetData>
    {
        private readonly IAnimatorService _animatorService;
        private readonly IMoveableService _moveableService;

        private Action _completeAction;

        public AvatarMoveToTargetState(IAnimatorService animatorService, IMoveableService moveService,
            IStateSwitcher sw): base(sw)
        {
            _animatorService = animatorService;
            _moveableService = moveService;
        }

        public override void Enter()
        {
            _completeAction = Complete;
        }

        public override void Exit()
        {
            _moveableService.Stop();
            _completeAction = null;
        }

        public override void Execute()
        {
            Vector3 targetDirection = GetTargetDirection();
            Vector3 moveDirection = GetTargetPosition();

            if(_moveableService.NeedMove(moveDirection) == true)
            {
                _animatorService.CrossFadeAnimate(AnimatorEnum.Walk, ignoreRepeat: true);
                
                _moveableService.Move(moveDirection, 1f);
            }
            else
            {
                _animatorService.CrossFadeAnimate(AnimatorEnum.Idle, ignoreRepeat: true);
                _completeAction?.Invoke();
            }
        }

        protected override void Complete()
        {
            _moveableService.Stop();
            _completeAction = null;
            base.Complete();
        }

        private Vector3 GetTargetDirection()
        {
            Vector3 direction = Data.Target.position - _moveableService.AgentPosition;

            return direction;
        }

        private Vector3 GetTargetPosition()
        {
            Vector3 direction = _moveableService.AgentPosition - Data.Target.position;

            Vector3 target = Data.Target.position + Data.StopDistance * direction.normalized;

            if(direction.magnitude <= Data.StopDistance)
            { 
                target = _moveableService.AgentPosition;
            }

            return target;
        }
    }
}