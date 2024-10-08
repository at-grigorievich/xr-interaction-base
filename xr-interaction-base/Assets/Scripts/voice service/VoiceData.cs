using ATG.Animator;

namespace ATG.Voice
{
    public readonly struct VoiceData
    {
        public VoiceType Voice { get;}
        public AnimatorEnum Animation {get;}
        public bool WithAnimation {get;}

        public VoiceData(VoiceType vType, AnimatorEnum animatorEnum, bool withAnimation)
        {
            Voice = vType;
            Animation = animatorEnum;

            WithAnimation = withAnimation;
        }
    }
}