using System;
using ATG.XRInteraction.Extensions;
using Unity.Burst.Intrinsics;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using VContainer.Unity;

namespace ATG.MVC
{
    /// <summary>
    /// vr player controller
    /// </summary>
    public sealed class VirtualAgentController : Controller<VirtualAgentView>, ITickable
    {
        private readonly HandController _leftHand;
        private readonly HandController _rightHand;
        
        public Transform CameraTransform => _view.CameraTransfrom;

        public XRBaseControllerInteractor LeftGrabInteractor => _leftHand.GetInteractorByType(InteractionType.Grab);
        public XRBaseControllerInteractor RightGrabInteractor => _rightHand.GetInteractorByType(InteractionType.Grab);

        public VirtualAgentController(HandController leftHand, HandController rightHand, VirtualAgentView view) : base(view)
        {
            _leftHand = leftHand;
            _rightHand = rightHand;
        }

        public override void SetActive(bool isActive)
        {
            if(_view == null) return;
            
            base.SetActive(isActive);
            
            _leftHand.SetActive(isActive);
            _rightHand.SetActive(isActive);
        }

        public void Tick()
        {
            if (IsActive == false) return;
            _leftHand.Update();
            _rightHand.Update();
        }

        public void SetupToXR()
        {
            _leftHand.PlaceToXR();
            _rightHand.PlaceToXR();
        }

        public void SetInteractorActive(HandType handType, InteractionType interactionType, bool isActive) =>
            GetHandByType(handType).SetInteractorActiveByType(interactionType, isActive);
        
        public void PlaceToPoint(Vector3 position, Quaternion rotation) => _view.PlaceToPoint(position, rotation);

        #region Tutorial info
        public void ShowTutorialInfo(in string info)
        {
            _leftHand.ShowTutorialInfo(info);
            _rightHand.ShowTutorialInfo(info);
        }
        public void HideTutorialInfo()
        {
            _leftHand.HideTutorialInfo();
            _rightHand.HideTutorialInfo();
        }
        #endregion

        #region Progress info
        public void ShowOnlyInfo(in string info, in Vector3 defaultPosition, in Vector3 defaultRotation)
        {
            _leftHand.ShowOnlyInfo(info, defaultPosition, defaultRotation);
            _rightHand.ShowOnlyInfo(info, defaultPosition, defaultRotation);
        }
    
        public void ShowRateInfo(in string info, in float defaultRate, in Vector3 defaultPosition, in Vector3 defaultRotation)
        {
            _leftHand.ShowRateInfo(info, defaultRate, defaultPosition, defaultRotation);
            _rightHand.ShowRateInfo(info, defaultRate, defaultPosition, defaultRotation);
        }

        public void ShowRateInfo(in string info, in float defaultRate)
        {
            _leftHand.ShowRateInfo(info, defaultRate);
            _rightHand.ShowRateInfo(info, defaultRate);
        }

        public void HideRateInfo()
        {
            _leftHand.HideRateInfo();
            _rightHand.HideRateInfo();
        }

        public void UpdateRateInfo(in float nextRate)
        {
            _leftHand.UpdateRateInfo(nextRate);
            _rightHand.UpdateRateInfo(nextRate);
        }
        #endregion
        
        public HandController GetHandByType(HandType handType) => handType switch
        {
            HandType.Left => _leftHand,
            HandType.Right => _rightHand,
            _ => throw new NullReferenceException("hand type is none!")
        };

        public void SetTrackerPoseOnlyRotation() => _view.SetTrackerPoseOnlyRotation();

        public void SetTrackerPosePositionAndRotation() => _view.SetTrackerPosePositionAndRotation();
    }
}