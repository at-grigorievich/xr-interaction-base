using System;
using ATG.Admin;
using ATG.SceneManagement;
using UnityEngine;
using VContainer.Unity;

namespace ATG.EntryPoint
{
    public sealed class InitialSceneEntryPoint: IStartable
    {
        private readonly SceneInfoData _nextSceneInfo;
        private readonly ISceneManagement _sceneManagement;

        private readonly IAdministrateable _adminService;

        public InitialSceneEntryPoint(ISceneManagement sceneManagement, SceneInfoData nextSceneInfo,
            IAdministrateable adminService)
        {
            _sceneManagement = sceneManagement;
            _adminService = adminService;

            _nextSceneInfo = nextSceneInfo;
        }

        public void Start()
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;

            if(_nextSceneInfo != null)
            {
                _sceneManagement.LoadBySceneInfoAdditiveAsync(_nextSceneInfo);
            }
            else throw new NullReferenceException("next loadeded scene is null !");

            _adminService.SetActive(true);
        }

        public void Dispose()
        {
            _adminService.SetActive(false);
        }
    }
}