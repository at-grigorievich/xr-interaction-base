using System;
using System.Collections.Generic;
using ATG.Animator;
using UnityEngine;

namespace ATG.Voice
{
    public class FaceVoiceService : IVoiceService
    {
        private readonly IVoiceService _voiceService;
        private readonly IAnimatorService _animatorService;

        private readonly IReadOnlyDictionary<VoiceType, AnimatorEnum> _animsByVoices;

        private readonly AnimatorEnum _idleFaceAnimationType;

        public bool IsActive => _voiceService.IsActive;

        public event Action OnSoundComplete
        {
            add => _voiceService.OnSoundComplete += value;
            remove => _voiceService.OnSoundComplete -= value;
        }

        public FaceVoiceService(IAnimatorService animatorService, IVoiceService baseVoiceService, FaceVoiceData faceConfig)
        {
            _animatorService = animatorService;
            _voiceService = baseVoiceService;

            _animsByVoices = new Dictionary<VoiceType, AnimatorEnum>(faceConfig.FacesByVoices);
            _idleFaceAnimationType = faceConfig.IdleFace;
        }

        public void PlaySound(VoiceType type)
        {
            AnimatorEnum anim = _animsByVoices.ContainsKey(type) == true ? _animsByVoices[type] : _idleFaceAnimationType;

            _animatorService.CrossFadeAnimate(anim);
            _voiceService.PlaySound(type);
        }

        public void SetActive(bool isActive) 
        {
            _voiceService.SetActive(isActive);

            if(isActive == false)
            {
                _animatorService.CrossFadeAnimate(_idleFaceAnimationType);
            }
        }

        public void SetLoop(bool isLooping) => _voiceService.SetLoop(isLooping);

        public void StopCurrentClip() 
        {
            _animatorService.CrossFadeAnimate(_idleFaceAnimationType);
            _voiceService.StopCurrentClip();
        }

        public void Update() => _voiceService.Update();
    }
}