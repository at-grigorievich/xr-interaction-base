using System;
using UnityEngine.XR.Interaction.Toolkit;

namespace ATG.XRInteraction.Extensions
{
    public interface ISocketPleaceable
    {
        public event Action<XRSocketInteractor> OnPlacedToSocket;
        void PlaceToSocket(XRSocketInteractor socket);
    }
}