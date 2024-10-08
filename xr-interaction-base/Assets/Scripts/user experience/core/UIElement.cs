using ATG.Activator;
using UnityEngine;

namespace ATG.UI
{
    [RequireComponent(typeof(RectTransform))]
    public abstract class UIElement: ActivateBehaviour
    {
        public virtual void Show(object sender, object data)
        {
            SetActive(true);
        }

        public virtual void Hide()
        {
            SetActive(false);
        }
    }
}