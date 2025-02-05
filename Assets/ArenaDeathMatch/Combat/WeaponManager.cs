using System.Collections.Generic;
using UnityEngine;

namespace ArenaDeathMatch.Combat
{
    // WeaponManager is responsible for managing all weapons in the game and
    // providing access to their data for generating statistical tables.
    public class WeaponManager : MonoBehaviour
    {
        public static WeaponManager Instance { get; private set; }

        [Header("Weapon Configuration")]
        // The WeaponDatabase should be assigned via the Inspector with the list of available weapons.
        public WeaponDatabase weaponDatabase;

        private Dictionary<WeaponType, WeaponData> weaponCache;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                InitializeWeaponCache();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        // Initializes the weapon cache for quick access to weapon data.
        private void InitializeWeaponCache()
        {
            weaponCache = new Dictionary<WeaponType, WeaponData>();

            if (weaponDatabase != null && weaponDatabase.weapons != null)
            {
                foreach (var weapon in weaponDatabase.weapons)
                {
                    if (!weaponCache.ContainsKey(weapon.type))
                    {
                        weaponCache[weapon.type] = weapon;
                    }
                }
            }
        }

        // Returns a list of all weapon data for generating statistical reports.
        public List<WeaponData> GetAllWeaponData()
        {
            if (weaponDatabase != null && weaponDatabase.weapons != null)
                return weaponDatabase.weapons;
            return new List<WeaponData>();
        }

        // Registers a new weapon into the database.
        public void RegisterWeapon(WeaponData weapon)
        {
            if (weaponDatabase != null)
            {
                if (weaponDatabase.weapons == null)
                    weaponDatabase.weapons = new List<WeaponData>();

                if (!weaponDatabase.weapons.Contains(weapon))
                {
                    weaponDatabase.weapons.Add(weapon);
                    weaponCache[weapon.type] = weapon;
                }
            }
        }

        // Unregisters a weapon from the database.
        public void UnregisterWeapon(WeaponData weapon)
        {
            if (weaponDatabase != null && weaponDatabase.weapons != null)
            {
                weaponDatabase.weapons.Remove(weapon);
                weaponCache.Remove(weapon.type);
            }
        }
    }

    // WeaponDatabase holds a collection of WeaponData, which is used by the StatisticalTablesGenerator.
    [System.Serializable]
    public class WeaponDatabase
    {
        public List<WeaponData> weapons;
    }

    // WeaponData represents the data structure for a weapon.
    [System.Serializable]
    public class WeaponData
    {
        public string name;
        public WeaponType type;
        public float damage;
        public int magazineSize;
        public float fireRate;
        public float reloadTime;
        public float recoilDuration;
        public GameObject prefab;
    }

    // Enum for weapon types.
    public enum WeaponType
    {
        Pistol,
        Rifle,
        Shotgun,
        Sniper,
        Melee,
        Explosive,
        Magic
    }
}
