using UnityEngine.XR.Interaction.Toolkit;

namespace ATG.XRInteraction.Extensions
{
    public interface IInteractorsHolder
    {
        bool GetInteractorActiveselfByType(InteractionType type);
        void SetInteractorActiveByType(InteractionType type, bool isActive);
        void SetActiveAllInteractors(bool isActive);

        XRBaseControllerInteractor GetInteractorByType(InteractionType type);
    }
}