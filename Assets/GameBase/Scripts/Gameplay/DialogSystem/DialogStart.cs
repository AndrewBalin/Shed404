using Dialogs.Scripts;
using Gameplay.QuestSystem.Runtime;
using UnityEngine;

namespace GameBase.Scripts.Gameplay.DialogSystem
{
    public class DialogStart : MonoBehaviour
    {
        [Tooltip("ID ноды начала диалога")]
        public string startDialogId = "introduction_start";
        [Tooltip("ID квеста, который нужно запустить вместе с диалогом")]
        public string questId = "QuestIntroduction";

        void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player"))
                return;
            
            DialogManager.instance.StartDialog(startDialogId, questId);
            GetComponent<Collider>().enabled = false;
        }
    }
}
