using System;
using UnityEngine;

namespace ATG.UI
{
    public sealed class RestartInfoView: InfoView
    {
        [SerializeField] private ScaleClickButton restartButton;
        [SerializeField] private bool hideOnAwake;

        private new  void Awake()
        {
            base.Awake();

            if(hideOnAwake == true)
            {
                Hide();
            }
        }

        public override void Show(object sender, object data)
        {
            base.Show(sender, data);

            if(data is not Action clickCallback)
            {
                throw new NullReferenceException("no callback to restart");
            }

            restartButton.Show(this, clickCallback);
        }

        public override void Hide()
        {
            base.Hide();
            
            if(restartButton != null)
            {
                restartButton.Hide();
            }
        }
    }
}
