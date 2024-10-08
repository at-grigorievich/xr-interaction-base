using System;
using UnityEngine;
using VContainer;

namespace ATG.VRDevice
{
    [Serializable]
    public sealed class DeviceDetectorFactory
    {
        [SerializeField] private DeviceData config;

        public void Create(IContainerBuilder builder)
        {
            builder.Register<DeviceDetectorService>(Lifetime.Singleton)
                .WithParameter<DeviceData>(config);
        }
    }
}