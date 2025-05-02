using UnityEngine;
using UnityEngine.AI;
using GamePanels.PanelMenu;

namespace Point_And_Click.Click_to_Move
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(NavMeshAgent))]
    public class PlayerController : MonoBehaviour
    {
        private const string Idle = "Idle";
        private const string Walk = "Walk";

        [Header("Movement")]
        [SerializeField] private ParticleSystem _clickEffect;
        [SerializeField] private LayerMask _clickableGround;

        [Header("Action")]
        [SerializeField] private LayerMask _clickableObject;
        [SerializeField] private ItemListUI _itemListUI;

        private CustomActions _input;
        private NavMeshAgent _agent;
        private Animator _animator;

        //float lookRotationSpeed = 8f;

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<Animator>();
        
            _input = new CustomActions();
            AssignInputs();
        }
        
        private void OnEnable() => _input.Enable(); 

        private void OnDisable() => _input.Disable();
    
        private void Update()
        {
            // FaceTarget();
            SetAnimations();
        }

        private void AssignInputs ()
        {
            _input.Main.Controller.performed += ctx => ClickToMove();
        }

        private void ClickToMove()
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
            if (Physics.Raycast(ray, out hit, 100f, _clickableObject))
            {
                if (hit.collider.TryGetComponent(out CollectibleComponent collectible))
                {
                    Debug.Log($"Collectible hit: ID={collectible.ID}");
                    
                    CollectibleItem item = collectible.ToItem();
                    _itemListUI.AddCollectedItem(item);
                    
                    Destroy(collectible.gameObject);
                }

                Debug.Log($"Clicked on object without CollectibleComponent: {hit.collider.gameObject.name}");
                
                return;
            }
            
            if (Physics.Raycast(ray, out hit, 100f, _clickableGround))
            {
                _agent.destination = hit.point;
                
                if (_clickEffect != null)
                {
                    Instantiate(_clickEffect, hit.point + new Vector3(0f, 0.1f, 0f), _clickEffect.transform.rotation);
                }
                
                FaceTarget();
            }
        }
        
        private void FaceTarget()
        {
            Vector3 direction = (_agent.destination - transform.position).normalized;    
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x,0,direction.z));
            //transform.rotation = Quaternion.Slerp(transform.rotation,lookRotation, Time.deltaTime * lookRotationSpeed);
            transform.rotation = lookRotation;  
        }

        private void SetAnimations()
        {
            if (_agent.velocity == Vector3.zero)
                _animator.Play(Idle);
            else
                _animator.Play(Walk);
        }
    }

    public class Quest: MonoBehaviour
    {
    
    }
}