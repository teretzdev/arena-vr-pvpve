using System.Collections.Generic;
using UnityEngine;
using ArenaDeathMatch.Abilities;

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
    }

    // AbilityDatabase holds a collection of AbilityData, which is used by the StatisticalTablesGenerator.
    [System.Serializable]
    public class AbilityDatabase
    {
        public List<AbilityData> abilities;
    }
}