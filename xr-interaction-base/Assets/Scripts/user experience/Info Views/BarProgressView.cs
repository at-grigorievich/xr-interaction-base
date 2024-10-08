using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace ATG.UI
{
    public sealed class BarProgressView : InfoView
    {
        [SerializeField] private GameObject barProgressObject;
        [SerializeField] private Image fillBar;
        [SerializeField] private float durationFill;

        private CancellationTokenSource _dis;

        public void SetRateVisible(bool isVisible) => barProgressObject.SetActive(isVisible);

        public void ShowRate(float rate)
        {
            if (rate > 1f || rate < 0f)
                rate = 0f;

            DisposeFilling();
            
            _dis = new CancellationTokenSource();

            //SetRateVisible(true);
            AnimateFilling(rate, _dis.Token).Forget();
        }
        
        public override void Dispose()
        {
            base.Dispose();
            DisposeFilling();
        }

        private async UniTask AnimateFilling(float rate, CancellationToken token)
        {
            Image fillingBar = fillBar;

            while(Mathf.Abs(rate - fillingBar.fillAmount) > 0.01f)
            {
                if(token.IsCancellationRequested == true) break;
                
                if(rate > fillBar.fillAmount)
                {
                    fillBar.fillAmount += durationFill * Time.deltaTime;
                }
                else
                {
                    fillBar.fillAmount -= durationFill * Time.deltaTime;
                }
                fillingBar.fillAmount = rate;

                await UniTask.Yield();
            }

            fillingBar.fillAmount = rate;
        }

        private void DisposeFilling()
        {
            _dis?.Cancel();
            _dis?.Dispose();

            _dis = null;
        }
    }
}