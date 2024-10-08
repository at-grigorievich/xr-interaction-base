using System;
using ATG.UI;
using Unity.XR.CoreUtils;
using UnityEngine;

namespace ATG.Callibration
{
    /// <summary>
    /// Сервис позволяет каллибровать уровень высоты игрока
    /// И сохраняет установленное значение в PlayerPrefs
    /// </summary>
    public sealed class HeightCallibrationService : ICallibrationService
    {
        private const string HeightRef = "vr-agent-height";

        private readonly XROrigin _xrOrigin;

        private readonly HeightCallibrateView _uiView;

        public bool IsActive {get; private set;}

        public HeightCallibrationService(XROrigin agentXROrigin, HeightCallibrateView uiView)
        {
            _xrOrigin = agentXROrigin;
            _uiView = uiView;
        }

        public void SetActive(bool isActive)
        {
            IsActive = isActive;

            if(IsActive == true)
            {
                OpenCallibratePanel();
            }
            else
            {
                HideCallibratePanel();
            }
        }


        private void OpenCallibratePanel()
        {
            _uiView.SetupInitialHeight(GetHeightFromPrefs());
            _uiView.Show(this, (Action)OnHeightChangedSaved);

            _uiView.OnSliderValueChanged.AddListener(OnHeightValueChanged);
        }

        private void HideCallibratePanel()
        {
            _uiView.Hide();

            _xrOrigin.CameraYOffset = GetHeightFromPrefs();

            _uiView.OnSliderValueChanged.RemoveListener(OnHeightValueChanged);
        }

        private void OnHeightValueChanged(float height)
        {
            _xrOrigin.CameraYOffset = height;
        }

        private void OnHeightChangedSaved()
        {
            float newHeightValue = _uiView.Value;
            
            SaveNewHeightInPrefs(newHeightValue);
            //HideCallibratePanel();
        }

#region PlayerPrefs
        private float GetHeightFromPrefs()
        {
            if(PlayerPrefs.HasKey(HeightRef) == false)
            {
                return _xrOrigin.CameraYOffset;
            }

            return PlayerPrefs.GetFloat(HeightRef);
        }

        private void SaveNewHeightInPrefs(float newValue)
        {
            PlayerPrefs.SetFloat(HeightRef, newValue);
        }
#endregion
    }
}