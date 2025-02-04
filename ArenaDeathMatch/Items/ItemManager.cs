<code>
using System.Collections.Generic;
using UnityEngine;
using ArenaDeathMatch.Utilities;

namespace ArenaDeathMatch.Items
{
    // ItemManager is responsible for managing all game items and providing access
    // to their data for the statistical tables generator.
    public class ItemManager : MonoBehaviour
    {
        public static ItemManager Instance { get; private set; }

        [Header("Item Configuration")]
        // The ItemDatabase should be assigned via the Inspector with the list of available items.
        public ItemDatabase itemDatabase;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Debug.LogError("Multiple instances of ItemManager detected. Destroying duplicate instance.");
                Destroy(gameObject);
                return;
            }

            Instance = this;
            // Uncomment the following line if the ItemManager should persist between scenes.
            // DontDestroyOnLoad(gameObject);
        }

        // Returns a list of all item data for generating statistical reports.
        public List<ItemData> GetAllItemData()
        {
            if (itemDatabase != null && itemDatabase.items != null)
                return itemDatabase.items;
            return new List<ItemData>();
        }

        // Registers a new item into the database.
        public void RegisterItem(ItemData item)
        {
            if (itemDatabase != null)
            {
                if (itemDatabase.items == null)
                    itemDatabase.items = new List<ItemData>();

                if (!itemDatabase.items.Contains(item))
                    itemDatabase.items.Add(item);
            }
        }

        // Unregisters an item from the database.
        public void UnregisterItem(ItemData item)
        {
            if (itemDatabase != null && itemDatabase.items != null)
                itemDatabase.items.Remove(item);
        }
    }

    // ItemDatabase holds a collection of ItemData, which is used by the StatisticalTablesGenerator.
    [System.Serializable]
    public class ItemDatabase
    {
        public List<ItemData> items;
    }
}
</code>