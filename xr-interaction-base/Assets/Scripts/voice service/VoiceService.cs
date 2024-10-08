using System;
using System.Collections.Generic;
using UnityEngine;

namespace ATG.Voice
{
    public sealed class VoiceService : IVoiceService
    {
        private readonly VoiceServiceData _config;
        private readonly AudioSource _audioSource;

        private readonly IReadOnlyDictionary<VoiceType, AudioClip> _sources;

        private Action _update;
        public event Action OnSoundComplete;

        public bool IsActive { get; private set; }

        public VoiceService(AudioSource source, VoiceServiceData config)
        {
            _audioSource = source;
            _config = config;

            _sources = new Dictionary<VoiceType, AudioClip>(_config.Voices);
        }

        public void SetActive(bool isActive)
        {
            IsActive = isActive;

            if (_audioSource != null)
            {
                _audioSource.enabled = isActive;

                StopCurrentClip();
                _audioSource.clip = null;
            }
        }

        public void PlaySound(VoiceType type)
        {
            if (_sources.ContainsKey(type) == false)
            {
                throw new NullReferenceException($"{type} audio clip not exist !");
            }

            PlaySound(_sources[type]);
        }

        public void Update()
        {
            _update?.Invoke();
        }
    
        public void SetLoop(bool isLooping)
        {
            _audioSource.loop = isLooping;
        }

        public void StopCurrentClip()
        {
            _update = null;

            _audioSource.Stop();
            _audioSource.clip = null;
        }

        private void PlaySound(AudioClip clip)
        {
            StopCurrentClip();

            _audioSource.clip = clip;
            _audioSource.Play();

            _update = WaitToComplete;
        }

        private void WaitToComplete()
        {
            if(_audioSource.isPlaying == true) return;
            
            StopCurrentClip();
            OnSoundComplete?.Invoke();
        }
    }
}