using System;
using UnityEngine;

namespace ATG.UI
{
    public sealed class GoToQuizView : UIElement
    {
        [SerializeField] private ScaleClickButton scaleClickButton;

        public override void Show(object sender, object data)
        {
            base.Show(sender, data);

            if (data is not Action callback)
            {
                throw new ArgumentOutOfRangeException("data must be callback!");
            }

            scaleClickButton.Show(this, callback);
        }

        public override void Hide()
        {
            base.Hide();
        }
    }
}