using UnityEngine;
using System;
using ATG.VRDevice;
using UnityEngine.UI;
using VContainer;

namespace ATG.UI
{
    public sealed class TutorialView: InfoView
    {  
        [Inject] private DeviceDetectorService _deviceDetectorService;

        [SerializeField] private TutorialIconByDevice[] iconsByDevice;
        
        public override void Show(object sender, object data)
        {
            base.Show(sender, data);

            SupportedDeviceType currentSupportedDevice = _deviceDetectorService.GetSupportedDeviceType();

            foreach(var icon in iconsByDevice)
            {
                icon.Update(currentSupportedDevice);
            }
        }

        [Serializable]
        private sealed class TutorialIconByDevice
        {
            [SerializeField] private bool isDefault;
            [SerializeField] private SupportedDeviceType deviceType;
            [SerializeField] private Image source;

            public void Update(SupportedDeviceType needDevice)
            {
                if(needDevice == SupportedDeviceType.None && isDefault == true)
                {
                    source.enabled = true;
                    return;
                }

                source.enabled = deviceType == needDevice;
            }
        }
    }
}