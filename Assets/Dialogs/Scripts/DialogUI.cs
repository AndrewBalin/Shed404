using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Dialogs.Scripts
{
    public class DialogUI : MonoBehaviour
    {
        public static DialogUI instance;
        
        [Header("UI Elements")]
        public TextMeshProUGUI speakerText;
        public TextMeshProUGUI dialogText;
        public GameObject continueButton;
        
        [Header("Options")]
        public GameObject optionsContainer;
        public GameObject optionPrefab;

        void Awake()
        {
            instance = this;
            
            continueButton.GetComponent<Button>().onClick.AddListener(
                () => DialogManager.instance.ContinueDialog()
            );
        }

        public void Show(DialogNode node)
        {
            speakerText.text = node.speaker;
            dialogText.text = node.text;

            foreach (Transform child in optionsContainer.transform)
                Destroy(child.gameObject);
            
            continueButton.SetActive(!string.IsNullOrEmpty(node.next));
            
            for (int i = 0; i < node.options.Count; i++)
            {
                var go = Instantiate(optionPrefab, optionsContainer.transform);
                go.GetComponentInChildren<Text>().text = node.options[i].text;
                int captured = i;
                go.GetComponent<Button>().onClick.AddListener(() => DialogManager.instance.SelectOption(captured));
            }
        }
        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}