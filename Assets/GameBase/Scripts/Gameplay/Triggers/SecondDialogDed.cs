using Dialogs.Scripts;
using Movement;
using UnityEngine;

public class SecondDialogDed : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            if (other.GetComponent<PlayerController>().isQuestPassed)
            {
                Destroy(gameObject);
                DialogManager.instance.StartDialog("introduction_4");
            }
    }
}
