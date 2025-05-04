using UnityEngine;

namespace GamePanels.PanelMenu
{
    [RequireComponent(typeof(Collider))]
    public class CollectibleComponent : MonoBehaviour
    {
        [SerializeField] private int _id;
        [SerializeField] private string _displayName;
        [SerializeField] private Sprite _icon;

        public int ID => _id;
        public string DisplayName => _displayName;
        public Sprite Icon => _icon;
        
        public CollectibleItem ToItem()
        {
            return new CollectibleItem(_id, _displayName, _icon);
        }
    }
}