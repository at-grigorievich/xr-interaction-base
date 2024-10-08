using UnityEngine;

namespace ATG.Moveable
{
    [CreateAssetMenu(menuName = "configs/new target-move config", fileName = "new_target_move_config")]
    public sealed class NavTargetMoveData : ScriptableObject
    {
        [field: SerializeField] public float BaseSpeed { get; private set; }
        [field: SerializeField] public float BaseRotation { get; private set; }
        [field: SerializeField] public float BaseAngularSpeed { get; private set; }
        [field: SerializeField] public float BaseAcceleration { get; private set; }
    }
}