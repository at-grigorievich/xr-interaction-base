using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace ATG.UI
{
    public sealed class HeightCallibrateView: UIElement
    {
        [SerializeField] private TextMeshProUGUI infoOutput;
        [SerializeField] private Slider heightSlider;
        [SerializeField] private ScaleClickButton scaleClickButton;

        public float Value => heightSlider.value;

        public Slider.SliderEvent OnSliderValueChanged => heightSlider.onValueChanged;

        public void SetupInitialHeight(float initHeight)
        {
            heightSlider.value = initHeight;
            OnValueChanged(initHeight);
        }

        public override void Show(object sender, object data)
        {
            base.Show(sender, data);

            if(data is not Action onCallibrated)
            {
                throw new ArgumentException("data must be action callback!");
            }
            
            scaleClickButton.Show(this, onCallibrated);
            heightSlider.onValueChanged.AddListener(OnValueChanged);
        }

        public override void Hide()
        {
            base.Hide();

            heightSlider.onValueChanged.RemoveListener(OnValueChanged);
            scaleClickButton.Hide();
        }

        private void OnValueChanged(float newValue)
        {
            infoOutput.text = $"Текущий оффсет: {newValue}";   
        }
    }
}