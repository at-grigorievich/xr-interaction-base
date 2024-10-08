using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ATG.MVC
{
    /// <summary>
    /// Устанавливает мин/макс углы для взаимодействия с обьектами типа "щипцы". 
    /// У которых есть вращающиеся элементы
    /// </summary>
    [Serializable]
    public sealed class ForcepsRotatedPart
    {
        [SerializeField] private Transform rotatedTarget;
        [SerializeField] private Vector3 rotateDirection;
        [SerializeField] private float minAngle;
        [SerializeField] private float maxAngle;

        public void UpdateAngle(float alpha)
        {
            float nextAngle = minAngle + (maxAngle - minAngle) * alpha;
            rotatedTarget.transform.localEulerAngles = rotateDirection * nextAngle;
        }
    }

    /// <summary>
    /// Базовый класс для обьектов типа "щипцы", с которыми можно взаимодействовать
    /// У которых есть вращающиеся элементы и эти элементы можно брать разными руками
    /// </summary>
    public abstract class ForcepsView : GrabView
    {
        [SerializeField] protected bool isInvertPressed; // Нужно ли инвертировать значение input device нажатия
        [SerializeField] protected InputActionReference leftHandTriggerInputAction;
        [SerializeField] protected InputActionReference rightHandTriggerInputAction;
        [Space(15)]
        [SerializeField] protected float needToPressInputMagnitude;
        [SerializeField] protected Collider pressCollider;
        [Space(15)]
        [SerializeField] protected ForcepsRotatedPart leftPart;
        [SerializeField] protected ForcepsRotatedPart rightPart;

        protected HandType _currentSelectHand = HandType.None;
        private InputActionReference _currentTriggerInputAction;
        
        public float PressedValue { get; private set; }

        private float _pressValueClamp = 1f;

        public bool IsPressed => pressCollider.enabled;

        public override void SetActive(bool isActive)
        {
            base.SetActive(isActive);

            ResetPressed();

            if (IsActive == true)
            {
                OnHandSelectEntered += OnHandSelectEnteredCallback;
                OnHandSelectExited += OnHandSelectExitedCallback;
            }
            else
            {
                OnHandSelectEntered -= OnHandSelectEnteredCallback;
                OnHandSelectExited -= OnHandSelectExitedCallback;
            }
        }
    
        protected virtual void OnHandSelectEnteredCallback(HandType handType)
        {
            _currentSelectHand = handType;

            _currentTriggerInputAction = _currentSelectHand switch
            {
                HandType.Left => leftHandTriggerInputAction,
                HandType.Right => rightHandTriggerInputAction,
                _ => null
            };
        }

        protected virtual void OnHandSelectExitedCallback(HandType type)
        {
            _currentSelectHand = HandType.None;
            _currentTriggerInputAction = null;
        }

        public virtual void UpdatePressed()
        {
            if (_currentSelectHand == HandType.None) return;

            PressedValue = _currentTriggerInputAction?.action.ReadValue<float>() ?? 0f;

            float angleValue = Mathf.Clamp(PressedValue, 0f, _pressValueClamp);
            angleValue = isActiveAndEnabled == false ? angleValue : 1f - angleValue;
            
            switch (_currentSelectHand)
            {
                case HandType.Left:
                    leftPart.UpdateAngle(angleValue);
                    break;
                case HandType.Right:
                    rightPart.UpdateAngle(angleValue);
                    break;
            }

            ChangePressColliderActive();
        }

        public virtual void ResetPressed()
        {
            PressedValue = 0f;

            leftPart.UpdateAngle(PressedValue);
            rightPart.UpdateAngle(PressedValue);

            UpdatePressedValueClamp(1f);
            ChangePressColliderActive();
        }

        /// <summary>
        /// Установить уровень для значения ввода input device
        /// </summary>
        /// <param name="nextClampValue"></param>
        public void UpdatePressedValueClamp(float nextClampValue)
        {
            _pressValueClamp = Mathf.Clamp01(nextClampValue);
        }

        private void ChangePressColliderActive()
        {
            pressCollider.enabled = PressedValue >= needToPressInputMagnitude;
        }
    }
}