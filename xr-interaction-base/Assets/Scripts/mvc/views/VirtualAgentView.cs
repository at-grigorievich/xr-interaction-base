using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.InputSystem.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

namespace ATG.MVC
{
    [RequireComponent(typeof(XROrigin), typeof(InputActionManager))]
    public sealed class VirtualAgentView : View
    {
        [SerializeField] private new Camera camera;
        [SerializeField] private TrackedPoseDriver trackedPoseDriver;

        public Transform CameraTransfrom => camera.transform;

        [field: SerializeField] public XRBaseController LeftController { get; private set; }
        [field: SerializeField] public XRBaseController RightController { get; private set; }

        public void PlaceToPoint(Vector3 position, Quaternion rotate)
        {
            transform.position = position;
            transform.rotation = rotate;
        }

        public void SetTrackerPoseOnlyRotation()
        {
            trackedPoseDriver.trackingType = TrackedPoseDriver.TrackingType.RotationOnly;
        }

        public void SetTrackerPosePositionAndRotation()
        {
            trackedPoseDriver.trackingType = TrackedPoseDriver.TrackingType.RotationAndPosition;
        }
    }
}