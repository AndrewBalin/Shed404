using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using UnityEngine.AI;
using UnityEngine.UI;

namespace Interactive
{
    [RequireComponent(typeof(Collider))]
    public class Interactable : MonoBehaviour
    {
        // Дистанция взаимодействия
        [Header("Interactive Settings")]
        public float interactionDistance = 3f;
        public InteractiveUseCase useCase;
        
        void Start()
        {
            this.GetComponent<CustomPassVolume>().customPasses[0].enabled = false;
        }
        
        public void OnHover()
        {
            this.GetComponent<CustomPassVolume>().customPasses[0].enabled = true;
        }
        
        public void OnUnhover()
        {
            this.GetComponent<CustomPassVolume>().customPasses[0].enabled = false;
        }
        
        public void PerformAction(string action, NavMeshAgent agent)
        {
            Debug.Log($"Action {action} performed on {gameObject.name}");
            useCase.Interact(this, agent, action);
        }
    }
}