using System.Collections.Generic;
using UnityEngine;

namespace ArenaDeathMatch.Combat
{
    [System.Serializable]
    public class WeaponDatabase : ScriptableObject
    {
        [Header("Weapon Collection")]
        public List<WeaponData> weapons;
    }
}