using ATG.MVC;
using UnityEngine;
using VContainer;

namespace ATG.Scenario
{
    public sealed class EnterAvatarPortalCommand : ScenarioCommand
    {
        [SerializeField]
        private ParticleSystem enterPortalVFX;

        private AvatarController _avatarController;

        [Inject]
        public void Constructor(AvatarController avatarController)
        {
            _avatarController = avatarController;
        }

        public override void Execute()
        {
            if(enterPortalVFX != null)
            {
                enterPortalVFX.Play();
            }
            
            _avatarController.SetActive(false);

            base.Execute();

            Complete();
        }

        public override void Exit()
        {
            enterPortalVFX.Stop();
        }
    }
}