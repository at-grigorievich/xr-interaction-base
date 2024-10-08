using ATG.Activator;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ATG.MVC
{
    public abstract class View : ActivateBehaviour
    {
        public void SetParent(Transform parent, bool resetToZero)
        {
            transform.SetParent(parent);

            if (resetToZero == false) return;
        
            transform.localRotation = Quaternion.identity;
            transform.localPosition = Vector3.zero;
        }

        public void SetScene(Scene scene) => SceneManager.MoveGameObjectToScene(gameObject, scene);

        public void SetParent(Transform parent, Vector3 localPosition, Quaternion localRotation)
        {
            transform.SetParent(parent);
            transform.localPosition = localPosition;
            transform.localRotation = localRotation;
        }

        public void SetLayer(int layerIndex)
        {
            gameObject.layer = layerIndex;
        }
    }
}