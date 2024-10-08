using System;
using System.Collections.Generic;
using System.Linq;
using ATG.Animator;
using UnityEngine;

namespace ATG.Voice
{
    [CreateAssetMenu(menuName = "configs/new face voice service config", fileName = "new_face_voice_service_config")]
    public sealed class FaceVoiceData : ScriptableObject
    {
        [SerializeField] private AnimatorEnum idleFace;
        [SerializeField] private FaceAnimationByVoiceType[] faceAnimationByVoiceTypes;

        public AnimatorEnum IdleFace => idleFace;
        public IEnumerable<KeyValuePair<VoiceType, AnimatorEnum>> FacesByVoices =>
            faceAnimationByVoiceTypes.Select(foo => new KeyValuePair<VoiceType, AnimatorEnum>(foo.VoiceType, foo.AnimationType));

        [Serializable]
        private class FaceAnimationByVoiceType
        {
            [field: SerializeField] public VoiceType VoiceType { get; private set; }
            [field: SerializeField] public AnimatorEnum AnimationType { get; private set; }
        }
    }
}