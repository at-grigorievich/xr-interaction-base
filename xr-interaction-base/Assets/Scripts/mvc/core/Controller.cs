using ATG.Activator;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ATG.MVC
{
    public abstract class Controller<T> : ActivateObject where T : View
    {
        protected T _view;

        public Transform ViewTransform => _view.transform;

        public Controller(T view)
        {
            _view = view;
        }

        public override void SetActive(bool isActive)
        {
            base.SetActive(isActive);

            if(_view != null)
            {
                _view.SetActive(isActive);
            }
        }

        public void SetScene(Scene scene) => _view.SetScene(scene);

        public void SetParent(Transform parent, bool resetToZero) =>
                _view.SetParent(parent, resetToZero);

        public void SetParent(Transform parent, Vector3 localPosition, Quaternion localRotation) =>
                _view.SetParent(parent,localPosition, localRotation);

        public void SetLayer(int layerIndex) => _view.SetLayer(layerIndex);

    }
}