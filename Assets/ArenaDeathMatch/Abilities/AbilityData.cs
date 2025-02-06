using System;

namespace ArenaDeathMatch.Abilities
{
    [Serializable]
    public class AbilityData
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public float Damage { get; set; }
        public float Cooldown { get; set; }
        public string Description { get; set; }

        // Constructor to initialize the ability data
        public AbilityData(string name, string type, float damage, float cooldown, string description)
        {
            Name = name;
            Type = type;
            Damage = damage;
            Cooldown = cooldown;
            Description = description;
        }

        // Default constructor for serialization
        public AbilityData() { }
    }
}
