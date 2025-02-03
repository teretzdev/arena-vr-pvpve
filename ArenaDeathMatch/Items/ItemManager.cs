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
            if (Instance == null)
            {
                Instance = this;
                PopulateItems();
                // Uncomment the following line if the ItemManager should persist between scenes.
                // DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
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
        
        // Populates the item database with all items in the game.
        public void PopulateItems()
        {
            if (itemDatabase == null)
                itemDatabase = new ItemDatabase();
            if (itemDatabase.items == null)
                itemDatabase.items = new List<ItemData>();
            if (itemDatabase.items.Count == 0)
            {
                // Health Potion
                ItemData healthPotion = new ItemData();
                healthPotion.name = "Health Potion";
                healthPotion.type = "Consumable";
                healthPotion.value = 50;
                healthPotion.weight = 0.5f;
                healthPotion.stackable = true;
                healthPotion.maxStack = 20;
                healthPotion.description = "Restores 50 health points.";
                itemDatabase.items.Add(healthPotion);
                
                // Mana Potion
                ItemData manaPotion = new ItemData();
                manaPotion.name = "Mana Potion";
                manaPotion.type = "Consumable";
                manaPotion.value = 45;
                manaPotion.weight = 0.5f;
                manaPotion.stackable = true;
                manaPotion.maxStack = 20;
                manaPotion.description = "Restores 50 mana points.";
                itemDatabase.items.Add(manaPotion);
                
                // Bomb
                ItemData bomb = new ItemData();
                bomb.name = "Bomb";
                bomb.type = "Explosive";
                bomb.value = 100;
                bomb.weight = 1.0f;
                bomb.stackable = false;
                bomb.maxStack = 1;
                bomb.description = "Deals massive damage to enemies.";
                itemDatabase.items.Add(bomb);
            }
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