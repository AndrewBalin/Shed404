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

        private Interactable currentInteractable;

        void Update()
        {
            
            HoverCheck();
            if (EventSystem.current.IsPointerOverGameObject()) return;

            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 100f, groundMask))
                {
                    agent.SetDestination(hit.point);
                    if (currentInteractable != null)
                    {
                        currentInteractable.OnUnhover();
                        currentInteractable = null;
                    }
                }
            }

            if (Input.GetMouseButtonDown(1))
            {
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 100f, interactableMask))
                {
                    Interactable interactable = hit.collider.GetComponent<Interactable>();
                    if (interactable != null)
                    {
                        currentInteractable = interactable;
                        currentInteractable.PerformAction("Use", agent);
                    }
                }
            }
            
            if (Input.GetMouseButtonUp(1))
            {
                ActionWheelUI.Instance.Hide();
            }

        }

        
        void HoverCheck()
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f, interactableMask))
            {
                Interactable hover = hit.collider.GetComponent<Interactable>();
                if (hover != null && hover != currentInteractable)
                {
                    if (currentInteractable != null)
                        currentInteractable.OnUnhover();
                    if (hover.interactionDistance > Vector3.Distance(agent.transform.position, hover.transform.position))
                    {
                        currentInteractable = hover;
                        currentInteractable.OnHover();
                    }
                }
            }
            else if (currentInteractable != null)
            {
                currentInteractable.OnUnhover();
                currentInteractable = null;
            }
        }
        
        void ShowActionWheel()
        {
            ActionWheelUI.Instance.Show(Input.mousePosition, currentInteractable);
        }
        
        void CloseActionWheel()
        {
            ActionWheelUI.Instance.Hide();
        }
    }
}
