using System;
using ATG.Activator;

namespace ATG.Voice
{
    public interface IVoiceService: IActivateable
    {
        event Action OnSoundComplete;

        void SetLoop(bool isLooping);

        void PlaySound(VoiceType type);

        void Update();

        void StopCurrentClip();
    }
}