using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.PanelMenu
{
    public class ItemsListDisplay : MonoBehaviour
    {
        [SerializeField] private List<ItemData> _itemsData; 
        [SerializeField] private ItemEntryUI _entryPrefab;
        [SerializeField] private Transform _listContainer;
        
        private Dictionary<int, ItemEntryUI> _entriesByID;
        private int _foundCount;
        
        private void Awake()
        {
            if (_entryPrefab == null || _listContainer == null || _itemsData == null)
            {
                Debug.LogError("ItemsListDisplay: Missing references!");
                enabled = false;
                
                return;
            }

            _entriesByID = new Dictionary<int, ItemEntryUI>(_itemsData.Count);

            foreach (ItemData data in _itemsData)
            {
                if (_entriesByID.ContainsKey(data.Id))
                {
                    Debug.LogWarning($"Duplicate ItemData id {data.Id}, skipping.");
                    
                    continue;
                }

                ItemEntryUI entry = Instantiate(_entryPrefab, _listContainer);
                
                entry.Initialize(data);
                _entriesByID[data.Id] = entry;
            }

            _foundCount = 0;
        }
        
        public void OnItemCollected(int id)
        {
            if (_entriesByID.TryGetValue(id, out ItemEntryUI entry) && !entry.Found)
            {
                entry.MarkFound();
                
                if (++_foundCount >= _entriesByID.Count)
                    OnAllItemsCollected();
            }
        }

        private void OnAllItemsCollected()
        {
            Debug.Log("Все предметы найдены!");
        }
    }
}