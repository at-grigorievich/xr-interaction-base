using System;
using ATG.UI;

namespace ATG.SceneManagement
{
    public sealed class ReloadSceneService : ISceneReloadeble
    {
        private readonly ISceneManagement _sceneManagement;

        private readonly RestartCurrentSceneView _uiView;

        public bool IsActive { get; private set; }

        public ReloadSceneService(ISceneManagement sceneManagement, RestartCurrentSceneView uiView)
        {
            _uiView = uiView;

            _sceneManagement = sceneManagement;
        }

        public void SetActive(bool isActive)
        {
            IsActive = isActive;

            if (IsActive == true)
            {
                ActivateSceneReloading();
            }
            else
            {
                DeactivateSceneReloading();
            }
        }

        public void ReloadScene()
        {
            _sceneManagement.ReloadCurrentSceneAsync();
        }

        private void ActivateSceneReloading()
        {
            _uiView.Show(this, (Action)ReloadScene);
        }

        private void DeactivateSceneReloading()
        {
            _uiView.Hide();
        }
    }
}