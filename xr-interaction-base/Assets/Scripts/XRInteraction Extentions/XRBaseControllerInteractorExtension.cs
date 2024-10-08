using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

namespace ATG.XRInteraction.Extensions
{
    public static class XRBaseControllerInteractorExtension
    {
        public static XRNode? GetHandNode(XRBaseControllerInteractor leftHand,
            XRBaseControllerInteractor rightHand,  Transform target)
        {
            if(leftHand.IsInteractedWith(target) == true) return XRNode.LeftHand;
            if(rightHand.IsInteractedWith(target) == true) return XRNode.RightHand;
            return null;
        }

        public static bool IsInteractedWith(this XRBaseControllerInteractor interactor, Transform target)
        {
            return ReferenceEquals(interactor.GetOldestInteractableSelected()?.transform ?? null,target);
        }

    }
}