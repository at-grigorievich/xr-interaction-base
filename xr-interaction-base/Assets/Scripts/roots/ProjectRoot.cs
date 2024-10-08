using ATG.EntryPoint;
using ATG.SceneManagement;
using ATG.VRDevice;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace ATG.Root
{
    public sealed class ProjectRoot: LifetimeScope
    {
        [SerializeField] private DeviceDetectorFactory deviceDetectorFactory;
        [SerializeField] private ProjectEntryPoint entryPoint;
        [SerializeField] private Material loadingSceneSkybox;

        protected override void Configure(IContainerBuilder builder)
        {
            deviceDetectorFactory.Create(builder);

            builder.Register<DefaultSceneManagement>(Lifetime.Singleton)
                .WithParameter("loadingSceneSkybox", loadingSceneSkybox)
                .AsImplementedInterfaces();
        }
    }
}