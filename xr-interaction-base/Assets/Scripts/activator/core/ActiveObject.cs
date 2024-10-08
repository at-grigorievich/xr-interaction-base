namespace ATG.Activator
{
    public abstract class ActivateObject: IActivateable
    {
        public bool IsActive { get; set; }

        public virtual void SetActive(bool isActive)
        {
            IsActive = isActive;
        }

        public virtual void Dispose()
        {
            SetActive(false);
        }
    }
}