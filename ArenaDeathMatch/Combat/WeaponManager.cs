using System;
using System.Collections.Generic;

namespace ArenaDeathMatch.Combat
{
    public class WeaponManager
    {
        private List<VRWeapon> weapons;

        public WeaponManager()
        {
            weapons = new List<VRWeapon>();
        }

        public void AddWeapon(VRWeapon weapon)
        {
            weapons.Add(weapon);
        }

        public void RemoveWeapon(VRWeapon weapon)
        {
            weapons.Remove(weapon);
        }

        public void UpdateWeapons()
        {
            foreach (var weapon in weapons)
            {
                weapon.Update();
            }
        }
    }

    public class VRWeapon
    {
        public string Name { get; private set; }
        public int AmmoCount { get; private set; }

        public VRWeapon(string name, int ammoCount)
        {
            Name = name;
            AmmoCount = ammoCount;
        }

        public void Fire()
        {
            if (AmmoCount > 0)
            {
                AmmoCount--;
                Console.WriteLine($"{Name} fired. Remaining ammo: {AmmoCount}");
            }
            else
            {
                Console.WriteLine($"{Name} is out of ammo!");
            }
        }

        public void Reload(int ammo)
        {
            AmmoCount += ammo;
            Console.WriteLine($"{Name} reloaded. Total ammo: {AmmoCount}");
        }

        public void Update()
        {
            // Update weapon state
        }
    }

    public class Bullet
    {
        public float Speed { get; private set; }
        public float Damage { get; private set; }

        public Bullet(float speed, float damage)
        {
            Speed = speed;
            Damage = damage;
        }

        public void OnHit()
        {
            Console.WriteLine($"Bullet hit with damage: {Damage}");
        }
    }

    public class ImpactManager
    {
        public void HandleImpact(Bullet bullet)
        {
            bullet.OnHit();
            // Additional impact handling logic
        }
    }
}
