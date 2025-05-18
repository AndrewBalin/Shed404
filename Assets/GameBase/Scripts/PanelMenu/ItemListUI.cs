using System.Collections.Generic;
using UnityEngine;

namespace GamePanels.PanelMenu
{
    public class ItemListUI : MonoBehaviour
    {
        [SerializeField] private GameObject _entryPrefab;
        [SerializeField] private Transform _contentParent;
        [SerializeField] private int _totalItemsToCollect = 5;
        
        private Dictionary<int, ItemEntry> _entriesById = new Dictionary<int, ItemEntry>();
        private int _collectedCount = 0;

        public void AddCollectedItem(CollectibleItem item)
        {
            if (_entriesById.ContainsKey(item.ID))
                return;
            
            GameObject itemObject = Instantiate(_entryPrefab, _contentParent);
            
            if (itemObject.TryGetComponent(out ItemEntry entry))
            {
                entry.Initialize(item.ID, item.DisplayName, item.Icon);
            }
            
            entry.SetCollected();
            
            _entriesById[item.ID] = entry;
            _collectedCount++;

            if (_collectedCount >= _totalItemsToCollect)
                OnAllCollected();
        }

        private void OnAllCollected()
        {
            Debug.Log("Все предметы собраны!");
        }
    }
}