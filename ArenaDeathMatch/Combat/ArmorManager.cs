<code>
using System.Collections.Generic;
using UnityEngine;
using ArenaDeathMatch.Utilities;

namespace ArenaDeathMatch.Combat
{
    // ArmorManager is responsible for managing all armor pieces and 
    // providing access to their data for the statistical tables generator.
    public class ArmorManager : MonoBehaviour
    {
        public static ArmorManager Instance { get; private set; }

        [Header("Armor Configuration")]
        // ArmorDatabase should be assigned via the Inspector with the list of available armors.
        public ArmorDatabase armorDatabase;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                PopulateArmors();
                // Optionally, if the ArmorManager should persist across scenes, uncomment the following line:
                // DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        
        // Populates the armor database with all armor pieces in the game.
        public void PopulateArmors()
        {
            if (armorDatabase == null)
                armorDatabase = new ArmorDatabase();
            if (armorDatabase.armors == null)
                armorDatabase.armors = new List<ArmorData>();

            if (armorDatabase.armors.Count == 0)
            {
                ArmorData steelPlate = new ArmorData();
                steelPlate.name = "Steel Plate Armor";
                steelPlate.defenseRating = 100f;
                steelPlate.weight = 30f;
                steelPlate.durability = 200f;
                steelPlate.requiredLevel = 20;
                steelPlate.rarity = "Rare";
                steelPlate.description = "A sturdy set of steel plate armor.";
                armorDatabase.armors.Add(steelPlate);

                ArmorData leather = new ArmorData();
                leather.name = "Leather Armor";
                leather.defenseRating = 50f;
                leather.weight = 15f;
                leather.durability = 100f;
                leather.requiredLevel = 5;
                leather.rarity = "Common";
                leather.description = "Lightweight leather armor for swift movement.";
                armorDatabase.armors.Add(leather);

                ArmorData chainmail = new ArmorData();
                chainmail.name = "Chainmail Armor";
                chainmail.defenseRating = 75f;
                chainmail.weight = 25f;
                chainmail.durability = 150f;
                chainmail.requiredLevel = 10;
                chainmail.rarity = "Uncommon";
                chainmail.description = "A balanced chainmail armor offering moderate protection.";
                armorDatabase.armors.Add(chainmail);
            }
        }

        // Returns a list of all armor data for generating statistical reports.
        public List<ArmorData> GetAllArmorData()
        {
            if (armorDatabase != null && armorDatabase.armors != null)
                return armorDatabase.armors;
            return new List<ArmorData>();
        }

        // Registers a new armor piece into the database.
        public void RegisterArmor(ArmorData armor)
        {
            if (armorDatabase != null)
            {
                if (armorDatabase.armors == null)
                    armorDatabase.armors = new List<ArmorData>();

                if (!armorDatabase.armors.Contains(armor))
                    armorDatabase.armors.Add(armor);
            }
        }

        // Unregisters an armor piece from the database.
        public void UnregisterArmor(ArmorData armor)
        {
            if (armorDatabase != null && armorDatabase.armors != null)
                armorDatabase.armors.Remove(armor);
        }
    }

    // ArmorDatabase holds a collection of ArmorData, which is used to build statistical tables.
    [System.Serializable]
    public class ArmorDatabase
    {
        public List<ArmorData> armors;
    }
}
</code>