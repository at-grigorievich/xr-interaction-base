using System;
using DG.Tweening;
using UnityEngine;

namespace ATG.MVC.Helpers
{
    [Serializable]
    public sealed class MoveViewToTargetContainer
    {
        [SerializeField] private View view;
        [Space(5)]
        [SerializeField] private MoveViewToTargetData config;

        public MoveViewToTarget Create()
        {
            return new
            (
                view: view,
                defaultPosition: view.transform.position,
                defaultRotation: view.transform.eulerAngles,
                config: config
            );
        }
    }

    public sealed class MoveViewToTarget : IDisposable
    {
        private readonly View _view;

        private Vector3 _defaultPosition;
        private Vector3 _defaultRotation;

        private readonly MoveViewToTargetData _config;

        private Sequence _seq;

        public MoveViewToTarget(View view,
                                Vector3 defaultPosition, Vector3 defaultRotation,
                                MoveViewToTargetData config)
        {
            _view = view;

            _defaultPosition = defaultPosition;
            _defaultRotation = defaultRotation;

            _config = config;
        }

        public void ResetToDefault(bool withDelay = true, Action callback = null)
        {
            Execute(_defaultPosition, Quaternion.Euler(_defaultRotation), withDelay: withDelay, callback: callback);
        }

        public void ChangeDefaultPoint(Vector3 newPosition, Vector3 newRotation)
        {
            _defaultPosition = newPosition;
            _defaultRotation = newRotation;
        }

        public void Execute(Vector3 position, Quaternion rotation, bool withDelay, 
            Action callback = null, 
            bool isLocalSpace = false)
        {
            Dispose();

            float delay = withDelay ? 0.6f : 0f;

            if (isLocalSpace == false)
            {
                _seq = DOTween.Sequence()
                    .AppendInterval(delay)
                    .Append(_view.transform.DOMove(position, _config.ReturnPositionDuration).SetEase(_config.ReturnPositionEase))
                    .Join(_view.transform.DORotate(rotation.eulerAngles, _config.ReturnRotationDuration).SetEase(_config.ReturnRotationEase))
                    .OnComplete(() => 
                    {
                        callback?.Invoke();
                        Dispose();
                    });
            }
            else
            {
                _seq = DOTween.Sequence()
                    .AppendInterval(delay)
                    .Append(_view.transform.DOLocalMove(position, _config.ReturnPositionDuration).SetEase(_config.ReturnPositionEase))
                    .Join(_view.transform.DOLocalRotate(rotation.eulerAngles, _config.ReturnRotationDuration).SetEase(_config.ReturnRotationEase))
                    .OnComplete(() => 
                    {
                        callback?.Invoke();
                        Dispose();
                    });
            }
        }

        public void Dispose()
        {
            _seq?.Kill();
            _seq = null;
        }
    }
}