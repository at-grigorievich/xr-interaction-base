using System;
using ATG.Activator;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace ATG.XRInteraction.Extensions
{
    [Serializable]
    public sealed class InteractorProvider : IActivateable
    {
        [SerializeField] private InteractionType interactionType;
        [SerializeField] private XRBaseControllerInteractor interactor;

        public InteractionType Type => interactionType;
        public XRBaseControllerInteractor Interactor => interactor;
        public bool IsActive { get; private set; }

        public void SetActive(bool isActive)
        {
            IsActive = isActive;

            if(interactor != null)
            {
                interactor.enabled = isActive;
            }
        }
    }
}