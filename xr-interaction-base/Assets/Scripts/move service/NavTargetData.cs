using UnityEngine;

namespace ATG.Moveable
{
    public readonly struct NavTargetData
    {
        public Transform Target {get;}
        public float StopDistance {get;}

        public NavTargetData(Transform target, float stopDistance)
        {
            Target = target;
            StopDistance = stopDistance;
        }
    }
}