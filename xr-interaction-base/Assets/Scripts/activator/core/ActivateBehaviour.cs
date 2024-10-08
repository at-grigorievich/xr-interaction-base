using UnityEngine;

namespace ATG.Activator
{
    public abstract class ActivateBehaviour : MonoBehaviour, IActivateable
    {
        public bool IsActive { get; private set; }
        
        public virtual void SetActive(bool isActive)
        {
            IsActive = isActive;
            
            if(gameObject != null)
                gameObject.SetActive(isActive);
        }
    }
}