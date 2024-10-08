using System;
using UnityEngine;

namespace ATG.Animator
{
    [Serializable]
    public sealed class AnimatorServiceFactory
    {
        [SerializeField, Header("Animator Config Link")]
        private AnimateableServiceData defaultConfig;
        [SerializeField, Header("Animator Link")]
        private UnityEngine.Animator animator;
        [SerializeField, Header("Rig Root Transform")]
        private Transform rigRoot;
        [SerializeField, Header("Animation Callback Receiver")]
        private AnimatorCallbackService animatorCallbackService;

        public IAnimatorService Create()
        {
            return new AnimatorService(animator, rigRoot, animatorCallbackService, defaultConfig);
        }
    }
}
