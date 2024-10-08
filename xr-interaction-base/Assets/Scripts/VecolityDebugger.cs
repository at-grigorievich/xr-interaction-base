using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class VelocityDebugger: MonoBehaviour
{
    public InputActionReference rightHand;
    public float limit = 0.1f;

    private float lastX;

    public void Update()
    {
        Vector3 vel;
        InputDevices.GetDeviceAtXRNode(XRNode.RightHand).TryGetFeatureValue(UnityEngine.XR.CommonUsages.deviceVelocity, out vel);

        /*Debug.Log(Mathf.Abs(vel.x) > limit);

        lastX = vel.x;*/

        Debug.DrawRay(transform.position, -Vector3.Project(vel, transform.forward), Color.red);
    }
}