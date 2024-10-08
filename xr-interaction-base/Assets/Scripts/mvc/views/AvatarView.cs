using ATG.Voice;
using UnityEngine;

namespace ATG.MVC
{
    public class AvatarView : View
    {
        [SerializeField] private Renderer[] renderersSet;
        [Space(10)]
        [SerializeField] private RepeatTalkingHelper repeatTalkingHelper;

        public override void SetActive(bool isActive)
        {
            base.SetActive(isActive);

            foreach (var r in renderersSet)
            {
                if (r == null) continue;

                r.enabled = isActive;
            }
        }

        public void ActivateRepeatTalking()
        {
            if (repeatTalkingHelper == null) return;
            repeatTalkingHelper?.SetActive(true);
        }
        public void DeactivateRepeatTalking()
        {
            if (repeatTalkingHelper == null) return;
            repeatTalkingHelper?.SetActive(false);
        }
    }
}