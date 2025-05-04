using Dialogs.Scripts;
using Gameplay.QuestSystem.Runtime;
using UnityEngine;

namespace GameBase.Scripts.Gameplay.DialogSystem
{
    public class DialogStart : MonoBehaviour
    {
        [Tooltip("ID ноды начала диалога")]
        [SerializeField] private string _startDialogId = "introduction_start";
        [Tooltip("ID квеста, который нужно запустить вместе с диалогом")]
        [SerializeField] private string _questId = "QuestIntroduction";

        void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player"))
                return;
            
            DialogManager.instance.StartDialog(_startDialogId, _questId);
            GetComponent<Collider>().enabled = false;
        }
    }
}
