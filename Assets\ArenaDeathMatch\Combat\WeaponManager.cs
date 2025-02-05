using System.Collections.Generic;
using UnityEngine;

namespace ArenaDeathMatch.Combat
{
    public class WeaponManager : MonoBehaviour
    {
        public static WeaponManager Instance { get; private set; }

        [Header("Weapon Configuration")]
        public WeaponDatabase weaponDatabase;
        public Transform weaponContainer;

        [Header("Physics Settings")]
        public PhysicsSettings physicsSettings;

        private Dictionary<WeaponType, WeaponData> weaponCache = new Dictionary<WeaponType, WeaponData>();
        private List<VRWeapon> activeWeapons = new List<VRWeapon>();

        [System.Serializable]
        public class PhysicsSettings
        {
            public LayerMask collisionLayers;
            public float bulletSpeed = 100f;
            public float bulletLifetime = 5f;
            public bool useAdvancedBallistics = true;
            public float gravityMultiplier = 1f;
            public float airResistance = 0.1f;
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                InitializeWeapons();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        #region Weapon Management

        private void InitializeWeapons()
        {
            if (weaponDatabase != null && weaponDatabase.weapons != null)
            {
                foreach (WeaponData weapon in weaponDatabase.weapons)
                {
                    weaponCache[weapon.type] = weapon;
                }
            }
        }

        public VRWeapon SpawnWeapon(WeaponType type, Vector3 position, Quaternion rotation)
        {
            if (!weaponCache.ContainsKey(type))
                return null;

            WeaponData data = weaponCache[type];
            GameObject weaponObj = Instantiate(data.prefab, position, rotation, weaponContainer);

            VRWeapon weapon = weaponObj.GetComponent<VRWeapon>();
            if (weapon == null)
            {
                Debug.LogError($"VRWeapon component missing on prefab for {type}");
                Destroy(weaponObj);
                return null;
            }

            weapon.Initialize(data);
            activeWeapons.Add(weapon);
            return weapon;
        }

        public void DestroyWeapon(VRWeapon weapon)
        {
            if (activeWeapons.Contains(weapon))
            {
                activeWeapons.Remove(weapon);
                Destroy(weapon.gameObject);
            }
        }

        public List<WeaponData> GetAllWeaponData()
        {
            if (weaponDatabase != null && weaponDatabase.weapons != null)
                return weaponDatabase.weapons;
            return new List<WeaponData>();
        }

        #endregion

        #region Data Structures

        [System.Serializable]
        public class WeaponData
        {
            public WeaponType type;
            public GameObject prefab;
            public float damage;
            public int magazineSize;
            public float fireRate;
            public float reloadTime;
            public float recoilDuration;
        }

        public enum WeaponType
        {
            Pistol,
            Rifle,
            Shotgun,
            SniperRifle,
            RocketLauncher,
            MeleeWeapon
        }

        public class VRWeapon : MonoBehaviour
        {
            public WeaponType type;
            public int currentAmmo;
            private WeaponData data;

            public void Initialize(WeaponData weaponData)
            {
                data = weaponData;
                currentAmmo = data.magazineSize;
            }

            public void Fire()
            {
                if (currentAmmo > 0)
                {
                    currentAmmo--;
                    Debug.Log($"{type} fired. Remaining ammo: {currentAmmo}");
                }
                else
                {
                    Debug.Log($"{type} is out of ammo!");
                }
            }

            public void Reload()
            {
                currentAmmo = data.magazineSize;
                Debug.Log($"{type} reloaded. Ammo: {currentAmmo}");
            }
        }

        #endregion
    }
}