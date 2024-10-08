using System;
using ATG.Activator;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public sealed class CollisionObject : MonoBehaviour, IActivateable
{
    [SerializeField] private bool disableOnAwake = true;

    public bool IsActive { get; private set; }

    public event Action<GameObject> OnCollisionEntered;
    public event Action<GameObject> OnCollisionExited;
    public event Action<GameObject> OnCollisionStaying;

    private void Awake()
    {
        SetActive(!disableOnAwake);
    }

    public void SetActive(bool isActive)
    {
        IsActive = isActive;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.rigidbody != null)
        {
            OnCollisionEntered?.Invoke(collision.rigidbody.gameObject);
            return;
        }
        OnCollisionEntered?.Invoke(collision.gameObject);
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.rigidbody != null)
        {
            OnCollisionStaying?.Invoke(collision.rigidbody.gameObject);
            return;
        }
        OnCollisionStaying?.Invoke(collision.gameObject);
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.rigidbody != null)
        {
            OnCollisionExited?.Invoke(collision.rigidbody.gameObject);
            return;
        }
        OnCollisionExited?.Invoke(collision.gameObject);
    }
}