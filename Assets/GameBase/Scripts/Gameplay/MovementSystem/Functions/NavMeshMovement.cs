using UnityEngine;
using UnityEngine.AI;

namespace GameBase.Scripts.Gameplay.MovementSystem.Functions
{
    public class NavMeshMovement
    {
        private readonly NavMeshAgent _agent;

        public NavMeshMovement(NavMeshAgent agent, float instantAcceleration, float stoppingDistance)
        {
            _agent = agent;
            _agent.acceleration = instantAcceleration;
            _agent.autoBraking = true;
            _agent.stoppingDistance = stoppingDistance;
             _agent.updateRotation = false;
        }

        public void MoveTo(Vector3 point)
        {
            _agent.isStopped = false;
            _agent.SetDestination(point);
        }

        public Vector3 CurrentVelocity => _agent.velocity;
        
        public Vector3 DesiredVelocity => _agent.desiredVelocity;
        
        public bool HasReachedDestination => !_agent.pathPending && _agent.remainingDistance <= _agent.stoppingDistance;
    }
}