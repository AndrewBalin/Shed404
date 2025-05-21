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
        
        private int _doneCount;
        
        protected override void OnQuestComplete()
        {
            SceneManager.LoadScene("End");
        }
        
        public bool Action(String action_name, GameObject action_object)
        {
            _doneCount++;
            countText.text = $"{_doneCount}/{count}";
            if (count == _doneCount)
            {
                QuestComplete();
            }
            return true;
        }
    }
}