using System;
using ATG.MVC;
using ATG.XRInteraction.Extensions;
using UnityEngine;
using VContainer;

namespace ATG.Scenario
{
    public sealed class SetAgentInteractorActiveCommand : ScenarioCommand
    {
        [SerializeField] private AgentInteractorActive[] interactionActives;

        private VirtualAgentController _virtualAgentController;

        [Inject]
        public void Constructor(VirtualAgentController virtualAgentController)
        {
            _virtualAgentController = virtualAgentController;
        }

        public override void Execute()
        {
            foreach (var interactionActive in interactionActives)
            {
                _virtualAgentController.SetInteractorActive(
                    interactionActive.HandType, 
                    interactionActive.Interaction, 
                    interactionActive.IsActive);
            }
            
            base.Execute();

            Complete();
        }

        public override void Exit()
        {
        }

        [Serializable]
        private class AgentInteractorActive
        {
            [field: SerializeField] public HandType HandType { get; private set; }
            [field: SerializeField] public bool IsActive { get; private set; }
            [field: SerializeField] public InteractionType Interaction { get; private set; }
        }
    }
}