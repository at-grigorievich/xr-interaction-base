using System;

namespace ATG.Activator
{
    public interface IActivateable
    {
        bool IsActive {get;}
        void SetActive(bool isActive);
    }
}