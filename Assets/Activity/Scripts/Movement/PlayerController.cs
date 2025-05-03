using System;
using Interactive;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;

namespace Movement
{
    public class PlayerController : MonoBehaviour
    {
        [Header("Main Settings")]
        public Camera cam;
        public NavMeshAgent agent;
        public LayerMask groundMask;
        public LayerMask interactableMask;
        
        private Interactable _currentInteractable;
        private Animator _animator;

        private void Start()
        {
            _animator = GetComponent<Animator>();
        }

        void Update()
        {
            HoverCheck();
            Animate();
            if (EventSystem.current.IsPointerOverGameObject()) return;

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit interactableHit, 100f, interactableMask))
                {
                    Interactable interactable = interactableHit.collider.GetComponent<Interactable>();
                    if (interactable != null && interactable.interactionDistance > Vector3.Distance(
                                                        agent.transform.position, interactable.transform.position))
                    {
                        _currentInteractable = interactable;
                        _currentInteractable.PerformAction("Use", agent);
                    }
                }
                else if (Physics.Raycast(ray, out RaycastHit groundHit, 100f, groundMask))
                {
                    agent.SetDestination(groundHit.point);
                }
                
            }
        }
        
        void HoverCheck()
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f, interactableMask))
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
            if (agent.velocity == Vector3.zero)
                _animator.Play(Animations.IDLE);
            else if (agent.velocity != Vector3.zero)
                _animator.Play(Animations.WALK);
            
            SetRotation();
        }

        void SetRotation()
        {
            Vector3 velocity = agent.velocity;
            if (velocity.sqrMagnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(velocity.normalized);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
            }  
        }

        private class Animations
        {
            public const string IDLE = "Idle";
            public const string WALK = "Walk";
        }
    }
}
