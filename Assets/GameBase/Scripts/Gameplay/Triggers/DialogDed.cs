using Dialogs.Scripts;
using UnityEngine;

public class DialogDed : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
        DialogManager.instance.StartDialog("introduction_buhat");
    }
}
