using System.Threading;
using Cysharp.Threading.Tasks;

namespace ATG.SceneManagement
{
    public interface ISceneManagement
    {
        UniTask LoadBySceneInfoAndUnloadCurrentAsync(SceneInfoData sceneInfo);
        UniTask LoadBySceneInfoAdditiveAndUnloadCurrentAsync( SceneInfoData sceneInfo);

        UniTask LoadBySceneInfoAndUnloadLastAsync(SceneInfoData sceneInfo, float delay = -1f);
        UniTask LoadBySceneInfoAdditiveAndUnloadLastAsync( SceneInfoData sceneInfo, float delay = -1f);

        UniTask LoadBySceneInfoAsync(SceneInfoData sceneInfo);
        UniTask LoadBySceneInfoAdditiveAsync(SceneInfoData sceneInfo);

        UniTask LoadBySceneInfoAsync(SceneInfoData sceneInfo, CancellationToken cancellationSource);
        UniTask LoadBySceneInfoAdditiveAsync(SceneInfoData sceneInfo, CancellationToken cancellationToken);

        UniTask LoadByIndexAsync(int index, CancellationToken cancellationSource);
        UniTask LoadByIndexAdditiveAsync(int index, CancellationToken cancellationSource);

        UniTask ReloadCurrentSceneAsync();
        UniTask UnloadLastScene(CancellationToken cancellationSource);
    }
}