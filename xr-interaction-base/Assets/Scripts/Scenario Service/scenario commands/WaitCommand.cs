using System;
using System.Collections;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ATG.Scenario
{
    public sealed class WaitCommand : ScenarioCommand, IDisposable
    {
        [SerializeField] private float waitSeconds;

        //private IEnumerator _waitCoroutine;

        private CancellationTokenSource _dis;
        

        public void Dispose()
        {
            _dis?.Cancel();
            _dis?.Dispose();

            _dis = null;
        }

        public override void Execute()
        {
            Dispose();
            _dis = new CancellationTokenSource();

            base.Execute();

            WaitToComplete(_dis.Token).Forget();
        }

        public override void Exit()
        {
            Dispose();
        }

        protected override void Complete()
        {
            Exit();
            base.Complete();
        }

        private async UniTask WaitToComplete(CancellationToken token)
        {
            if(token.IsCancellationRequested == true) return;

            await UniTask.Delay((int)(waitSeconds * 1000f), cancellationToken: token);

            Complete();
        }
    }
}