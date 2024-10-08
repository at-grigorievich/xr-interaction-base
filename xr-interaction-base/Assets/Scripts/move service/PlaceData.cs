using ATG.Animator;
using UnityEngine;

namespace ATG.Moveable
{
    public readonly struct PlaceData
    {
        public AnimatorEnum AnimationType { get; }
        public Transform Placement { get; }

        public PlaceData(Transform placement, AnimatorEnum animationType)
        {
            Placement = placement;
            AnimationType = animationType;
        }

        public static PlaceData IdleWait => new PlaceData(null, AnimatorEnum.Idle);
    }
}