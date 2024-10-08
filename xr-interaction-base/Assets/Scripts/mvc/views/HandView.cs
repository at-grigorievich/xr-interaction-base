using ATG.UI;
using ATG.XRInteraction.Extensions;
using UnityEngine;

namespace ATG.MVC
{
    public class HandView : View
    {
        [SerializeField] private Renderer[] handRendererSet;
        [field: SerializeField] public InteractorProviderSet InteractorsSet { get; private set; }

        [SerializeField] private InfoView tutorialView;
        [SerializeField] private BarProgressView progressView;

        public Transform RigRoot => transform;

        public override void SetActive(bool isActive)
        {
            base.SetActive(isActive);

            tutorialView?.SetActive(false);
            progressView?.SetActive(false);
        }

#region Tutorial info
        public void ShowTutorialInfo(in string info) => tutorialView?.Show(this, info);
        public void HideTutorialInfo() => tutorialView?.Hide();
#endregion
#region Info&Rate info
        public void ShowOnlyInfo(in string info, in Vector3 defaultPosition, in Vector3 defaultRotation)
        {
            progressView?.Show(this, info);
            progressView?.SetRateVisible(false);

            if (progressView != null)
            {
                progressView.transform.localPosition = defaultPosition;
                progressView.transform.localRotation = Quaternion.Euler(defaultRotation);
            }
        }

        public void ShowRateInfo(in string info, in float defaultRate, in Vector3 defaultPosition, in Vector3 defaultRotation)
        {
            ShowRateInfo(info, defaultRate);
            
            if (progressView != null)
            {
                progressView.transform.localPosition = defaultPosition;
                progressView.transform.localRotation = Quaternion.Euler(defaultRotation);
            }
        }
        
        public void ShowRateInfo(in string infoText, in float defaultRate) 
        {
            progressView?.Show(this, infoText);
            
            progressView?.SetRateVisible(true);
            progressView?.ShowRate(defaultRate);
        }

        public void HideInfo() => progressView?.Hide();

        public void UpdateRateInfo(in float nextRate) => progressView?.ShowRate(nextRate);
#endregion
    }
}