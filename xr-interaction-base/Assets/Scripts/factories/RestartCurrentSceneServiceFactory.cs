using System;
using ATG.SceneManagement;
using ATG.UI;
using UnityEngine;

namespace ATG.Factory
{
    [Serializable]
    public sealed class RestartCurrentSceneServiceFactory
    {
        [SerializeField] private RestartCurrentSceneView restartCurrentSceneView;

        public ISceneReloadeble Create(ISceneManagement sceneManagement)
        {
            return new ReloadSceneService(sceneManagement, restartCurrentSceneView);
        }
    }
}