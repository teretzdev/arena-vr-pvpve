<code>
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using ArenaDeathMatch.Combat;
using ArenaDeathMatch.Items;
using ArenaDeathMatch.Abilities;

namespace ArenaDeathMatch.Utilities
{
    // StatisticalTablesGenerator is responsible for collecting data from various game managers
    // (Weapons, Armors, Items, Abilities) and generating human‐readable statistical tables summarizing their attributes.
    public class StatisticalTablesGenerator : MonoBehaviour
    {
        // Generates a full statistics report by combining separate tables.
        public static string GenerateStatisticsReport()
        {
            StringBuilder report = new StringBuilder();
            report.AppendLine("=== Weapon Statistics ===");
            report.AppendLine(GenerateWeaponStatsTable());
            report.AppendLine();
            report.AppendLine("=== Armor Statistics ===");
            report.AppendLine(GenerateArmorStatsTable());
            report.AppendLine();
            report.AppendLine("=== Item Statistics ===");
            report.AppendLine(GenerateItemStatsTable());
            report.AppendLine();
            report.AppendLine("=== Ability Statistics ===");
            report.AppendLine(GenerateAbilityStatsTable());
            return report.ToString();
        }

        // Generates a table of weapon stats using WeaponManager's weapon database.
        public static string GenerateWeaponStatsTable()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Name\tType\tDamage\tMagazine\tFire Rate\tReload Time\tRecoil Duration");
            
            if (WeaponManager.Instance != null && WeaponManager.Instance.weaponDatabase != null && WeaponManager.Instance.weaponDatabase.weapons != null)
            {
                foreach (WeaponManager.WeaponData weapon in WeaponManager.Instance.weaponDatabase.weapons)
                {
                    // Retrieve a friendly name from the prefab if available, else use type name.
                    string name = (weapon.prefab != null) ? weapon.prefab.name : weapon.type.ToString();
                    sb.AppendLine(string.Format("{0}\t{1}\t{2:0.##}\t{3}\t{4:0.##}\t{5:0.##}\t{6:0.##}",
                        name,
                        weapon.type.ToString(),
                        weapon.damage,
                        weapon.magazineSize,
                        weapon.fireRate,
                        weapon.reloadTime,
                        weapon.recoilDuration));
                }
            }
            else
            {
                sb.AppendLine("No weapon data available.");
            }
            return sb.ToString();
        }

        // Generates a table of armor stats using ArmorManager's armor database.
        public static string GenerateArmorStatsTable()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Name\tDefense\tWeight\tDurability\tRequired Level\tRarity");
            
            // Assuming ArmorManager follows a similar singleton pattern as WeaponManager.
            if (ArmorManager.Instance != null && ArmorManager.Instance.armorDatabase != null && ArmorManager.Instance.armorDatabase.armors != null)
            {
                foreach (ArmorData armor in ArmorManager.Instance.armorDatabase.armors)
                {
                    sb.AppendLine(string.Format("{0}\t{1:0.##}\t{2:0.##}\t{3:0.##}\t{4}\t{5}",
                        armor.name,
                        armor.defenseRating,
                        armor.weight,
                        armor.durability,
                        armor.requiredLevel,
                        armor.rarity));
                }
            }
            else
            {
                sb.AppendLine("No armor data available.");
            }
            return sb.ToString();
        }

        // Generates a table of item stats using ItemManager's item database.
        public static string GenerateItemStatsTable()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Name\tType\tValue\tWeight\tStackable\tMax Stack");
            
            // Assuming ItemManager follows a similar singleton pattern and provides access to item data.
            if (ItemManager.Instance != null && ItemManager.Instance.itemDatabase != null && ItemManager.Instance.itemDatabase.items != null)
            {
                foreach (ItemData item in ItemManager.Instance.itemDatabase.items)
                {
                    sb.AppendLine(string.Format("{0}\t{1}\t{2}\t{3:0.##}\t{4}\t{5}",
                        item.name,
                        item.type,
                        item.value,
                        item.weight,
                        item.stackable ? "Yes" : "No",
                        item.maxStack));
                }
            }
            else
            {
                sb.AppendLine("No item data available.");
            }
            return sb.ToString();
        }

        // Generates a table of ability stats using AbilityManager's ability database.
        public static string GenerateAbilityStatsTable()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("Name\tType\tDamage\tMana Cost\tCooldown\tRange\tAoE\tDuration");
            
            // Assuming AbilityManager follows a similar singleton pattern and provides access to ability data.
            if (AbilityManager.Instance != null && AbilityManager.Instance.abilityDatabase != null && AbilityManager.Instance.abilityDatabase.abilities != null)
            {
                foreach (AbilityData ability in AbilityManager.Instance.abilityDatabase.abilities)
                {
                    sb.AppendLine(string.Format("{0}\t{1}\t{2:0.##}\t{3:0.##}\t{4:0.##}\t{5:0.##}\t{6:0.##}\t{7:0.##}",
                        ability.name,
                        ability.type,
                        ability.damage,
                        ability.manaCost,
                        ability.cooldown,
                        ability.range,
                        ability.areaOfEffect,
                        ability.duration));
                }
            }
            else
            {
                sb.AppendLine("No ability data available.");
            }
            return sb.ToString();
        }

        // Example method to print all the generated statistics to the console.
        public void PrintStatistics()
        {
            Debug.Log(GenerateStatisticsReport());
        }
    }

    // Dummy data structures are declared below to simulate the databases for Armor, Item and Ability.
    // In the real project, these should be defined in their own files and managed by their respective managers.

    // Armor data structure for statistical reporting.
    public class ArmorData
    {
        public string name;
        public float defenseRating;
        public float weight;
        public float durability;
        public int requiredLevel;
        public string rarity;
    }

    // Item data structure for statistical reporting.
    public class ItemData
    {
        public string name;
        public string type;
        public int value;
        public float weight;
        public bool stackable;
        public int maxStack;
    }

    // Ability data structure for statistical reporting.
    public class AbilityData
    {
        public string name;
        public string type;
        public float damage;
        public float manaCost;
        public float cooldown;
        public float range;
        public float areaOfEffect;
        public float duration;
    }
}
</code>
