using UnityEngine;

namespace ATG.UI
{
    [RequireComponent(typeof(Canvas))]
    public abstract class UIView : UIElement
    {
        private Canvas _canvas;

        public abstract UIElementType ViewType { get; }

        protected void Awake()
        {
            _canvas = GetComponent<Canvas>();
        }

        public override void SetActive(bool isActive)
        {
            base.SetActive(isActive);
            
            _canvas.enabled = isActive;
        }

        public void ChangeLayerIndex(int index) => _canvas.sortingLayerID = index;
    }
}