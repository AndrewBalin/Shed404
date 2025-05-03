using UnityEngine;
using UnityEngine.AI;

namespace Interactive
{
    [RequireComponent(typeof(Collider))]
    [RequireComponent(typeof(Outline))]
    public class Interactable : MonoBehaviour
    {
        [Header("Interactive Settings")]
        // Дистанция взаимодействия
        public float interactionDistance = 3f;
        public InteractiveUseCase useCase;
        
        private Outline _outline;
        
        void Start()
        {
            _outline = gameObject.GetComponent<Outline>();
        }
        
        public void OnHover()
        {
            _outline.enabled = true;
        }
        
        public void OnUnhover()
        {
            _outline.enabled = false;
        }
        
        public void PerformAction(string action, NavMeshAgent agent)
        {
            useCase.Interact(this, agent, action);
        }
    }
}