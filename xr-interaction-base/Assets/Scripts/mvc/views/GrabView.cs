using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace ATG.MVC
{
    /// <summary>
    /// Базовый класс для обьектов, которые можно брать в руки
    /// </summary>
    [RequireComponent(typeof(HandsGrabInteractable))]
    public abstract class GrabView : View
    {
        protected HandsGrabInteractable _grabInteractable;

        public SelectEnterEvent FirstSelected => _grabInteractable.firstSelectEntered;

        public SelectExitEvent LastExited => _grabInteractable.lastSelectExited;

        public event Action<HandType> OnHandSelectEntered
        {
            add => _grabInteractable.OnHandSelectEntered += value;
            remove => _grabInteractable.OnHandSelectEntered -= value;
        }

        public event Action<HandType> OnHandSelectExited
        {
            add => _grabInteractable.OnHandSelectExited += value;
            remove => _grabInteractable.OnHandSelectExited -= value;
        }

        public event Action<HandType> OnHandHoverEntered
        {
            add => _grabInteractable.OnHandHoverEntered += value;
            remove => _grabInteractable.OnHandHoverEntered -= value;
        }

        public event Action<HandType> OnHandHoverExited
        {
            add => _grabInteractable.OnHandHoverExited += value;
            remove => _grabInteractable.OnHandHoverExited -= value;
        }

        public bool IsGrabbed => _grabInteractable.isSelected;

        protected void Awake()
        {
            _grabInteractable = GetComponent<HandsGrabInteractable>();
        }

        public override void SetActive(bool isActive)
        {
            base.SetActive(isActive);

            if(_grabInteractable != null)
            {
                _grabInteractable.enabled = isActive;
            }
        }

        public void EnableGrabInteractable()
        {
            if(_grabInteractable == null) return;
            _grabInteractable.enabled = true;
        }

        public void DisableGrabInteractable() 
        {
            if(_grabInteractable == null) return;
            _grabInteractable.enabled = false;
        }
    }
}