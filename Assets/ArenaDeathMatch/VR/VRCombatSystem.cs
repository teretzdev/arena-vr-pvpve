using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArenaDeathMatch.Abilities;
using ArenaDeathMatch.Combat;
using BNG; // VRIF by BNG
using UnityEngine.XR.Interaction.Toolkit;

namespace ArenaDeathMatch.VR
{
    public class VRCombatSystem : MonoBehaviour
    {
        [Header("Melee Combat Settings")]
        public float meleeRange = 2.0f;
        public float meleeDamage = 25.0f;
        public LayerMask meleeTargetLayer;

        [Header("Ranged Combat Settings")]
        public Transform rangedWeaponSpawnPoint;
        public GameObject projectilePrefab;
        public float projectileSpeed = 20.0f;

        [Header("Magic Combat Settings")]
        public Transform magicSpawnPoint;
        public AbilityData magicAbility;
        public float magicCooldown = 5.0f;

        [Header("References")]
        public BNGPlayerController bngPlayerController;
        public XRDirectInteractor leftHandInteractor;
        public XRDirectInteractor rightHandInteractor;

        private float lastMagicUseTime;

        private void Start()
        {
            if (bngPlayerController == null)
            {
                bngPlayerController = GetComponent<BNGPlayerController>();
                if (bngPlayerController == null)
                {
                    Debug.LogError("BNGPlayerController is not assigned or found on the GameObject.");
                }
            }

            if (rangedWeaponSpawnPoint == null)
            {
                Debug.LogError("Ranged weapon spawn point is not assigned.");
            }

            if (magicSpawnPoint == null)
            {
                Debug.LogError("Magic spawn point is not assigned.");
            }

            if (magicAbility == null)
            {
                Debug.LogError("Magic ability is not assigned.");
            }
        }

        private void Update()
        {
            HandleMeleeCombat();
            HandleRangedCombat();
            HandleMagicCombat();
        }

        private void HandleMeleeCombat()
        {
            if (bngPlayerController.GetButtonDown("RightTrigger"))
            {
                PerformMeleeAttack();
            }
        }

        private void PerformMeleeAttack()
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, meleeRange, meleeTargetLayer);
            foreach (var hitCollider in hitColliders)
            {
                if (hitCollider.TryGetComponent(out Health targetHealth))
                {
                    targetHealth.TakeDamage(meleeDamage);
                    Debug.Log($"Melee attack hit: {hitCollider.gameObject.name}, Damage: {meleeDamage}");
                }
            }
        }

        private void HandleRangedCombat()
        {
            if (bngPlayerController.GetButtonDown("LeftTrigger"))
            {
                PerformRangedAttack();
            }
        }

        private void PerformRangedAttack()
        {
            if (projectilePrefab == null || rangedWeaponSpawnPoint == null)
            {
                Debug.LogError("Projectile prefab or spawn point is not assigned.");
                return;
            }

            GameObject projectile = Instantiate(projectilePrefab, rangedWeaponSpawnPoint.position, rangedWeaponSpawnPoint.rotation);
            if (projectile.TryGetComponent(out Rigidbody rb))
            {
                rb.velocity = rangedWeaponSpawnPoint.forward * projectileSpeed;
                Debug.Log($"Ranged attack fired: {projectile.name}, Speed: {projectileSpeed}");
            }
        }

        private void HandleMagicCombat()
        {
            if (bngPlayerController.GetButtonDown("AButton") && Time.time >= lastMagicUseTime + magicCooldown)
            {
                PerformMagicAttack();
                lastMagicUseTime = Time.time;
            }
        }

        private void PerformMagicAttack()
        {
            if (magicAbility == null || magicSpawnPoint == null)
            {
                Debug.LogError("Magic ability or spawn point is not assigned.");
                return;
            }

            GameObject magicEffect = Instantiate(magicAbility.visualEffect, magicSpawnPoint.position, magicSpawnPoint.rotation);
            if (magicEffect.TryGetComponent(out Rigidbody rb))
            {
                rb.velocity = magicSpawnPoint.forward * magicAbility.range;
            }

            Debug.Log($"Magic attack performed: {magicAbility.abilityName}, Damage: {magicAbility.damage}, Range: {magicAbility.range}");
        }

        public void EquipWeapon(GameObject weapon)
        {
            if (weapon.TryGetComponent(out WeaponData weaponData))
            {
                Debug.Log($"Equipped weapon: {weaponData.type}, Damage: {weaponData.damage}");
            }
            else
            {
                Debug.LogError("The object does not contain WeaponData.");
            }
        }

        public void UnequipWeapon(GameObject weapon)
        {
            Debug.Log($"Unequipped weapon: {weapon.name}");
        }
    }
}