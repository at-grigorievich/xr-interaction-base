using ATG.Activator;
using ATG.UI;
using UnityEngine;

namespace ATG.Voice
{
    public sealed class RepeatTalkingHelper: ActivateBehaviour
    {
        public const string Layer = "repeat-talking";

        [SerializeField] private InfoView repeatVoicePanel;
        [SerializeField] private Collider trigger;

        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
            SetActive(false);
        }

        public override void SetActive(bool isActive)
        {
            base.SetActive(isActive);

            if(trigger != null)
            {
                trigger.enabled = isActive;
            }

            if(repeatVoicePanel != null)
            {
                if (isActive == true)
                {
                    repeatVoicePanel.Show(this, null);
                }
                else
                {
                    repeatVoicePanel.Hide();
                }
            }
        }


        private void Update()
        {
            if(IsActive == false) return;

            Vector3 target = _camera.transform.position;
            target.y = transform.position.y;

            transform.LookAt(target);
        }
    }
}