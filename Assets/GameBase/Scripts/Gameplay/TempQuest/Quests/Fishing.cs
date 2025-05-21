using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GameBase.Scripts.Gameplay.TempQuest.Quests
{
    [System.Serializable] 
    public class Fishing : Quest
    {
        public int count;
        public TextMeshProUGUI countText;
        
        public int _doneCount;
        
        protected override void OnQuestComplete()
        {
            SceneManager.LoadScene("End");
        }
        
        protected override void OnAction()
        {
            Debug.Log("Fishing 1");
            _doneCount++;
            countText.text = $"{_doneCount}/{count}";
            if (count == _doneCount)
            {
                QuestComplete();
            }
        }
    }
}