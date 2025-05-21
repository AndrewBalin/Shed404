using Dialogs.Scripts;
using GameBase.Scripts.Gameplay.TempQuest;
using UnityEngine;

public class Pound : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        QuestSystem.instance.CompleteQuest("drive_to_pound");
    }
}
