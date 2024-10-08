using System;
using ATG.SceneManagement;
using ATG.UI;

namespace ATG.Quiz
{
    public sealed class TeleportToQuizService: IQuizTeleporteable
    {
        private readonly ISceneManagement _sceneManagement;

        private readonly GoToQuizView _goToQuizView;

        private readonly SceneInfoData _quizScene;

        public bool IsActive { get; private set; }

        public TeleportToQuizService(ISceneManagement sceneManagement, SceneInfoData quizScene, GoToQuizView goToQuizView)
        {
            _quizScene = quizScene;

            _sceneManagement = sceneManagement;

            _goToQuizView = goToQuizView;
        }

        public void SetActive(bool isActive)
        {
            IsActive = isActive;

            if (IsActive == true)
            {
                ActivateSceneReloading();
            }
            else
            {
                DeactivateSceneReloading();
            }
        }

        public void TeleportToQuiz()
        {
            _sceneManagement.LoadBySceneInfoAdditiveAndUnloadLastAsync(_quizScene);
        }

        private void ActivateSceneReloading()
        {
            _goToQuizView.Show(this, (Action)TeleportToQuiz);
        }

        private void DeactivateSceneReloading()
        {
            _goToQuizView.Hide();
        }
    }
}