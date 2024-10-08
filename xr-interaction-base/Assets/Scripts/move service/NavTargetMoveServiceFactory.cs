using System;
using UnityEngine;
using UnityEngine.AI;

namespace ATG.Moveable
{
    [Serializable]
    public sealed class NavTargetMoveServiceFactory
    {
        [SerializeField] private NavMeshAgent navMeshAgent;
        [SerializeField] private NavTargetMoveData moveConfig;

        public IMoveableService Create()
        {
            return new NavTargetMoveService(navMeshAgent, moveConfig);
        }
    }
}