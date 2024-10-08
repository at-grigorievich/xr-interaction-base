using DG.Tweening;
using UnityEngine;

namespace ATG.Arrow
{
    public sealed class ArrowAnimation: MonoBehaviour
    {   
        [SerializeField] private bool isActiveFromStart;
        [Space(5)]
        [SerializeField] private Renderer arrowRenderer;
        [SerializeField] private Transform arrowTransform;
        [Space(5)]
        [SerializeField] private Vector3 firstPosition;
        [SerializeField] private Vector3 secondPosition;
        [Space(5)]
        [SerializeField] private float duration;
        [SerializeField] private Ease ease;

        private Sequence _seq;

        private void Start()
        {
            if(isActiveFromStart == true)
            {
                StartAnimate();
            }
            else
            {
                StopAnimate();
            }
        }

        private void OnDestroy()
        {
            _seq?.Kill();
            _seq = null;
        }

        [ContextMenu("Test animate")]
        public void StartAnimate()
        {
            arrowTransform.localPosition = firstPosition;
            arrowRenderer.enabled = true;

            _seq?.Kill();
            _seq = DOTween.Sequence()
                .Append(arrowTransform.DOLocalMove(secondPosition, duration))
                .Append(arrowTransform.DOLocalMove(firstPosition, duration))
                .SetEase(ease)
                .SetLoops(-1);
        }

        public void StopAnimate()
        {
            _seq.Kill();
            _seq = null;

            arrowRenderer.enabled = false;
        }
    }
}