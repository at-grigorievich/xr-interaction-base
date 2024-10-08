using System;
using ATG.Admin;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;

namespace ATG.Factory
{
    [Serializable]
    public class AdminServiceFactory
    {
        [SerializeField] private CallibrationServiceFactory callibrationServiceFactory;
        [SerializeField] private TeleportToQuizServiceFactory teleportToQuizServiceFactory;
        [SerializeField] private RestartCurrentSceneServiceFactory restartCurrentSceneServiceFactory;
        [Space(10)]
        [SerializeField] private Canvas adminPanelCanvas;
        [SerializeField] private InputActionReference activateAdminInput;

        public void Create(IContainerBuilder builder)
        {
            builder.Register<IAdministrateable, AdminService>(Lifetime.Singleton)
                .WithParameter<CallibrationServiceFactory>(callibrationServiceFactory)
                .WithParameter<TeleportToQuizServiceFactory>(teleportToQuizServiceFactory)
                .WithParameter<RestartCurrentSceneServiceFactory>(restartCurrentSceneServiceFactory)
                .WithParameter<Canvas>(adminPanelCanvas)
                .WithParameter<InputActionReference>(activateAdminInput);
        }
    }
}