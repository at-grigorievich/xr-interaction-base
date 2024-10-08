using ATG.Activator;

namespace ATG.Voice
{
    public interface IRepeatVoiceService: IActivateable
    {
        void UpdateLastVoice(VoiceData voiceData);

        void Replay();
        void Stop();
    }
}