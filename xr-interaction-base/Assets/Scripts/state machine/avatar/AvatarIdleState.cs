using ATG.Animator;
using ATG.Moveable;
using UnityEngine;

namespace ATG.StateMachine
{
    public sealed class AvatarIdleState : StatementWithData<PlaceData>
    {
        private readonly IAnimatorService _animatorService;
        private readonly IMoveableService _moveableService;

        public AvatarIdleState(IAnimatorService animatorService, IMoveableService moveService, IStateSwitcher sw) : base(sw)
        {
            _animatorService = animatorService;
            _moveableService = moveService;
        }

        public override void Enter()
        {
            if (Data.Placement != null)
            {
                _moveableService.Place(Data.Placement.position, Data.Placement.rotation);
            }
            _animatorService.CrossFadeAnimate(Data.AnimationType);
        }

        public override void Execute() { }
    }
}