using UnityEngine;
using ATG.Activator;

namespace ATG.Moveable
{
    public interface IMoveableService : IActivateable
    {
        float CurrentSpeed { get; }

        Vector3 AgentPosition { get; }
        Vector3 AgentForward { get; }

        void Place(Vector3 position, Quaternion rotation);

        bool NeedMove(Vector3 target);
        void Move(Vector3 targetPoint, float speedCoefficient = 1.0f);


        bool NeedRotate(Vector3 lookAt);
        void Rotate(Vector3 lookAt, float speedCoefficient = 1.0f);

        void Stop();
    }
}