using System;
using ATG.MVC;
using ATG.Scenario;
using ATG.SceneManagement;
using ATG.Voice;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace ATG.EntryPoint
{
    public class PlayeableSceneEntryPoint : IStartable, IDisposable, ICompleteable
    {
        protected readonly Material _skybox;

        protected readonly VirtualAgentController _agentController;
        protected readonly AvatarController _escortAvatarController;

        protected readonly IScenarioService _scenarioService;
        protected readonly ISceneManagement _sceneManagement;

        [Inject] protected IRepeatVoiceService _repeatVoiceService;

        protected readonly SceneInfoData _nextSceneInfo;
        protected readonly SceneInfoData _initialSceneInfo;

        public PlayeableSceneEntryPoint(VirtualAgentController agentController, AvatarController escortAvatarController,
            IScenarioService scenarioService, ISceneManagement sceneManagement, SceneInfoData nextSceneInfo,
            SceneInfoData initialSceneInfo, Material skybox)
        {
            _agentController = agentController;
            _escortAvatarController = escortAvatarController;
            _scenarioService = scenarioService;
            _sceneManagement = sceneManagement;

            _skybox = skybox;

            _nextSceneInfo = nextSceneInfo;
            _initialSceneInfo = initialSceneInfo;
        }

        public virtual void Start()
        {
            RenderSettings.skybox = _skybox;

            _escortAvatarController.SetScene(SceneManager.GetActiveScene());
            _agentController.SetScene(SceneManager.GetActiveScene());

            _escortAvatarController.SetActive(true);
            _agentController.SetActive(true);

            _repeatVoiceService.SetActive(true);

            _scenarioService.StartOrContinue();
        }

        public virtual void Dispose()
        {
            _scenarioService.Pause();

            _agentController.SetActive(false);
            _escortAvatarController.SetActive(false);

            _repeatVoiceService.SetActive(false);
        }

        public virtual void Complete()
        {
            Dispose();
            
            _sceneManagement.LoadBySceneInfoAdditiveAndUnloadLastAsync(_nextSceneInfo);
        }
    }
}