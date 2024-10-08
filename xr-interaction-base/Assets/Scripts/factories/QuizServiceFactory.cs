using System;
using ATG.Quiz;
using UnityEngine;
using VContainer;

namespace ATG.Factory
{
    [Serializable]
    public sealed class QuizServiceFactory
    {
        [SerializeField] private QuizData quizConfig;
        [SerializeField] private int maxQuestionsCount;

        public void Create(IContainerBuilder builder)
        {
            builder.Register<IQuizService, QuizService>(Lifetime.Singleton)
                .WithParameter<QuizData>(quizConfig)
                .WithParameter<int>(maxQuestionsCount);
        }
    }
}