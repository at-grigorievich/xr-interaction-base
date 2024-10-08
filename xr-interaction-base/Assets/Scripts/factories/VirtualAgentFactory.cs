using UnityEngine;
using ATG.MVC;
using VContainer;
using System;

namespace ATG.Factory
{
    [Serializable]
    public class VirtualAgentFactory
    {
        [SerializeField] private VirtualAgentView agentViewInstance;
        [SerializeField] private HandControllerFactory leftHandCreator;
        [SerializeField] private HandControllerFactory rightHandCreator;

        public void Create(IContainerBuilder builder)
        {
            HandController leftHand = leftHandCreator.Create();
            HandController rightHand = rightHandCreator.Create();

            VirtualAgentController controller = new(leftHand, rightHand, agentViewInstance);

            controller.SetupToXR();

            controller.SetActive(false);

            builder.RegisterInstance(controller).AsSelf().AsImplementedInterfaces();
        }
    }
}