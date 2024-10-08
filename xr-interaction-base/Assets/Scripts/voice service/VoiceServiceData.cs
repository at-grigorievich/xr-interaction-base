using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace ATG.Voice
{
    [CreateAssetMenu(menuName = "configs/new voice service config", fileName = "new_voice_service_config")]
    public sealed class VoiceServiceData: ScriptableObject
    {
        [SerializeField] private VoiceContainer[] voices;

        public IEnumerable<KeyValuePair<VoiceType, AudioClip>> Voices =>
            voices.Select(v => new KeyValuePair<VoiceType, AudioClip>(v.Type, v.Clip));

        [Serializable]
        private sealed class VoiceContainer
        {
            [field: SerializeField] public VoiceType Type {get; private set;}
            [field: SerializeField] public AudioClip Clip {get; private set;}
        }
    }
}