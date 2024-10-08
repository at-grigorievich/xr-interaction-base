using System;
using ATG.Animator;
using ATG.Moveable;
using ATG.MVC;
using ATG.StateMachine;
using ATG.Voice;
using UnityEngine;
using VContainer;

using SM = ATG.StateMachine.StateMachine;

namespace ATG.Factory
{
    [Serializable]
    public sealed class AvatarFactory
    {
        [SerializeField] private AvatarView avatarView;
        
        [SerializeField] private AnimatorServiceFactory avatarAnimatorCreator;
        [SerializeField] private NavTargetMoveServiceFactory moveServiceCreator;
        [SerializeField] private FaceVoiceServiceFactory faceVoiceServiceFactory;

        public void Create(IContainerBuilder builder)
        {
            IAnimatorService animatorService = avatarAnimatorCreator.Create();
            IMoveableService moveService = moveServiceCreator.Create();
            IVoiceService voiceService = faceVoiceServiceFactory.Create(animatorService);

            SM sm = new(exception => Debug.LogError(exception));

            voiceService.SetLoop(false);

            sm.AddStatementsRange
            (
                new AvatarIdleState(animatorService, moveService, sm),
                new AvatarMoveToTargetState(animatorService, moveService, sm),
                new CharacterTalkingState(voiceService, animatorService, sm),
                new AvatarRotateToTargetState(animatorService, moveService, sm)
            );

            AvatarController controller = new(sm, animatorService, moveService, voiceService, avatarView);

            controller.SetActive(false);

            builder.RegisterInstance(controller).AsSelf().AsImplementedInterfaces();
        }
    }
}