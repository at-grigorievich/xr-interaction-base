using DG.Tweening;
using UnityEngine;

namespace ATG.MVC
{
    [CreateAssetMenu(menuName = "configs/new move-to-target config", fileName = "move_to_target_config")]
    public sealed class MoveViewToTargetData : ScriptableObject
    {
        [field: SerializeField] public float ReturnPositionDuration { get; private set; }
        [field: SerializeField] public float ReturnRotationDuration { get; private set; }
        [field: Space(5)]
        [field: SerializeField] public Ease ReturnPositionEase { get; private set; }
        [field: SerializeField] public Ease ReturnRotationEase { get; private set; }
    }
}