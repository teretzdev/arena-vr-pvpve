using UnityEngine;

namespace ArenaDeathMatch.Abilities
{
    [System.Serializable]
    public class AbilityData
    {
        [Header("Basic Information")]
        public string abilityName; // The name of the ability
        public string description; // A brief description of the ability

        [Header("Cost and Cooldown")]
        public int manaCost; // The mana cost to use the ability
        public float cooldown; // The cooldown time in seconds before the ability can be used again

        [Header("Effect Details")]
        public float damage; // The amount of damage the ability deals
        public float range; // The range of the ability in meters
        public float areaOfEffect; // The radius of the ability's effect, if applicable

        [Header("Visual and Audio Effects")]
        public GameObject visualEffect; // The visual effect prefab for the ability
        public AudioClip soundEffect; // The sound effect played when the ability is used
    }
}