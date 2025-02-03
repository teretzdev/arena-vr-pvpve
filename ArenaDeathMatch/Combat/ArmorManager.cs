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
                // Optionally, if the ArmorManager should persist across scenes, uncomment the following line:
                // DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
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
