using UnityEngine;
using UnityEngine.AI;

namespace ATG.Moveable
{
    public sealed class NavTargetMoveService : IMoveableService
    {
        private readonly NavTargetMoveData _config;
        private readonly NavMeshAgent _agent;

        public float CurrentSpeed => _agent.speed;

        public bool IsActive { get; private set; }

        public Vector3 AgentForward => _agent.transform.forward;

        public Vector3 AgentPosition => _agent.transform.position;

        public bool HasAgent => _agent != null;

        public NavTargetMoveService(NavMeshAgent agent, NavTargetMoveData config)
        {
            _agent = agent;
            _config = config;

        }

        public void SetActive(bool isActive)
        {
            IsActive = isActive;

            if (_agent != null)
            {
                _agent.enabled = isActive;

                if (_agent.isOnNavMesh == true)
                {
                    _agent.isStopped = isActive == false;

                    _agent.speed = isActive == true ? _config.BaseSpeed : 0f;
                    _agent.angularSpeed = isActive == true ? _config.BaseAngularSpeed : 0f;
                    _agent.acceleration = isActive == true ? _config.BaseAcceleration : 0f;
                }
            }
        }

        public void Place(Vector3 position, Quaternion rotation)
        {
            if(_agent == null) return;

            _agent.enabled = false;

            _agent.transform.position = position;
            _agent.transform.rotation = rotation;
            
            _agent.enabled = true;
        }

        public bool NeedMove(Vector3 targetPoint)
        {
            targetPoint.y = AgentPosition.y;
            return Vector3.Distance(AgentPosition, targetPoint) > 0.1f;
        }

        public void Move(Vector3 targetPoint, float speedCoefficient = 1f)
        {
            if (IsActive == false) return;

            _agent.isStopped = false;
            _agent.speed = _config.BaseSpeed * speedCoefficient;
            _agent.SetDestination(targetPoint);
        }

        public bool NeedRotate(Vector3 lookAt)
        {
            lookAt.y = _agent.transform.position.y;

            float dot = Vector3.Dot(AgentForward, lookAt.normalized); // must be 1 and this vectors are collineal
            return dot < 0.92f;
        }

        public void Rotate(Vector3 lookAt, float speedCoefficient = 1f)
        {
            if (IsActive == false) return;
            lookAt.y = _agent.transform.position.y;


            Quaternion rotation = Quaternion.LookRotation(lookAt);
            _agent.transform.rotation = Quaternion.Lerp(_agent.transform.rotation, rotation, _config.BaseRotation * Time.deltaTime);
        }

        public void Stop()
        {
            if (_agent == null) return;

            if(_agent.isOnNavMesh == true)
             _agent.isStopped = true;
        }
    }
}