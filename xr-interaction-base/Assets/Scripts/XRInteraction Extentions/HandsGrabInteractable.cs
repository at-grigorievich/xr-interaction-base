using System;
using ATG.MVC;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class HandsGrabInteractable : XRGrabInteractable
{
    public Transform leftHandAttachTransform;
    public Transform rightHandAttachTransform;
    
    public event Action<HandType> OnHandSelectEntered;
    public event Action<HandType> OnHandSelectExited;

    public event Action<HandType> OnHandHoverEntered;
    public event Action<HandType> OnHandHoverExited;

    protected override void OnSelectEntering(SelectEnterEventArgs args)
    {
        var handType = GetHandType(args.interactorObject.transform);

        switch(handType)
        {
            case HandType.Left:
                OnHandSelectEntered?.Invoke(handType);
                attachTransform = leftHandAttachTransform;
                break;
            case HandType.Right:
                OnHandSelectEntered?.Invoke(handType);
                attachTransform = rightHandAttachTransform;
                break;
        }

        base.OnSelectEntering(args);
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        var handType = GetHandType(args.interactorObject.transform);

        switch(handType)
        {
            case HandType.Left:
                OnHandSelectExited?.Invoke(handType);
                attachTransform = leftHandAttachTransform;
                break;
            case HandType.Right:
                OnHandSelectExited?.Invoke(handType);
                attachTransform = rightHandAttachTransform;
                break;
        }

        base.OnSelectExited(args);
    }

    protected override void OnHoverEntered(HoverEnterEventArgs args)
    {
        var handType = GetHandType(args.interactorObject.transform);

        if(handType != HandType.None)
        {
            OnHandHoverEntered?.Invoke(handType);
        }

        base.OnHoverEntered(args);
    }

    protected override void OnHoverExited(HoverExitEventArgs args)
    {
        var handType = GetHandType(args.interactorObject.transform);

        if(handType != HandType.None)
        {
            OnHandHoverExited?.Invoke(handType);
        }

        base.OnHoverExited(args);
    }

    private HandType GetHandType(Transform transform)
    {
        if(transform.CompareTag("LeftHand") == true)
        {
            return HandType.Left;
        }
        else if(transform.CompareTag("RightHand"))
        {
            return HandType.Right;
        }
        return HandType.None;
    }
}