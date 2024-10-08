using ATG.Animator;
using ATG.StateMachine;
using ATG.XRInteraction.Extensions;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

using SM = ATG.StateMachine.StateMachine;

namespace ATG.MVC
{
    public enum HandType : byte
    {
        None = 0,
        Left = 1,
        Right = 2
    }
    
    /// <summary>
    /// vr hand controller
    /// </summary>
    public sealed class HandController : Controller<HandView>, IInteractorsHolder
    {
        private readonly SM _sm;
        private readonly XRBaseController _xrController;

        private readonly IAnimatorService _animatorService;
        private readonly InteractorProviderSet _interactorSet;

        public readonly HandActiveAmplitudeClamp GripAmplitudeClamp;
        public readonly HandActiveAmplitudeClamp TriggerAmplitudeClamp;
        
        public HandController(SM sm, XRBaseController xrController, 
            IAnimatorService animatorService, 
            HandActiveAmplitudeClamp gripAmlitudeClamp, HandActiveAmplitudeClamp triggerAmplitudeClamp,
             HandView view) : base(view)
        {
            _sm = sm;

            _xrController = xrController;
            _interactorSet = view.InteractorsSet;

            _animatorService = animatorService;

            GripAmplitudeClamp = gripAmlitudeClamp;
            TriggerAmplitudeClamp = triggerAmplitudeClamp;
        }

        public override void SetActive(bool isActive)
        {
            base.SetActive(isActive);

            _animatorService.SetActive(isActive);

            TriggerAmplitudeClamp.SetToDefault();
            GripAmplitudeClamp.SetToDefault();

            if (isActive == true)
            {
                _sm.StartOrContinueMachine();
                _sm.SwitchState<HandActiveState>();
            }
            else
            {
                SetActiveAllInteractors(false);
                _sm.PauseMachine();
            }
        }

        public void Update()
        {
            if (IsActive == false) return;
            _sm.ExecuteMachine();
        }

        public void PlaceToXR()
        {
            SetParent(_xrController.modelParent, true);
            _xrController.model = _view.transform;
        }

        #region IInteractorsHolder implementation / work with interactors
        public bool GetInteractorActiveselfByType(InteractionType type) =>
            _interactorSet.GetInteractorActiveselfByType(type);

        public void SetInteractorActiveByType(InteractionType type, bool isActive) =>
            _interactorSet.SetInteractorActiveByType(type, isActive);

        public void SetActiveAllInteractors(bool isActive) =>
            _interactorSet.SetActiveAllInteractors(isActive);

        public XRBaseControllerInteractor GetInteractorByType(InteractionType type) =>
            _interactorSet.GetInteractorByType(type);
        #endregion
        #region Tutorial info
        public void ShowTutorialInfo(in string info) => _view.ShowTutorialInfo(info);
        public void HideTutorialInfo() => _view.HideTutorialInfo();
        #endregion
        #region Rate info
        public void ShowOnlyInfo(in string info, in Vector3 defaultPosition, in Vector3 defaultRotation) => 
            _view.ShowOnlyInfo(info, defaultPosition, defaultRotation);
        
        public void ShowRateInfo(in string info, in float defaultRate, in Vector3 defaultPosition, in Vector3 defaultRotation) =>
            _view.ShowRateInfo(info, defaultRate, defaultPosition, defaultRotation);
        
        public void ShowRateInfo(in string info, in float defaultRate) =>
            _view.ShowRateInfo(info, defaultRate);

        public void HideRateInfo() => _view.HideInfo();
        
        public void UpdateRateInfo(in float nextRate) => _view.UpdateRateInfo(nextRate);
        #endregion
    }
}