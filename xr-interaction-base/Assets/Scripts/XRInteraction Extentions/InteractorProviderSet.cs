using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace ATG.XRInteraction.Extensions
{
    [Serializable]
    public sealed class InteractorProviderSet : IInteractorsHolder
    {
        [SerializeField] private InteractorProvider[] providers;
        
        public bool GetInteractorActiveselfByType(InteractionType type)
        {
            InteractorProvider select = GetProviderByType(type);
            return select.IsActive;
        }

        public void SetInteractorActiveByType(InteractionType type, bool isActive)
        {
            InteractorProvider select = GetProviderByType(type);
            select.SetActive(isActive);
        }

        public void SetActiveAllInteractors(bool isActive)
        {
            foreach (var provider in providers)
            {
                provider.SetActive(isActive);
            }
        }

        private InteractorProvider GetProviderByType(InteractionType type)
        {
            InteractorProvider selected = Array.Find(providers, provider => provider.Type == type);

            if (selected == null)
            {
                throw new NullReferenceException($"Interactor with type={type} doesn't exist !");
            }

            return selected;
        }

        public XRBaseControllerInteractor GetInteractorByType(InteractionType type)
        {
            InteractorProvider selected = Array.Find(providers, provider => provider.Type == type);

            if (selected == null)
            {
                throw new NullReferenceException($"Interactor with type={type} doesn't exist !");
            }

            return selected.Interactor;
        }
    }
}