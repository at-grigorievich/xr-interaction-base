using System;
using ATG.Quiz;
using ATG.SceneManagement;
using ATG.UI;
using UnityEngine;

namespace ATG.Factory
{
    [Serializable]
    public sealed class TeleportToQuizServiceFactory
    {
        [SerializeField] private GoToQuizView goToQuizView;
        [SerializeField] private SceneInfoData quizScene;

        public IQuizTeleporteable Create(ISceneManagement sceneManagement)
        {
            return new TeleportToQuizService(sceneManagement, quizScene, goToQuizView);
        }
    }
}