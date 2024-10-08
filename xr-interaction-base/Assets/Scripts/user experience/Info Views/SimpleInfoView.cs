using UnityEngine;

namespace ATG.UI
{
    public sealed class SimpleInfoView: InfoView
    {
        [SerializeField] private bool isHideOnAwake;

        private new void Awake()
        {
            base.Awake();

            if(isHideOnAwake == true)
            {
                SetActive(false);
            }
        }

        public void Show()
        {
            SetActive(true);

            Dispose();
            
            if (useFadeAnimation == true)
            {
                Fade();
            }
            else
            {
                canvasGroup.alpha = defaultAlpha;
            }
        }
    }
}