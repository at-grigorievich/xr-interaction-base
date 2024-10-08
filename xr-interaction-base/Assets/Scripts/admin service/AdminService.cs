using ATG.Callibration;
using ATG.Factory;
using ATG.Quiz;
using ATG.SceneManagement;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ATG.Admin
{
    /// <summary>
    /// Фасад для работы с сервисами: Каллибровка высоты, Перемещение сразу к квизу, Перезагрузка текущей сцены
    /// + контроллирует UI отображение
    /// </summary>
    public class AdminService : IAdministrateable
    {
        private readonly InputActionReference _adminEnteredInput;

        private readonly Canvas _adminCanvas;

        private readonly ICallibrationService _callibrationService;
        private readonly ISceneReloadeble _sceneReloadService;
        private readonly IQuizTeleporteable _quizTeleportService;

        public bool IsActive {get; private set;}
        
        private bool _lastVisible;

        public AdminService(CallibrationServiceFactory callibrationServiceFactory, 
                            RestartCurrentSceneServiceFactory sceneReloadebleFactory, 
                            TeleportToQuizServiceFactory quizTeleporteableFactory, 
                            Canvas adminCanvas,
                            InputActionReference adminEnteredInput,
                            ISceneManagement sceneManagement)
        {
            _callibrationService = callibrationServiceFactory.Create();
            _sceneReloadService = sceneReloadebleFactory.Create(sceneManagement);
            _quizTeleportService = quizTeleporteableFactory.Create(sceneManagement);

            _adminEnteredInput = adminEnteredInput;

            _adminCanvas = adminCanvas;
        }

        public void SetActive(bool isActive)
        {
            IsActive = isActive;

            _lastVisible = false;
            _adminCanvas.enabled = false;

            _callibrationService.SetActive(false);
            _sceneReloadService.SetActive(false);
            _quizTeleportService.SetActive(false);
            
            if(IsActive == true)
            {
                _adminEnteredInput.action.performed += OnActivate;
            }
            else
            {
                _adminEnteredInput.action.performed -= OnActivate;
            }
        }

        private void OnActivate(InputAction.CallbackContext context)
        {
            if(_lastVisible == false)
            {
                 _adminCanvas.enabled = true;

                _callibrationService.SetActive(true);
                _sceneReloadService.SetActive(true);
                _quizTeleportService.SetActive(true);
            }
            else
            {
                _adminCanvas.enabled = false;

                _callibrationService.SetActive(false);
                _sceneReloadService.SetActive(false);
                _quizTeleportService.SetActive(false);
            }
            
            _lastVisible = !_lastVisible;
        }
    }
}