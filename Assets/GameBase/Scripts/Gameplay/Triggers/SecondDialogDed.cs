using Dialogs.Scripts;
using GameBase.Scripts.Gameplay.MovementSystem;
using GameBase.Scripts.Gameplay.TempQuest;
using Movement;
using UnityEngine;

public class SecondDialogDed : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            if (QuestSystem.instance.IsQuestComplete("find_objects_1"))
            {
                Destroy(gameObject);
                DialogManager.instance.StartDialog("introduction_4");
            }
    }
}
