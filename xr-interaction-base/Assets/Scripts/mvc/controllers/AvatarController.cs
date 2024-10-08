using System;
using ATG.Animator;
using ATG.Moveable;
using ATG.StateMachine;
using ATG.Voice;
using VContainer.Unity;

using SM = ATG.StateMachine.StateMachine;

namespace ATG.MVC
{
    public class AvatarController : Controller<AvatarView>, ITickable
    {
        private readonly SM _sm;

        private readonly IAnimatorService _animatorService;
        private readonly IMoveableService _moveableService;
        private readonly IVoiceService _voiceService;

        public bool IsIdle => Type.Equals(_sm.CurrentState, typeof(AvatarIdleState));

        public IStateSwitcher StateSwitcher => _sm;

        public IVoiceService VoiceService => _voiceService;

        public AvatarController(SM sm, IAnimatorService animService, IMoveableService moveService, 
            IVoiceService voiceService, AvatarView view) 
            : base(view)
        {
            _sm = sm;
            _animatorService = animService;
            _moveableService = moveService;
            _voiceService = voiceService;
        }

        public override void SetActive(bool isActive)
        {
            base.SetActive(isActive);
            
            _moveableService.SetActive(isActive);
            _animatorService.SetActive(isActive);
            _voiceService.SetActive(isActive);
            
            if (isActive == true)
            {
                _sm.SwitchState<AvatarIdleState>();
                _sm.StartOrContinueMachine();
            }
            else
            {
                _sm.PauseMachine();
            }
        }

        public void Tick()
        {
            if(IsActive == false) return;
            _sm.ExecuteMachine();
            _voiceService.Update();
        }

        public void Idle()
        {
            _sm.SwitchState<AvatarIdleState>();
        }

        public void Talking(VoiceData data)
        {
            if(data.WithAnimation == true)
            {
                _sm.SwitchState<CharacterTalkingState, VoiceData>(data);
            }
            else
            {
                _voiceService.PlaySound(data.Voice);
            }
        }

        public void ActivateRepeatTalking() => _view.ActivateRepeatTalking();
        public void DeactivateRepeatTalking() => _view.DeactivateRepeatTalking();
    }
}