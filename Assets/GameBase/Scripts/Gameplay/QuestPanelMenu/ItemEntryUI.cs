using UnityEngine;
using TMPro;

namespace Gameplay.PanelMenu
{
    public class ItemEntryUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _label;
        [SerializeField] private GameObject _foundIcon;

        public bool Found { get; private set; }

        public void Initialize(ItemData data)
        {
            _label.text = data.DisplayName;
            Found = false;
            
            if (_foundIcon != null)
                _foundIcon.SetActive(false);
        }

        public void MarkFound()
        {
            if (Found)
                return;
            
            Found = true;
            
            if (_foundIcon != null)
                _foundIcon.SetActive(true);
            
            _label.fontStyle |= FontStyles.Strikethrough;
        }
    }
}