using System;
using UnityEngine;

namespace ATG.MVC
{
    [Serializable]
    public sealed class HandActiveAmplitudeClampCreator
    {
        [SerializeField] private float defaultMinAmplitude;
        [SerializeField] private float defaultMaxAmplitude;

        public HandActiveAmplitudeClamp Create() => 
            new HandActiveAmplitudeClamp(defaultMinAmplitude, defaultMaxAmplitude);
    }


    /// <summary>
    /// Контролирует ключевые точки для анимации рук.
    /// Актуально, если анимации рук выполнены через SpeedTree, можно ограничивать значения.
    /// Например: Не полностью сжимать руку при взаимодействии с инструментами.
    /// </summary>
    public class HandActiveAmplitudeClamp
    {
        public readonly float DefaultMinAmplitude;
        public readonly float DefaultMaxAmplitude;

        public float CurrentMinAmplitude {get; private set; }
        public float CurrentMaxAmplitude { get; private set; }

        public HandActiveAmplitudeClamp(float defaultMin, float defaultMax)
        {
            DefaultMinAmplitude = defaultMin;
            DefaultMaxAmplitude = defaultMax;
        }

        public void SetToDefault()
        {
            SetMaxAmplitude(DefaultMaxAmplitude);
            SetMinAmplitude(DefaultMinAmplitude);
        }   

        public float GetClampedAmplutide(float value)
        {
            return Mathf.Clamp(value, CurrentMinAmplitude, CurrentMaxAmplitude);
        }

        public void SetMinAmplitude(float value)
        {
            CurrentMinAmplitude = value;
        }

        public void SetMaxAmplitude(float value)
        {
            CurrentMaxAmplitude = value;
        }
    }
}