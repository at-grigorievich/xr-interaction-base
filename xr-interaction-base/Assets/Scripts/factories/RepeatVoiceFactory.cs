using System;
using ATG.Voice;
using UnityEngine;
using VContainer;

namespace ATG.Factory
{
    [Serializable]
    public sealed class RepeatVoiceFactory
    {
        [SerializeField] private RepeatVoiceData config;

        public void Create(IContainerBuilder builder)
        {
            builder.Register<RepeatVoiceService>(Lifetime.Singleton)
                .WithParameter<RepeatVoiceData>(config)
                .AsImplementedInterfaces();
        }
    }
}