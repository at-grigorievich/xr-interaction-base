using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine.SceneManagement;
using UnityEngine;

namespace ATG.SceneManagement
{
    public sealed class DefaultSceneManagement : ISceneManagement, IDisposable
    {
        private readonly Camera _camera;
        private readonly Material _loadingSceneSkybox;

        private readonly int _loadingCameraMask;
        private readonly int _defaultCameraMask;

        private int _lastSceneIndex;

        private CancellationTokenSource _dis;

        public DefaultSceneManagement(Material loadingSceneSkybox)
        {
            _loadingSceneSkybox = loadingSceneSkybox;

            _loadingCameraMask = LayerMask.GetMask("vr-agent");
            _defaultCameraMask = -1; // everything

            _camera = Camera.main;
        }

        public void Dispose()
        {
            _dis?.Cancel();
            _dis?.Dispose();

            _dis = null;
        }

        public async UniTask LoadBySceneInfoAndUnloadCurrentAsync(SceneInfoData sceneInfo)
        {
            Dispose();
            _dis = new CancellationTokenSource();

            ChangeLoadingEnvironment();

            await SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene()).WithCancellation(_dis.Token);
            await LoadBySceneInfoAsync(sceneInfo, _dis.Token);

            ChangeSceneEnvironment();
        }

        public async UniTask LoadBySceneInfoAdditiveAndUnloadCurrentAsync(SceneInfoData sceneInfo)
        {
            Dispose();
            _dis = new CancellationTokenSource();

            ChangeLoadingEnvironment();

            await SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene()).WithCancellation(_dis.Token);
            await LoadBySceneInfoAdditiveAsync(sceneInfo, _dis.Token);
            
            ChangeSceneEnvironment();
        }

        public async UniTask LoadBySceneInfoAndUnloadLastAsync(SceneInfoData sceneInfo, float delay = -1)
        {
            Dispose();
            _dis = new CancellationTokenSource();

            ChangeLoadingEnvironment();

            if (_dis.IsCancellationRequested == true) return;

            if (delay != -1f)
            {
                await UniTask.Delay((int)delay * 1000, cancellationToken: _dis.Token);
            }

            await UnloadLastScene(_dis.Token);
            await LoadBySceneInfoAsync(sceneInfo, _dis.Token);

            ChangeSceneEnvironment();
        }

        public async UniTask LoadBySceneInfoAdditiveAndUnloadLastAsync(SceneInfoData sceneInfo, float delay = -1)
        {
            Dispose();
            _dis = new CancellationTokenSource();
            ChangeLoadingEnvironment();

            if (_dis.IsCancellationRequested == true) return;

            if (delay != -1f)
            {
                await UniTask.Delay((int)delay * 1000, cancellationToken: _dis.Token);
            }

            await UnloadLastScene(_dis.Token);
            await LoadBySceneInfoAdditiveAsync(sceneInfo, _dis.Token);

            ChangeSceneEnvironment();
        }

        public async UniTask LoadBySceneInfoAsync(SceneInfoData sceneInfo)
        {
            Dispose();
            _dis = new CancellationTokenSource();

            await LoadBySceneInfoAsync(sceneInfo, _dis.Token);
        }

        public async UniTask LoadBySceneInfoAdditiveAsync(SceneInfoData sceneInfo)
        {
            Dispose();
            _dis = new CancellationTokenSource();

            await LoadBySceneInfoAdditiveAsync(sceneInfo, _dis.Token);
        }

        public async UniTask ReloadCurrentSceneAsync()
        {
            if(_lastSceneIndex == 0) return;

            Dispose();
            _dis = new CancellationTokenSource();

            ChangeLoadingEnvironment();

            await UnloadLastScene(_dis.Token);
            await LoadScene(_lastSceneIndex, LoadSceneMode.Additive, _dis.Token);
            
            ChangeSceneEnvironment();
        }

        public async UniTask LoadBySceneInfoAsync(SceneInfoData sceneInfo, CancellationToken token)
        {
            if (token.IsCancellationRequested == true) return;

            int? index = sceneInfo.GetBuildSettingsIndex();

            if (SceneIndexNotNull(index) == true)
            {
                await LoadScene(index.Value, LoadSceneMode.Single, token);
            }
        }

        public async UniTask LoadBySceneInfoAdditiveAsync(SceneInfoData sceneInfo, CancellationToken token)
        {
            if (token.IsCancellationRequested == true) return;

            int? index = sceneInfo.GetBuildSettingsIndex();

            if(_lastSceneIndex == index)
            {
                Dispose();
                throw new ArgumentException($"scene with id {index} already loaded");
            }

            if (SceneIndexNotNull(index) == true)
            {
                await LoadScene(index.Value, LoadSceneMode.Additive, token);
            }
        }

        public async UniTask LoadByIndexAdditiveAsync(int index, CancellationToken token)
        {
            if (token.IsCancellationRequested == true) return;

            if (IsSceneIndexInList(index) == false) return;

            await LoadScene(index, LoadSceneMode.Additive, token);
        }

        public async UniTask LoadByIndexAsync(int index, CancellationToken token)
        {
            if (token.IsCancellationRequested == true) return;

            if (IsSceneIndexInList(index) == false) return;

            await LoadScene(index, LoadSceneMode.Single, token);
        }

        public async UniTask UnloadLastScene(CancellationToken token)
        {
            if (token.IsCancellationRequested == true) return;
            
            await SceneManager.UnloadSceneAsync(_lastSceneIndex).WithCancellation(token);
        }

        private async UniTask LoadScene(int index, LoadSceneMode mode, CancellationToken token)
        {
            if (token.IsCancellationRequested == true) return;

            await SceneManager.LoadSceneAsync(index, mode).WithCancellation(token);

            _lastSceneIndex = index;
        }


        private bool IsSceneIndexInList(in int index)
        {
            if (index < 0 || index >= SceneManager.sceneCountInBuildSettings)
                throw new ArgumentOutOfRangeException($"scene list not contains scene with id = {index}");

            return true;
        }

        private bool SceneIndexNotNull(in int? value)
        {
            if (value.HasValue == false)
            {
                throw new NullReferenceException("scene index does not exist !");
            }
            return true;
        }

        private void ChangeLoadingEnvironment()
        {
            RenderSettings.skybox = _loadingSceneSkybox;
            _camera.cullingMask = _loadingCameraMask;
        }

        private void ChangeSceneEnvironment()
        {
            _camera.cullingMask = _defaultCameraMask;
        }
    }
}