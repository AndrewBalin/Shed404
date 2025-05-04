using System;
using UnityEngine;

namespace Gameplay.PanelMenu
{
    [Serializable]
    public class ItemData
    {
        [SerializeField] private int _id;
        [SerializeField] private string _displayName;

        public int Id => _id;
        public string DisplayName => _displayName;
    }
}