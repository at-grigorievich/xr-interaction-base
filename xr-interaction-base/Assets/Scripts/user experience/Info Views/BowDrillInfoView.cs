using DG.Tweening;
using UnityEngine;

namespace ATG.UI
{
    public sealed class BowDrillInfoView: InfoView
    {
        [SerializeField] private RectTransform handRect;
        [Space(10)]
        [SerializeField] private float drillDuration;
        [SerializeField] private Ease drillEase;
        [SerializeField] private Vector2 leftPos;
        [SerializeField] private Vector2 rightPos;
        [SerializeField] private Vector2 defaultPos;

        private Sequence _seq;


        public override void Show(object sender, object data)
        {
            base.Show(sender, data);
            
            handRect.anchoredPosition = defaultPos;

            _seq = DOTween.Sequence()
                .Append(handRect.DOAnchorPos(leftPos, drillDuration))
                .Append(handRect.DOAnchorPos(defaultPos, drillDuration))
                .Append(handRect.DOAnchorPos(rightPos, drillDuration))
                .Append(handRect.DOAnchorPos(defaultPos, drillDuration))
                .SetEase(drillEase).SetLoops(-1);

            _seq.Play();
        }

        public override void Hide()
        {
            base.Hide();
            
            _seq?.Kill();
            _seq = null;
        }

        public override void Dispose()
        {
            base.Dispose();
        }


    }
}