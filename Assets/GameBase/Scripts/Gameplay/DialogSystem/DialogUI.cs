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
        
        [Header("Canvas")]
        public GameObject dialogBoxRoot;
        public Canvas dialogCanvas;

        public bool IsOpen { get; private set; }
        
        void Awake()
        {
            instance = this;
            
            if (dialogCanvas == null)
                dialogCanvas = GetComponentInParent<Canvas>();

            if (dialogBoxRoot != null)
                dialogBoxRoot.SetActive(false);

            if (dialogCanvas != null)
                dialogCanvas.gameObject.SetActive(false);

            if (continueButton != null)
            {
                var btn = continueButton.GetComponent<Button>();
                if (btn != null)
                    btn.onClick.AddListener(() => DialogManager.instance.ContinueDialog());
            }
        }

        public void Show(DialogNodeWithId node)
        {
            IsOpen = true;
            
            if (dialogCanvas != null)
                dialogCanvas.gameObject.SetActive(true);

            if (dialogBoxRoot != null)
                dialogBoxRoot.SetActive(true);

            speakerText.text = node.speaker;
            dialogText.text = node.text;
            
            // foreach (Transform child in optionsContainer.transform)
            //     Destroy(child.gameObject);

            continueButton.SetActive(true);
            
            // for (int i = 0; i < node.options.Count; i++)
            // {
            //     var go = Instantiate(optionPrefab, optionsContainer.transform);
            //     go.GetComponentInChildren<Text>().text = node.options[i].text;
            //     int captured = i;
            //     go.GetComponent<Button>().onClick.AddListener(() => DialogManager.instance.SelectOption(captured));
            // }
        }
        public void Hide()
        {
            IsOpen = false;

            if (dialogBoxRoot != null)
                dialogBoxRoot.SetActive(false);

            if (dialogCanvas != null)
                dialogCanvas.gameObject.SetActive(false);
        }
    }
}