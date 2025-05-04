using Dialogs.Scripts;
using UnityEngine;

public class DialogStart : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        DialogManager.instance.StartDialog("introduction_start");
        gameObject.GetComponent<Collider>().enabled = false;
    }
}
