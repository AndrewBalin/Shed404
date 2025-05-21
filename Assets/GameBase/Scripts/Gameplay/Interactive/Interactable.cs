using Dialogs.Scripts;
using GameBase.Scripts.Gameplay.TempQuest;
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
        public string handleAction;
        public string questPassed;
        
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
            if (!string.IsNullOrEmpty(questPassed) && !QuestSystem.instance.IsQuestActive(questPassed)) return;
            if (_active) return;
            _outline.enabled = true;
        }
        
        public void OnUnhover()
        {
            if (!string.IsNullOrEmpty(questPassed) && !QuestSystem.instance.IsQuestActive(questPassed)) return;
            if (_active) return;
            _outline.enabled = false;
        }
        
        public void PerformAction(string action, NavMeshAgent agent)
        {
            if (!string.IsNullOrEmpty(questPassed) && !QuestSystem.instance.IsQuestActive(questPassed)) return;
            if (_active) return;
            // useCase.Interact(this, agent, action);
            if (!string.IsNullOrEmpty(handleAction) && handleAction.StartsWith("dialog_start:"))
            {
                DialogManager.instance.StartDialog(handleAction.Substring("dialog_start:".Length));
                return;
            }
            
            switch (handleAction)
            {
                case "quest1":
                    defaultCanvas.gameObject.SetActive(false);
                    quest1Canvas.gameObject.SetActive(true);
                    _outline.enabled = false;
                    _active = true;
                    return;
                case "quest2":
                    camera.gameObject.SetActive(false);
                    player.gameObject.SetActive(false);
                    car.gameObject.SetActive(true);
                    _outline.enabled = false;
                    _active = true;
                    return;
                case "CollectItem":
                    bool collected = QuestSystem.instance.Action("CollectItem", this.gameObject);
                    if (!collected) return;
                    _outline.enabled = false;
                    _active = true;
                    return;
            }
        }
    }
}