using Interactive;
using UnityEngine;

public class OpenQuest1 : InteractiveUseCase
{
    public Canvas quest1Canvas;
    public Canvas defaultCanvas;
    public Interactable interactive;
    
    public void Interact(Interactable interactable, UnityEngine.AI.NavMeshAgent agent, string action)
    {
        defaultCanvas.gameObject.SetActive(false);
        quest1Canvas.gameObject.SetActive(true);
        interactive.enabled = false;
    }
}
