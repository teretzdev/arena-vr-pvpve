using UnityEngine;

namespace ArenaDeathMatch.Combat
{
    public abstract class RangedWeapon : MonoBehaviour
    {
        public int AmmoCount { get; protected set; }
        public float Range { get; protected set; }
        public float Damage { get; protected set; }

        protected virtual void Awake()
        {
            AmmoCount = 0;
            Range = 0f;
            Damage = 0f;
        }

        public abstract void Aim();

        public virtual void Shoot()
        {
            if (AmmoCount > 0)
            {
                AmmoCount--;
                Debug.Log($"{GetType().Name} fired. Remaining ammo: {AmmoCount}");
            }
            else
            {
                Debug.Log($"{GetType().Name} is out of ammo!");
            }
        }

        public virtual void Reload(int ammo)
        {
            AmmoCount += ammo;
            Debug.Log($"{GetType().Name} reloaded. Current ammo: {AmmoCount}");
        }
    }

    public class Bow : RangedWeapon
    {
        protected override void Awake()
        {
            base.Awake();
            AmmoCount = 10;
            Range = 50f;
            Damage = 15f;
        }

        public override void Aim()
        {
            Debug.Log("Aiming with the Bow.");
        }

        public override void Shoot()
        {
            base.Shoot();
            if (AmmoCount >= 0)
            {
                // Implement specific shooting logic for Bow
                Debug.Log("Arrow released from the Bow.");
            }
        }
    }

    public class Crossbow : RangedWeapon
    {
        protected override void Awake()
        {
            base.Awake();
            AmmoCount = 5;
            Range = 70f;
            Damage = 25f;
        }

        public override void Aim()
        {
            Debug.Log("Aiming with the Crossbow.");
        }

        public override void Shoot()
        {
            base.Shoot();
            if (AmmoCount >= 0)
            {
                // Implement specific shooting logic for Crossbow
                Debug.Log("Bolt fired from the Crossbow.");
            }
        }
    }

    public class Gun : RangedWeapon
    {
        protected override void Awake()
        {
            base.Awake();
            AmmoCount = 30;
            Range = 100f;
            Damage = 20f;
        }

        public override void Aim()
        {
            Debug.Log("Aiming with the Gun.");
        }

        public override void Shoot()
        {
            base.Shoot();
            if (AmmoCount >= 0)
            {
                // Implement specific shooting logic for Gun
                Debug.Log("Bullet fired from the Gun.");
            }
        }
    }
}
