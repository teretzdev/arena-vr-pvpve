using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArenaDeathMatch.Combat
{
    public abstract class SpecialWeapon : MonoBehaviour
    {
        public string weaponName;
        public float cooldownTime;
        public AudioClip activationSound;
        public ParticleSystem activationEffect;

        protected bool isOnCooldown;

        public virtual void Activate(Vector3 targetPosition)
        {
            if (isOnCooldown)
            {
                Debug.Log($"{weaponName} is on cooldown.");
                return;
            }

            if (activationSound != null)
            {
                AudioSource.PlayClipAtPoint(activationSound, transform.position);
            }

            if (activationEffect != null)
            {
                Instantiate(activationEffect, targetPosition, Quaternion.identity);
            }

            StartCoroutine(CooldownRoutine());
        }

        private IEnumerator CooldownRoutine()
        {
            isOnCooldown = true;
            yield return new WaitForSeconds(cooldownTime);
            isOnCooldown = false;
        }
    }

    public class ThunderGrenade : SpecialWeapon
    {
        public float explosionRadius;
        public float explosionDamage;
        public GameObject explosionPrefab;

        public override void Activate(Vector3 targetPosition)
        {
            base.Activate(targetPosition);
            Explode(targetPosition);
        }

        private void Explode(Vector3 position)
        {
            Collider[] hitColliders = Physics.OverlapSphere(position, explosionRadius);
            foreach (var hitCollider in hitColliders)
            {
                IDamageable damageable = hitCollider.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    damageable.TakeDamage(new DamageInfo { amount = explosionDamage, point = position });
                }
            }

            if (explosionPrefab != null)
            {
                Instantiate(explosionPrefab, position, Quaternion.identity);
            }
        }
    }

    // Additional special weapons can be implemented here following the same pattern.
}
