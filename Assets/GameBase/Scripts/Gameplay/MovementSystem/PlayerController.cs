using UnityEngine;
using UnityEngine.AI;
using GameBase.Scripts.Gameplay.MovementSystem.Functions;

namespace GameBase.Scripts.Gameplay.MovementSystem
{
    [RequireComponent(typeof(Animator))]
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController Instance;
        
        [Header("Camera Settings")]
        [SerializeField] private Camera _mainCamera;
        [SerializeField] private Camera _homeCamera;
        
        [Header("NavMesh] Settings")]   
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private LayerMask _groundMask;
        [SerializeField] private LayerMask _interactableMask;
        
        private InputHandler _input;
        private NavMeshMovement _movement;
        private SimpleAnimation _animation;
        private Rotation _rotation;
        private Animator _animator;
        private Camera _activeCamera;
        
        private float _speed;
        private float _turnSpeed;
        private float _stoppingDistance;
        
        public bool IsQuestPassed;
        
        public NavMeshAgent Agent => _agent;
        
        private void Awake()
        {
            _speed = _agent.speed;
            _turnSpeed = _agent.angularSpeed;
            _stoppingDistance = _agent.stoppingDistance;
            
            _animator = GetComponent<Animator>();
            
            _input = new InputHandler(_groundMask, _interactableMask);
            _movement = new NavMeshMovement(_agent, _speed, _stoppingDistance);
            _animation = new SimpleAnimation(_animator , _movement);
            _rotation  = new Rotation(_turnSpeed);

            _input.MoveRequested += point => _movement.MoveTo(point);
            _input.InteractRequested += i => i.PerformAction("Use", _agent);
            _input.Hover += i => i.OnHover();
            _input.Unhover += i => i.OnUnhover();
        }

        private void Start()
        {
            _activeCamera = _mainCamera;
            _homeCamera.gameObject.SetActive(false);
            _mainCamera.gameObject.SetActive(true);
            IsQuestPassed = false;
        }

        private void Update()
        {
            _input.ProcessHover(_activeCamera);
            _input.ProcessInput(_activeCamera);
            
            _animation.Update();
            _rotation.UpdateRotation(transform, _movement.CurrentVelocity);
        }

        public void SetActiveCamera(string cameraName)
        {
            if (cameraName == "MainCamera")
            {
                _homeCamera.gameObject.SetActive(false);
                _mainCamera.gameObject.SetActive(true);
                _activeCamera = _mainCamera;
            }
            else if (cameraName == "HomeCamera")
            {
                _mainCamera.gameObject.SetActive(false);
                _homeCamera.gameObject.SetActive(true);
                _activeCamera = _homeCamera;
            }
        }
        
        /*void Update()
        {
            //Animate();
            
            if (DialogUI.instance != null && DialogUI.instance.IsOpen)
                return;
            
            HoverCheck();
            
            if (EventSystem.current.IsPointerOverGameObject()) return;

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
                
                if (Physics.Raycast(ray, out RaycastHit interactableHit, 100f, _interactableMask))
                {
                    Interactable interactable = interactableHit.collider.GetComponent<Interactable>();
                    
                    if (interactable != null)
                    {
                        _currentInteractable = interactable;
                        _currentInteractable.PerformAction("Use", _agent);
                    }
                }
                else if (Physics.Raycast(ray, out RaycastHit groundHit, 100f, _groundMask))
                {
                    _agent.SetDestination(groundHit.point);
                }
                
            }
        }
        
        void HoverCheck()
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out RaycastHit hit, 100f, _interactableMask))
            {
                Interactable hover = hit.collider.GetComponent<Interactable>();
                
                if (hover != null && hover != _currentInteractable)
                {
                    if (_currentInteractable != null)
                        _currentInteractable.OnUnhover();
                    
                    _currentInteractable = hover;
                    _currentInteractable.OnHover();
                }
            }
            else if (_currentInteractable != null)
            {
                _currentInteractable.OnUnhover();
                _currentInteractable = null;
            }
        }

        void Animate()
        {
            if (_agent.velocity == Vector3.zero)
                _animator.Play(Animations.IDLE);
            else if (_agent.velocity != Vector3.zero)
                _animator.Play(Animations.WALK);
            
            SetRotation();
        }

        void SetRotation()
        {
            Vector3 velocity = _agent.velocity;
            
            if (velocity.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(velocity.normalized);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            }  
        }*/
    }
}