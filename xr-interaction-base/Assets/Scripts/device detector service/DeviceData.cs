using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ATG.VRDevice
{
    [CreateAssetMenu(menuName = "configs/device data", fileName = "new_devices_config")]
    public sealed class DeviceData : ScriptableObject
    {
        [SerializeField] private Device[] devices;

        public IEnumerable<KeyValuePair<string, SupportedDeviceType>> SupportedDevices =>
            devices.Select(device => new KeyValuePair<string, SupportedDeviceType>(device.DeviceManufacturer, device.DeviceType));

        [Serializable]
        private sealed class Device
        {
            [field: SerializeField] public string DeviceManufacturer { get; private set; }
            [field: SerializeField] public SupportedDeviceType DeviceType { get; private set; }
        }
    }
}