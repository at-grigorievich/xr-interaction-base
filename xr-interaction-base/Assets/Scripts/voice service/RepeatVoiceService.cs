using System;
using ATG.MVC;
using ATG.StateMachine;
using UnityEngine;
using VContainer.Unity;

namespace ATG.Voice
{
    public sealed class RepeatVoiceService : IRepeatVoiceService, ITickable
    {
        private readonly RepeatVoiceData _config;

        private readonly AvatarController _avatarController;

        private readonly Statement _avatarTalkingState;

        private readonly Camera _camera;

        private readonly LayerMask _speakerMask;

        private VoiceData? _lastData;

        private float _currentDelay;
        private float _visionTimer;

        private Action _timerUpdater;
        private bool _allowRepeat;

        public bool IsActive { get; private set; }

        public RepeatVoiceService(AvatarController avatarController, RepeatVoiceData config)
        {
            _config = config;
            _avatarController = avatarController;
            _camera = Camera.main;

            _avatarTalkingState = _avatarController.StateSwitcher.GetState<CharacterTalkingState>();

            _speakerMask = LayerMask.GetMask(RepeatTalkingHelper.Layer);
        }

        public void SetActive(bool isActive)
        {
            IsActive = isActive;

            if (IsActive == false)
            {
                _lastData = null;

                UnsubscribeToCompleteTalking();
                Reset();
            }
        }

        public void Tick()
        {
            if (IsActive == false) return;

            _timerUpdater?.Invoke();

            if (_allowRepeat == true)
            {
                if(Physics.Raycast(_camera.transform.position, _camera.transform.forward, _config.MaxDistanceToSpeaker, _speakerMask) == true)
                {
                    _visionTimer += Time.deltaTime;

                    if(_visionTimer >= _config.MinDelayToReplay)
                    {
                        Replay();
                    }
                }
                else
                {
                    _visionTimer = 0f;
                }
            }
        }

        public void Replay()
        {
            if (IsActive == false) return;
            if (_lastData.HasValue == false) return;

            if (_allowRepeat == false) return;
            
            if(_avatarController.IsIdle == false) return;

            _avatarController.Talking(_lastData.Value);
            
            Reset();

            _avatarTalkingState.OnComplete += OnCompletedTalking;
        }

        
        public void Stop()
        {
            UnsubscribeToCompleteTalking();
            Reset();
        }

        public void UpdateLastVoice(VoiceData voiceData)
        {
            if (IsActive == false) return;

            _lastData = voiceData;

            StartTimer();
        }

        private void OnCompletedTalking()
        {
            _avatarController.Idle();
            StartTimer();
        }

        private void UnsubscribeToCompleteTalking()
        {
            _avatarTalkingState.OnComplete -= OnCompletedTalking;
        }

        private void Reset()
        {
            _allowRepeat = false;
            _currentDelay = 0f;
            _visionTimer = 0f;

            _timerUpdater = null;

            _avatarController.DeactivateRepeatTalking();
        }

        private void StartTimer()
        {
            Reset();

            UnsubscribeToCompleteTalking();
            _timerUpdater = UpdateTimer;
        }

        private void UpdateTimer()
        {
            if (_lastData.HasValue == false) return;

            _currentDelay += Time.deltaTime;

            bool isAllowRepeat = _currentDelay >= _config.DelayAfterLastRepeat;

            if (isAllowRepeat == true)
            {
                AllowRepeat();
            }
        }

        private void AllowRepeat()
        {
            _timerUpdater = null;
            _allowRepeat = true;

            _avatarController.ActivateRepeatTalking();
        }
    }
}