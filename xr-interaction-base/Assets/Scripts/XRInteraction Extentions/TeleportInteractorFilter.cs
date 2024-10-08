using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Filtering;

[RequireComponent(typeof(XRInteractorLineVisual), typeof(XRRayInteractor))]
public class TeleportInteractorFilter : MonoBehaviour, IXRSelectFilter, IXRHoverFilter
{
    [SerializeField] private bool onlySingleRay = true;
    [SerializeField] private XRRayInteractor mirrorRayInteractor;

    private InputAction _selectTeleportAction;

    private XRInteractorLineVisual _lineVisual;

    public bool canProcess { get; private set; }

    private ActionBasedController _mirrorRayInteractorController;

    private void Awake()
    {
        XRRayInteractor rayInteractor = GetComponent<XRRayInteractor>();

        if (rayInteractor.xrController is ActionBasedController actionBasedController)
        {
            _selectTeleportAction = actionBasedController.selectAction.action;
        
            _lineVisual = GetComponent<XRInteractorLineVisual>();

            SetLineVisualActive(false);

            if(onlySingleRay == true && mirrorRayInteractor != null)
            {
                _mirrorRayInteractorController = mirrorRayInteractor.xrController as ActionBasedController;
            }
        }
        else throw new InvalidCastException("Teleport interactor must have ActionBasedController");
    }
    
    private void OnEnable()
    {
        _selectTeleportAction.performed += OnTeleportSetActive;
    }

    private void OnDisable()
    {
        _selectTeleportAction.performed -= OnTeleportSetActive;
    }

    private void OnDestroy()
    {
        _selectTeleportAction.performed -= OnTeleportSetActive;
    }

    private void OnTeleportSetActive(InputAction.CallbackContext context)
    {
        float value = context.ReadValue<float>();

        canProcess = value > 0f;

        SetLineVisualActive(canProcess);

        if(_mirrorRayInteractorController != null)
        {
            if(canProcess == true)
            {
                _mirrorRayInteractorController.selectAction.action.Disable();
            }
            else 
            {
                _mirrorRayInteractorController.selectAction.action.Enable();
            }
        }
    }

    public bool Process(IXRHoverInteractor interactor, IXRHoverInteractable interactable)
    {
        return canProcess;
    }

    public bool Process(IXRSelectInteractor interactor, IXRSelectInteractable interactable)
    {
        return canProcess;
    }

    private void SetLineVisualActive(bool isActive)
    {
        _lineVisual.enabled = isActive;
    }
}
