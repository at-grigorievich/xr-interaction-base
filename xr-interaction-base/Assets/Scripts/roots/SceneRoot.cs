using ATG.EntryPoint;
using ATG.Scenario;
using ATG.SceneManagement;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ATG.Root
{
    public class SceneRoot : LifetimeScope
    {
        [SerializeField] protected SceneInfoData initialScene;
        [SerializeField] protected SceneInfoData nextScene;
        [Space(15)]
        [SerializeField] protected StepByStepScenarioService scenario;
        [Space(15)]
        [SerializeField] protected Material skybox;

        protected override void Configure(IContainerBuilder builder)
        {
            RegisterScenario(builder);
            RegisterEntryPoint(builder);
        }

        protected virtual void RegisterEntryPoint(IContainerBuilder builder)
        {
            builder.RegisterEntryPoint<PlayeableSceneEntryPoint>()
                .WithParameter("skybox", skybox)
                .WithParameter("nextSceneInfo", nextScene)
                .WithParameter("initialSceneInfo", initialScene);
        }

        protected virtual void RegisterScenario(IContainerBuilder builder)
        {
            builder.RegisterInstance<IScenarioService>(scenario);
        }
    }

}