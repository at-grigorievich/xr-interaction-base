using ATG.Animator;
using ATG.Voice;

namespace ATG.StateMachine
{
    public sealed class CharacterTalkingState : StatementWithData<VoiceData>
    {
        private readonly IVoiceService _voiceService;
        private readonly IAnimatorService _animatorService;

        public CharacterTalkingState(IVoiceService voiceService, IAnimatorService animatorService,
            IStateSwitcher sw) : base(sw)
        {
            _voiceService = voiceService;
            _animatorService = animatorService;
        }

        public override void Enter()
        {
            if (Data.WithAnimation == true)
            {
                _animatorService.CrossFadeAnimate(Data.Animation);
            }

            _voiceService.PlaySound(Data.Voice);
            _voiceService.OnSoundComplete += Complete;
        }

        public override void Exit()
        {
            _voiceService.OnSoundComplete -= Complete;
            //_voiceService.StopCurrentClip();
        }

        public override void Execute()
        {
        }
    }
}