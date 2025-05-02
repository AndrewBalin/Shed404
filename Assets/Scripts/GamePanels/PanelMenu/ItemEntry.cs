using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace GamePanels.PanelMenu
{
    public class ItemEntry : MonoBehaviour
    {
        [SerializeField] private Image _iconImage;
        [SerializeField] private TextMeshProUGUI _label;
        [SerializeField] private Image _checkmarkIcon;

        public int ItemID { get; private set; }
        public bool IsCollected { get; private set; }

        public void Initialize(int id, string displayName, Sprite icon)
        {
            ItemID = id;
            IsCollected = false;
            _iconImage.sprite = icon;
            _label.text = displayName;
            _checkmarkIcon.enabled = false;
        }

        public void SetCollected()
        {
            if (IsCollected)
                return;
            
            IsCollected = true;
            _checkmarkIcon.enabled = true;
            _label.color = Color.grey;
        }
    }
}