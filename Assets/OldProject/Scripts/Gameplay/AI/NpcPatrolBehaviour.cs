using UnityEngine;
using UnityEngine.AI;
using Zenject;
using Gameplay.AI.Common;

namespace Gameplay.AI
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class NpcPatrolBehaviour : MonoBehaviour, IPatrolBehaviour
    {
        [SerializeField] private float _idleInterval = 2f;
        
        private IWaypointProvider _waypoints;
        private INpcAnimation _animation;
        private IWaypointSelector _selector;
        private NavMeshAgent _agent;

        private int _currentIndex;
        private bool _isPatrolling;
        private float _idleTimer;
        
        [Inject]
        public void Construct(IWaypointProvider waypoints, INpcAnimation npcAnimation, IWaypointSelector selector)
        {
            _waypoints = waypoints;
            _animation = npcAnimation;
            _selector  = selector;
        }
        
        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
        }

        private void Start()
        {
            StartPatrol();
        }
        
        private void Update()
        {
            if (!_isPatrolling)
                return;

            bool isMoving = !_agent.pathPending && _agent.remainingDistance > _agent.stoppingDistance;
            
            if (isMoving)
            {
                Vector3 direction = _agent.velocity.normalized;
                
                if (direction.sqrMagnitude > 0.001f)
                    transform.rotation = Quaternion.LookRotation(direction);

                _animation.PlayWalk();
                _idleTimer = 0f;
            }
            else
            {
                _animation.PlayIdle();

                _idleTimer += Time.deltaTime;
                
                if (_idleTimer >= _idleInterval)
                {
                    _idleTimer = 0f;
                    _currentIndex = _selector.GetNextIndex(_currentIndex, _waypoints.Waypoints.Count);
                    
                    if (_currentIndex >= 0)
                        _agent.SetDestination(_waypoints.Waypoints[_currentIndex]);
                }
            }
        }

        public void StartPatrol()
        {
            _isPatrolling = true;
            _currentIndex = -1;
            _currentIndex = _selector.GetNextIndex(_currentIndex, _waypoints.Waypoints.Count);
            
            _agent.SetDestination(_waypoints.Waypoints[_currentIndex]);
        }

        public void StopPatrol()
        {
            _isPatrolling = false;
            _agent.isStopped = true;
            
            _animation.PlayIdle();
        }
    }
}