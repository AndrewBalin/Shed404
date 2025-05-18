using Dialogs.Scripts;
using GameBase.Scripts.Gameplay.MovementSystem;
using Movement;
using UnityEngine;

public class SecondDialogDed : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            if (other.GetComponent<PlayerController>().IsQuestPassed)
            {
                Destroy(gameObject);
                DialogManager.instance.StartDialog("introduction_4");
            }
    }
}
