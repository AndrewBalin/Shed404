using System;
using UnityEngine;

namespace GamePanels.PanelMenu
{
    [Serializable]
    public class CollectibleItem
    {
        [SerializeField] private int _id;
        [SerializeField] private string _displayName;
        [SerializeField] private Sprite _icon;
        
        public int ID => _id;
        public string DisplayName => _displayName;
        public Sprite Icon => _icon;
        
        public CollectibleItem(int id, string displayName, Sprite icon)
        {
            _id = id;
            _displayName = displayName;
            _icon = icon;
        }
    }
}