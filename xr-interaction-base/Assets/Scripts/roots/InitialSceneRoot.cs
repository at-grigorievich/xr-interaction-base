using ATG.EntryPoint;
using ATG.Factory;
using ATG.SceneManagement;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ATG.Root
{
    public sealed class InitialSceneRoot: LifetimeScope
    {
        [SerializeField] private SceneInfoData loadingSceneInfo;
        [SerializeField] private VirtualAgentFactory agentCreator;
        [SerializeField] private AdminServiceFactory adminServiceFactory;

        protected override void Configure(IContainerBuilder builder)
        {
            agentCreator.Create(builder);
            adminServiceFactory.Create(builder);

            builder.RegisterEntryPoint<InitialSceneEntryPoint>()
                .WithParameter<SceneInfoData>(loadingSceneInfo);
        }
    }
}