using UnityEngine.InputSystem;

namespace ATG.DTO
{
    public readonly struct HandActionsDTO
    {
        public InputActionReference GripAction { get; }
        public InputActionReference TriggerAction { get; }

        public HandActionsDTO(InputActionReference gripAction, InputActionReference triggerAction)
        {
            GripAction = gripAction;
            TriggerAction = triggerAction;
        }
    }
}