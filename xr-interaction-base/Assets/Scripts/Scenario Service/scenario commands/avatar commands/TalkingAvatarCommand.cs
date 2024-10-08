using ATG.Animator;
using ATG.MVC;
using ATG.StateMachine;
using ATG.Voice;
using UnityEngine;
using VContainer;

namespace ATG.Scenario
{
    public class TalkingAvatarCommand : ScenarioCommand
    {
        [SerializeField] private AnimatorEnum talkingAnimation;
        [SerializeField] private VoiceType voiceTag;
        [Space(5)]
        [SerializeField] private bool withAnimate = true;
        [SerializeField] private bool ignoreRepeat = false;

        private IRepeatVoiceService _repeatVoiceService;
        private AvatarController _controller;
        private IStateSwitcher _sw;

        [Inject]
        public void Constructor(AvatarController avatar, IRepeatVoiceService repeatVoiceService)
        {
            _controller = avatar;
            _repeatVoiceService = repeatVoiceService;

            _sw = _controller.StateSwitcher;
        }
        public override void Execute()
        {
            base.Execute();

            _repeatVoiceService.Stop();

            _controller.Talking(new VoiceData(voiceTag, talkingAnimation, withAnimate));
            
            if (withAnimate == true)
            {
                SubscribeStateToComplete<CharacterTalkingState>(_sw);
            }
            else
            {
                Complete();
            }
        }

        public override void Exit()
        {
            _controller.Idle();
        }

        protected override void Complete()
        {
            Exit();
            base.Complete();
            
            if(ignoreRepeat == false)
            {
                _repeatVoiceService.UpdateLastVoice(new VoiceData(voiceTag, talkingAnimation, withAnimate));
            }
        }
    }
}