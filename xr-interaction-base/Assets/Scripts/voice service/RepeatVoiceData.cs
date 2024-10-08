using UnityEngine;

namespace ATG.Voice
{
    [CreateAssetMenu(menuName = "configs/new repeat voice service config", fileName = "new_repeat_voice_service_config")]
    public sealed class RepeatVoiceData : ScriptableObject
    {
        // Время (сек) после проигрыша аудиодорожки
        [field: SerializeField] public float DelayAfterLastRepeat { get; private set; }

        // Максимальная дистанция, чтобы воспроизвести повтор
        [field: SerializeField] public float MaxDistanceToSpeaker { get; private set; }

        // Время (сек) которое должен быть наведен игрок на триггер повтора звука
        // используется чтобы исключить случайный взгляд
        [field: SerializeField] public float MinDelayToReplay {get; private set;}
    }
}