using System.Collections.Generic;
using UnityEngine;

namespace ArenaDeathMatch.Combat
{
    [CreateAssetMenu(fileName = "WeaponDatabase", menuName = "ArenaDeathMatch/WeaponDatabase")]
    public class WeaponDatabase : ScriptableObject
    {
        public List<WeaponData> weapons;

        private void OnEnable()
        {
            InitializeWeapons();
        }

        private void InitializeWeapons()
        {
            weapons = new List<WeaponData>
            {
                new WeaponData
                {
                    type = WeaponType.GasCan,
                    prefab = Resources.Load<GameObject>("Prefabs/Weapons/SM_Wep_Bomb_GasCan_01"),
                    damage = 100f,
                    magazineSize = 0,
                    fireRate = 0f,
                    reloadTime = 0f,
                    recoilDuration = 0f,
                    description = "A throwable gas canister that explodes on impact, dealing massive area damage."
                },
                new WeaponData
                {
                    type = WeaponType.PropaneTank,
                    prefab = Resources.Load<GameObject>("Prefabs/Weapons/SM_Wep_Bomb_Propane_01"),
                    damage = 120f,
                    magazineSize = 0,
                    fireRate = 0f,
                    reloadTime = 0f,
                    recoilDuration = 0f,
                    description = "A propane tank that can be detonated to cause a fiery explosion."
                },
                new WeaponData
                {
                    type = WeaponType.Chainsaw,
                    prefab = Resources.Load<GameObject>("Prefabs/Weapons/SM_Wep_ChainSaw_01"),
                    damage = 75f,
                    magazineSize = 0,
                    fireRate = 0.5f,
                    reloadTime = 0f,
                    recoilDuration = 0f,
                    description = "A powerful melee weapon that deals continuous damage when held against enemies."
                },
                new WeaponData
                {
                    type = WeaponType.Crossbow,
                    prefab = Resources.Load<GameObject>("Prefabs/Weapons/SM_Wep_CrossBow_01"),
                    damage = 50f,
                    magazineSize = 1,
                    fireRate = 1.5f,
                    reloadTime = 2.0f,
                    recoilDuration = 0.3f,
                    description = "A precision weapon that fires bolts with high accuracy and damage."
                },
                new WeaponData
                {
                    type = WeaponType.CleanCrossbow,
                    prefab = Resources.Load<GameObject>("Prefabs/Weapons/SM_Wep_CrossBow_Clean_01"),
                    damage = 55f,
                    magazineSize = 1,
                    fireRate = 1.4f,
                    reloadTime = 1.8f,
                    recoilDuration = 0.3f,
                    description = "A cleaner, more efficient version of the standard crossbow with slightly improved stats."
                },
                new WeaponData
                {
                    type = WeaponType.MetalBat,
                    prefab = Resources.Load<GameObject>("Prefabs/Weapons/SM_Wep_Bat_Metal_01"),
                    damage = 30f,
                    magazineSize = 0,
                    fireRate = 1.0f,
                    reloadTime = 0f,
                    recoilDuration = 0f,
                    description = "A sturdy metal bat that delivers heavy blows to enemies."
                },
                new WeaponData
                {
                    type = WeaponType.WoodenBat,
                    prefab = Resources.Load<GameObject>("Prefabs/Weapons/SM_Wep_Bat_Wood_01"),
                    damage = 25f,
                    magazineSize = 0,
                    fireRate = 1.2f,
                    reloadTime = 0f,
                    recoilDuration = 0f,
                    description = "A lightweight wooden bat, ideal for quick melee attacks."
                }
            };
        }
    }

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
        public string description;
    }

    public enum WeaponType
    {
        GasCan,
        PropaneTank,
        Chainsaw,
        Crossbow,
        CleanCrossbow,
        MetalBat,
        WoodenBat
    }
}
