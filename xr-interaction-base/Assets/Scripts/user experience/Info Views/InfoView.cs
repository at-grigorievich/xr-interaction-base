using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace ATG.UI
{
    public class InfoView : UIView, IDisposable
    {
        [SerializeField] protected CanvasGroup canvasGroup;
        [SerializeField] protected TextMeshProUGUI infoOutput;
        [Space(15)]
        [SerializeField] protected bool useFadeAnimation;
        [SerializeField] private float fadeInDuration;
        [SerializeField] private float fadeOutDuration;
        [SerializeField, Range(0f, 1f)] protected float defaultAlpha;

        private CancellationTokenSource _source;

        public override UIElementType ViewType => UIElementType.HandView;

        private void OnDestroy()
        {
            Dispose();
        }


        //for unity events
        public void ShowActionCallback() => Show(this, null);

        public override void Show(object sender, object data)
        {
            base.Show(sender, data);

            Dispose();

            if (data is string str)
            {
                if(infoOutput.text != null)
                {
                    infoOutput.text = str;
                }
            }
            
            if (useFadeAnimation == true)
            {
                Fade();
            }
            else
            {
                canvasGroup.alpha = defaultAlpha;
            }
        }

        public override void Hide()
        {
            Dispose();
            
            _source = new CancellationTokenSource();

            FadeInAnimate(0f, fadeOutDuration, _source.Token, base.Hide).Forget();
        }

        public virtual void Dispose()
        {
            _source?.Cancel();
            _source?.Dispose();
            _source = null;
        }

        protected void Fade()
        {
            Dispose();

            _source = new CancellationTokenSource();
            canvasGroup.alpha = 0f;

            FadeInAnimate(defaultAlpha, fadeInDuration, _source.Token).Forget();
        }

        protected async UniTask FadeInAnimate(float needAlpha, float duration, CancellationToken token,
                                                                                        Action callback = null)
        {
            if (token.IsCancellationRequested == true) return;
            if (canvasGroup == null) return;

            while(true)
            {
                if (token.IsCancellationRequested == true) return;
                
                if(Mathf.Abs(canvasGroup.alpha - needAlpha) <= 0.01f == true) break;

                float step = duration * Time.deltaTime;

                if (needAlpha > canvasGroup.alpha)
                {
                    canvasGroup.alpha += step;
                }
                else
                {
                    canvasGroup.alpha -= step;
                }

                await UniTask.Yield();
            }

            callback?.Invoke();
        }
    }
}
