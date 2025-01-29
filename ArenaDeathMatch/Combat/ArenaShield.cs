using UnityEngine;

namespace ArenaDeathMatch.Combat
{
    public abstract class Shield : MonoBehaviour
    {
        public float Durability { get; protected set; }
        public float RechargeRate { get; protected set; }
        public bool IsActive { get; private set; }

        protected virtual void Awake()
        {
            IsActive = false;
        }

        public virtual void Activate()
        {
            IsActive = true;
            Debug.Log($"{GetType().Name} activated.");
        }

        public virtual void Deactivate()
        {
            IsActive = false;
            Debug.Log($"{GetType().Name} deactivated.");
        }

        public abstract void AbsorbDamage(float damage);
    }

    public class EnergyShield : Shield
    {
        public float EnergyCapacity { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            Durability = 100f;
            RechargeRate = 5f;
            EnergyCapacity = 50f;
        }

        public override void AbsorbDamage(float damage)
        {
            if (!IsActive) return;

            float absorbed = Mathf.Min(damage, EnergyCapacity);
            EnergyCapacity -= absorbed;
            Durability -= (damage - absorbed);

            Debug.Log($"{GetType().Name} absorbed {absorbed} damage. Remaining energy: {EnergyCapacity}, Durability: {Durability}");

            if (Durability <= 0)
            {
                Deactivate();
            }
        }
    }

    public class ReflectiveShield : Shield
    {
        public float ReflectionRate { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            Durability = 80f;
            RechargeRate = 3f;
            ReflectionRate = 0.3f;
        }

        public override void AbsorbDamage(float damage)
        {
            if (!IsActive) return;

            float reflectedDamage = damage * ReflectionRate;
            Durability -= (damage - reflectedDamage);

            Debug.Log($"{GetType().Name} reflected {reflectedDamage} damage. Durability: {Durability}");

            if (Durability <= 0)
            {
                Deactivate();
            }
        }
    }

    public class AbsorbShield : Shield
    {
        public float AbsorptionRate { get; private set; }

        protected override void Awake()
        {
            base.Awake();
            Durability = 120f;
            RechargeRate = 4f;
            AbsorptionRate = 0.5f;
        }

        public override void AbsorbDamage(float damage)
        {
            if (!IsActive) return;

            float absorbed = damage * AbsorptionRate;
            Durability -= (damage - absorbed);

            Debug.Log($"{GetType().Name} absorbed {absorbed} damage. Durability: {Durability}");

            if (Durability <= 0)
            {
                Deactivate();
            }
        }
    }
}
