using System;
using ATG.Callibration;
using ATG.UI;
using Unity.XR.CoreUtils;
using UnityEngine;

namespace ATG.Factory
{
    [Serializable]
    public sealed class CallibrationServiceFactory
    {
        [SerializeField] private XROrigin _agentXROrigin;
        [SerializeField] private HeightCallibrateView heightCallibrateView;

        public ICallibrationService Create()
        {
            return new HeightCallibrationService(_agentXROrigin, heightCallibrateView);
        }
    }
}