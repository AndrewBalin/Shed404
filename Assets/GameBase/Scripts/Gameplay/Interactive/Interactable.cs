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
        public string iaction;
        
        [Header("Quest Settings")]
        public Canvas quest1Canvas;
        public Canvas defaultCanvas;
        
        [Header("Active Settings")]
        public GameObject car;
        public GameObject camera;
        public GameObject player;
        
        private Outline _outline;
        private bool _active;
        
        void Start()
        {
            _outline = gameObject.GetComponent<Outline>();
            _active = false;
        }
        
        public void OnHover()
        {
            if (_active) return;
            _outline.enabled = true;
        }
        
        public void OnUnhover()
        {
            if (_active) return;
            _outline.enabled = false;
        }
        
        public void PerformAction(string action, NavMeshAgent agent)
        {
            if (_active) return;
            // useCase.Interact(this, agent, action);
            switch (iaction)
            {
                case "quest1":
                    defaultCanvas.gameObject.SetActive(false);
                    quest1Canvas.gameObject.SetActive(true);
                    _active = true;
                    return;
                case "quest2":
                    camera.gameObject.SetActive(false);
                    player.gameObject.SetActive(false);
                    car.gameObject.SetActive(true);
                    return;
            }
        }
    }
}