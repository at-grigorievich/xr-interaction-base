using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ATG.UI
{
    [RequireComponent(typeof(RectTransform))]
    public sealed class ScaleClickButton: UIElement, IDisposable, IPointerDownHandler, IPointerUpHandler 
    {   
        [SerializeField] private TextMeshProUGUI buttonTextOutput;
        [Space(10)]
        [SerializeField] private float scaleDuration;
        [SerializeField] private Ease scaleEase;
        [Space(5)]
        [SerializeField] private Vector3 defaultScale;
        [SerializeField] private Vector3 clickScale;
    
        private RectTransform _rect;

        private Tween _tween;

        public event Action OnClick;

        private void Awake()
        {
            _rect = GetComponent<RectTransform>();
        }

        [ContextMenu("Fake click")]
        public void FakeClick()
        {
            OnClick?.Invoke();
        }

        public override void Show(object sender, object data)
        {
            base.Show(sender, data);

            OnClick = null;
            
            if(data is Action clickCallback)
            {
                OnClick += clickCallback;
            }
        }

        public override void Hide()
        {
            OnClick = null;
            
            Dispose();
            base.Hide();
        }   

        public void Dispose()
        {
            _tween?.Kill();

            if(_rect == null) return;
            _rect.localScale = defaultScale;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if(IsActive == false) return;

            _tween?.Kill();
            _tween = _rect.DOScale(clickScale, scaleDuration).SetEase(scaleEase);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if(IsActive == false) return;

            _tween?.Kill();
            _tween = _rect.DOScale(defaultScale, scaleDuration).SetEase(scaleEase);

            OnClick?.Invoke();
        }

        public void UpdateText(string newText)
        {
            buttonTextOutput.text = newText;
        }
    }
}