using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArenaDeathMatch.Combat
{
    public abstract class MagicWeapon : MonoBehaviour
    {
        public string weaponName;
        public float manaCost;
        public float cooldownTime;
        public AudioClip castSound;
        public ParticleSystem castEffect;

        protected bool isOnCooldown;

        public virtual void CastSpell(Vector3 targetPosition)
        {
            if (isOnCooldown)
            {
                Debug.Log($"{weaponName} is on cooldown.");
                return;
            }

            if (castSound != null)
            {
                AudioSource.PlayClipAtPoint(castSound, transform.position);
            }

            if (castEffect != null)
            {
                Instantiate(castEffect, targetPosition, Quaternion.identity);
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

    public class NecromanticStaff : MagicWeapon
    {
        public int summonCount;
        public GameObject summonPrefab;

        public override void CastSpell(Vector3 targetPosition)
        {
            base.CastSpell(targetPosition);
            SummonMinions(targetPosition);
        }

        private void SummonMinions(Vector3 position)
        {
            for (int i = 0; i < summonCount; i++)
            {
                Vector3 summonPosition = position + Random.insideUnitSphere * 2f;
                Instantiate(summonPrefab, summonPosition, Quaternion.identity);
            }
        }
    }

    public class HydraStaff : MagicWeapon
    {
        public float poisonDuration;
        public float poisonDamage;

        public override void CastSpell(Vector3 targetPosition)
        {
            base.CastSpell(targetPosition);
            ApplyPoisonEffect(targetPosition);
        }

        private void ApplyPoisonEffect(Vector3 position)
        {
            Collider[] hitColliders = Physics.OverlapSphere(position, 5f);
            foreach (var hitCollider in hitColliders)
            {
                IDamageable damageable = hitCollider.GetComponent<IDamageable>();
                if (damageable != null)
                {
                    StartCoroutine(ApplyPoison(damageable));
                }
            }
        }

        private IEnumerator ApplyPoison(IDamageable target)
        {
            float elapsed = 0f;
            while (elapsed < poisonDuration)
            {
                target.TakeDamage(new DamageInfo { amount = poisonDamage, point = target.transform.position });
                elapsed += 1f;
                yield return new WaitForSeconds(1f);
            }
        }
    }

    public class IdolStaff : MagicWeapon
    {
        public float shieldDuration;
        public GameObject shieldPrefab;

        public override void CastSpell(Vector3 targetPosition)
        {
            base.CastSpell(targetPosition);
            ActivateShield(targetPosition);
        }

        private void ActivateShield(Vector3 position)
        {
            GameObject shield = Instantiate(shieldPrefab, position, Quaternion.identity);
            Destroy(shield, shieldDuration);
        }
    }
}
