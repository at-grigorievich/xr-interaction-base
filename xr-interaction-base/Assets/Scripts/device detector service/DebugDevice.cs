using UnityEngine;
using VContainer;

namespace ATG.VRDevice
{
    /// <summary>
    /// Debug gameobject vr headset device name
    /// </summary>
    public sealed class DebugDevice: MonoBehaviour
    {
        [Inject] private DeviceDetectorService _deviceDetectorService;

        [ContextMenu("Debug devices")]
        public void DebugDevices()
        {
            _deviceDetectorService.DebugDevices();
        }
    }
}