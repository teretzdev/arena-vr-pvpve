<code>
using System.Collections.Generic;
using UnityEngine;

namespace ArenaDeathMatch.Abilities
{
    // AbilityManager is responsible for managing all abilities in the game and
    // providing access to their data for generating statistical tables.
    public class AbilityManager : MonoBehaviour
    {
        public static AbilityManager Instance { get; private set; }

        [Header("Ability Configuration")]
        // The AbilityDatabase should be assigned via the Inspector with the list of available abilities.
        public AbilityDatabase abilityDatabase;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                PopulateAbilities();
                // Uncomment the following line if the AbilityManager should persist between scenes.
                // DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        // Returns a list of all ability data for generating statistical reports.
        public List<AbilityData> GetAllAbilityData()
        {
            if (abilityDatabase != null && abilityDatabase.abilities != null)
                return abilityDatabase.abilities;
            return new List<AbilityData>();
        }

        // Registers a new ability into the database.
        public void RegisterAbility(AbilityData ability)
        {
            if (abilityDatabase != null)
            {
                if (abilityDatabase.abilities == null)
                    abilityDatabase.abilities = new List<AbilityData>();

                if (!abilityDatabase.abilities.Contains(ability))
                    abilityDatabase.abilities.Add(ability);
            }
        }

        // Unregisters an ability from the database.
        public void UnregisterAbility(AbilityData ability)
        {
            if (abilityDatabase != null && abilityDatabase.abilities != null)
                abilityDatabase.abilities.Remove(ability);
        }
        
        // Populates the ability database with all abilities available in the game.
        public void PopulateAbilities()
        {
            if (abilityDatabase == null)
                abilityDatabase = new AbilityDatabase();
            if (abilityDatabase.abilities == null)
                abilityDatabase.abilities = new List<AbilityData>();
            if (abilityDatabase.abilities.Count == 0)
            {
                AbilityData fireball = new AbilityData();
                fireball.name = "Fireball";
                fireball.type = "Magic";
                fireball.damage = 75f;
                fireball.manaCost = 50f;
                fireball.cooldown = 5f;
                fireball.range = 30f;
                fireball.areaOfEffect = 5f;
                fireball.duration = 0f;
                fireball.description = "Casts a fiery projectile that explodes on impact.";
                abilityDatabase.abilities.Add(fireball);
                
                AbilityData iceBlast = new AbilityData();
                iceBlast.name = "Ice Blast";
                iceBlast.type = "Magic";
                iceBlast.damage = 65f;
                iceBlast.manaCost = 40f;
                iceBlast.cooldown = 6f;
                iceBlast.range = 25f;
                iceBlast.areaOfEffect = 4f;
                iceBlast.duration = 0f;
                iceBlast.description = "Unleashes a chilling blast that slows enemies.";
                abilityDatabase.abilities.Add(iceBlast);
                
                AbilityData lightningStrike = new AbilityData();
                lightningStrike.name = "Lightning Strike";
                lightningStrike.type = "Magic";
                lightningStrike.damage = 85f;
                lightningStrike.manaCost = 60f;
                lightningStrike.cooldown = 7f;
                lightningStrike.range = 35f;
                lightningStrike.areaOfEffect = 3f;
                lightningStrike.duration = 0f;
                lightningStrike.description = "Calls down lightning to strike a single target.";
                abilityDatabase.abilities.Add(lightningStrike);
            }
        }
    }

    // AbilityDatabase holds a collection of AbilityData, which is used by the StatisticalTablesGenerator.
    [System.Serializable]
    public class AbilityDatabase
    {
        public List<AbilityData> abilities;
    }
}
</code>