using System;
using ATG.Animator;
using ATG.MVC;
using UnityEngine;
using UnityEngine.InputSystem;
using ATG.StateMachine;

using SM = ATG.StateMachine.StateMachine;
using UnityEngine.XR.Interaction.Toolkit;

namespace ATG.Factory
{
    [Serializable]
    public sealed class HandControllerFactory
    {
        [SerializeField] private XRBaseController handXRController;
        [SerializeField] private HandView handViewInstance;
        [Space(10)]
        [SerializeField] private AnimatorServiceFactory animatorServiceFactory;
        [SerializeField] private HandActiveAmplitudeClampCreator gripAmplitudeClampCreator;
        [SerializeField] private HandActiveAmplitudeClampCreator triggerAmplitudeClampCreator;
        [Space(10)]
        [SerializeField] private InputActionReference gripInputAction;
        [SerializeField] private InputActionReference triggerInputAction;

        public HandController Create()
        {
            IAnimatorService animatorService = animatorServiceFactory.Create();

            HandActiveAmplitudeClamp gripAmplitudeClamp = gripAmplitudeClampCreator.Create();
            HandActiveAmplitudeClamp triggerAmplitudeClamp = triggerAmplitudeClampCreator.Create();

            SM sm = new((string exception) => Debug.LogError(exception));

            sm.AddStatementsRange
            (
                new HandActiveState(animatorService, new ATG.DTO.HandActionsDTO(gripInputAction, triggerInputAction),
                    gripAmplitudeClamp, triggerAmplitudeClamp, sm)
            );

            HandController controller = new(sm, handXRController, animatorService, 
                gripAmplitudeClamp, triggerAmplitudeClamp, handViewInstance);

            return controller;
        }
    }
}