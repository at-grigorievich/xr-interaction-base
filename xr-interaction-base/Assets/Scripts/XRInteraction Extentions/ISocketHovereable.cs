using System;
using UnityEngine.XR.Interaction.Toolkit;

namespace ATG.XRInteraction.Extensions
{
    public interface ISocketHovereable
    {
        public event Action<XRSocketInteractor> OnHoveredToSocketEntered;
        public event Action<XRSocketInteractor> OnHoveredToSocketExited;
        void HoveredBySocketEntered(XRSocketInteractor socket);
        void HoveredBySocketExited(XRSocketInteractor socket);
    }
}