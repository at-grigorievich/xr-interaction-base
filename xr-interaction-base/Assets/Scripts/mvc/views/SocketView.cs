using System;
using ATG.XRInteraction.Extensions;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using VContainer;

namespace ATG.MVC
{
    /// <summary>
    /// Базовый класс для обьектов, которые представляют собой сокеты точки
    /// </summary>
    [RequireComponent(typeof(XRSocketInteractor))]
    public class SocketView : View
    {
        [SerializeField] private Renderer visualization;
        [SerializeField] private Renderer[] otherVisualization;
        [SerializeField] private OutlineWithAnimation outlineWithAnimation;
        [Space(5)]
        [SerializeField] private bool isDisactiveOnAwake;
        [Space(5)]
        [SerializeField] private bool useHaptic;
        [SerializeField] private float amplitude;
        [SerializeField] private float duration;

        [Inject] private VirtualAgentController _virtualAgent;

        private XRSocketInteractor socketInteractor;

        public event Action OnPlaced;

        private void Awake()
        {
            socketInteractor = GetComponent<XRSocketInteractor>();
            SetActive(!isDisactiveOnAwake);
        }

        public override void SetActive(bool isActive)
        { 
            base.SetActive(isActive);

            SetVisual(isActive);
            socketInteractor.enabled = isActive;

            if (isActive == true)
            {
                socketInteractor.selectEntered.AddListener(OnSocketPlaced);

                socketInteractor.hoverEntered.AddListener(OnSocketHoverEntered);
                socketInteractor.hoverExited.AddListener(OnSocketHoverExited);
            }
            else
            {
                socketInteractor.hoverEntered.RemoveListener(OnSocketHoverEntered);
                socketInteractor.selectEntered.RemoveListener(OnSocketPlaced);
                socketInteractor.hoverExited.RemoveListener(OnSocketHoverExited);
            }
        }

        public void SetVisualActive(bool isActive)
        {
            if (IsActive == false) return;
            SetVisual(isActive);
        }

        private void SetVisual(bool isActive)
        {
            visualization.enabled = isActive;

            outlineWithAnimation.SetActive(isActive);

            if (otherVisualization != null)
            {
                foreach (var other in otherVisualization)
                {
                    other.enabled = isActive;
                }
            }
        }

        private void OnSocketHoverEntered(HoverEnterEventArgs arg)
        {
            if (useHaptic == false) return;

            if (_virtualAgent == null)
                throw new VContainerException(typeof(VirtualAgentController), $"You forget to resolve {transform.name}");

            if (_virtualAgent.LeftGrabInteractor.IsInteractedWith(arg.interactableObject.transform) == true)
            {
                _virtualAgent.LeftGrabInteractor.SendHapticImpulse(amplitude, duration);
            }

            if (_virtualAgent.RightGrabInteractor.IsInteractedWith(arg.interactableObject.transform) == true)
            {
                _virtualAgent.RightGrabInteractor.SendHapticImpulse(amplitude, duration);
            }

            socketInteractor.hoverEntered.RemoveListener(OnSocketHoverEntered);
            socketInteractor.hoverExited.AddListener(OnSocketHoverExited);

            if (arg.interactableObject.transform.TryGetComponent<ISocketHovereable>(out ISocketHovereable view) == true)
            {
                view.HoveredBySocketEntered(socketInteractor);
            }
        }

        private void OnSocketHoverExited(HoverExitEventArgs arg)
        {
            socketInteractor.hoverExited.RemoveListener(OnSocketHoverExited);
            socketInteractor.hoverEntered.AddListener(OnSocketHoverEntered);

            if (arg.interactableObject.transform.TryGetComponent<ISocketHovereable>(out ISocketHovereable view) == true)
            {
                view.HoveredBySocketExited(socketInteractor);
            }
        }

        private void OnSocketPlaced(SelectEnterEventArgs arg0)
        {
            if (arg0.interactableObject.transform.TryGetComponent<ISocketPleaceable>(out ISocketPleaceable view) == true)
            {
                view.PlaceToSocket(socketInteractor);
            }

            OnPlaced?.Invoke();
        }
    }
}