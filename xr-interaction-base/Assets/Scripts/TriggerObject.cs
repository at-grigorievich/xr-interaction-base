using System;
using ATG.Activator;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public sealed class GrabTriggerObject : IActivateable
{
    private readonly TriggerObject _trigger;
    private readonly Renderer _triggerVisualization;

    private readonly XRGrabInteractable _interactable;

    public event Action<GameObject> OnTriggerEntered
    {
        add => _trigger.OnTriggerEntered += value;
        remove => _trigger.OnTriggerEntered -= value;
    }

    public event Action<GameObject> OnTriggerExited
    {
        add => _trigger.OnTriggerExited += value;
        remove => _trigger.OnTriggerExited -= value;
    }

    public event Action<GameObject> OnTriggerStaying
    {
        add => _trigger.OnTriggerStaying += value;
        remove => _trigger.OnTriggerStaying -= value;
    }

    public bool IsActive { get; private set; }

    public GrabTriggerObject(TriggerObject trigger, Renderer visual,
        XRGrabInteractable interactable)
    {
        _trigger = trigger;
        _triggerVisualization = visual;

        _interactable = interactable;
    }

    public void SetActive(bool isActive)
    {
        IsActive = isActive;

        if (IsActive == true)
        {
            _interactable.firstSelectEntered.AddListener(OnFirstSelected);
            _interactable.lastSelectExited.AddListener(OnLastExited);

            if (_interactable.isSelected == true)
            {
                OnFirstSelected(null);
            }
            else
            {
                OnLastExited(null);
            }
        }
        else
        {
            _interactable.firstSelectEntered.RemoveListener(OnFirstSelected);
            _interactable.lastSelectExited.RemoveListener(OnLastExited);

            _trigger.SetActive(false);
            _triggerVisualization.enabled = false;
        }
    }

    private void OnFirstSelected(SelectEnterEventArgs _)
    {
        _trigger.SetActive(true);
        _triggerVisualization.enabled = true;
    }

    private void OnLastExited(SelectExitEventArgs _)
    {
        _trigger.SetActive(false);
        _triggerVisualization.enabled = false;
    }
}

[RequireComponent(typeof(Collider))]
public sealed class TriggerObject : MonoBehaviour, IActivateable
{
    [SerializeField] private bool disableOnAwake = true;

    private Collider _collider;

    public bool IsActive { get; private set; }

    public event Action<GameObject> OnTriggerEntered;
    public event Action<GameObject> OnTriggerExited;
    public event Action<GameObject> OnTriggerStaying;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;

        SetActive(!disableOnAwake);
    }

    public void SetActive(bool isActive)
    {
        if (_collider != null)
        {
            _collider.enabled = isActive;
        }

        IsActive = isActive;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody != null)
            OnTriggerEntered?.Invoke(other.attachedRigidbody.gameObject);

        OnTriggerEntered?.Invoke(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.attachedRigidbody != null)
            OnTriggerExited?.Invoke(other.attachedRigidbody.gameObject);

        OnTriggerExited?.Invoke(other.gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.attachedRigidbody != null)
            OnTriggerStaying?.Invoke(other.attachedRigidbody.gameObject);

        OnTriggerStaying?.Invoke(other.gameObject);
    }
}