using ATG.UI;
using System;
using UnityEngine;

namespace ATG.Scenario
{
    public class RestartStoryCommand : ScenarioCommand
    {
        [SerializeField] private RestartInfoView restartView;

        public override void Execute()
        {
            base.Execute();

            restartView.Show(this, (Action)Complete);
        }

        public override void Exit()
        {
            restartView.Hide();
        }
    }
}
