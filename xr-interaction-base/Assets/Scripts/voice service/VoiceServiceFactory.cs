using System;
using ATG.Animator;
using UnityEngine;

namespace ATG.Voice
{
    [Serializable]
    public sealed class FaceVoiceServiceFactory : VoiceServiceFactory
    {
        [SerializeField] private FaceVoiceData faceVoiceConfig;

        public IVoiceService Create(IAnimatorService animatorService)
        {
            var basic = base.Create();

            return new FaceVoiceService(animatorService, basic, faceVoiceConfig);
        }
    }

    [Serializable]
    public class VoiceServiceFactory
    {
        [SerializeField] protected VoiceServiceData config;
        [SerializeField] protected AudioSource source;

        public IVoiceService Create()
        {
            return new VoiceService(source, config);
        }
    }
}