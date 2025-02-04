<code>
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using ArenaDeathMatch.Combat;
using ArenaDeathMatch.Testing;

namespace ArenaDeathMatch.Testing
{
    public class WeaponManagerTests
    {
        private GameObject weaponManagerObject;
        private WeaponManager weaponManager;
        private DummyWeaponDatabase dummyDatabase;
        private GameObject dummyWeaponPrefab;

        // A dummy implementation for WeaponDatabase as expected by WeaponManager.
        private class DummyWeaponDatabase
        {
            public List<WeaponManager.WeaponData> weapons = new List<WeaponManager.WeaponData>();
        }

        [SetUp]
        public void SetUp()
        {
            // Create a GameObject for WeaponManager and add its component.
            weaponManagerObject = new GameObject("WeaponManager_Test");
            weaponManager = weaponManagerObject.AddComponent<WeaponManager>();

            // Create a dummy prefab with a VRWeapon component.
            dummyWeaponPrefab = new GameObject("DummyWeaponPrefab");
            // Add the VRWeapon component (as defined in WeaponManager) for simulation.
            dummyWeaponPrefab.AddComponent<WeaponManager.VRWeapon>();
            // Disable the prefab in hierarchy so it is not active.
            dummyWeaponPrefab.SetActive(false);
            
            // Create a dummy weapon database and assign it to WeaponManager.
            dummyDatabase = new DummyWeaponDatabase();

            // Create a dummy WeaponData for testing.
            WeaponManager.WeaponData testWeaponData = new WeaponManager.WeaponData();
            testWeaponData.type = WeaponManager.WeaponType.Pistol;
            testWeaponData.prefab = dummyWeaponPrefab;
            testWeaponData.magazineSize = 10;
            testWeaponData.reloadTime = 1.0f;
            testWeaponData.damage = 25f;
            // Provide dummy curves for recoil (using AnimationCurve.Linear for simplicity)
            testWeaponData.recoilDuration = 0.5f;
            testWeaponData.recoilCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
            testWeaponData.recoilRotationCurve = AnimationCurve.Linear(0f, 0f, 1f, 1f);
            // Add the dummy weapon data to the database.
            dummyDatabase.weapons.Add(testWeaponData);

            // Since WeaponManager expects a WeaponDatabase, we assign our dummy database via reflection.
            // For testing purposes we directly assign the weapons list.
            weaponManager.weaponDatabase = ScriptableObject.CreateInstance<DummyWeaponDatabaseScriptable>();
            weaponManager.weaponDatabase.weapons = dummyDatabase.weapons;

            // Call Awake manually to initialize weapon cache.
            weaponManager.InitializeWeapons();
        }

        [TearDown]
        public void TearDown()
        {
            Object.DestroyImmediate(weaponManagerObject);
            Object.DestroyImmediate(dummyWeaponPrefab);
        }

        [Test]
        public void TestWeaponFiring_DecreasesAmmo()
        {
            // Spawn the dummy weapon using the WeaponManager.
            Vector3 spawnPos = Vector3.zero;
            Quaternion spawnRot = Quaternion.identity;
            VRWeapon weapon = weaponManager.SpawnWeapon(WeaponManager.WeaponType.Pistol, spawnPos, spawnRot) as VRWeapon;
            Assert.IsNotNull(weapon, "Spawned weapon should not be null");

            int initialAmmo = weapon.currentAmmo;
            // Call Fire and check if ammo decreases
            weapon.Fire();
            Assert.AreEqual(initialAmmo - 1, weapon.currentAmmo, "Ammo should decrease by one after firing");
        }

        [Test]
        public void TestMagicWeapon_SpawnAndCastSpell()
        {
            // Spawn a MagicWeapon using the WeaponManager.
            Vector3 spawnPos = Vector3.zero;
            Quaternion spawnRot = Quaternion.identity;
            MagicWeapon magicWeapon = weaponManager.SpawnWeapon(WeaponManager.WeaponType.MagicWeapon, spawnPos, spawnRot) as MagicWeapon;
            Assert.IsNotNull(magicWeapon, "Spawned MagicWeapon should not be null");

            // Simulate casting a spell.
            Vector3 targetPosition = new Vector3(1, 0, 0);
            magicWeapon.CastSpell(targetPosition);

            // Verify that the spell casting logic works (e.g., cooldown is triggered).
            Assert.IsTrue(magicWeapon.isOnCooldown, "MagicWeapon should be on cooldown after casting a spell");
        }

        [Test]
        public void TestThunderGrenade_SpawnAndExplode()
        {
            // Spawn a ThunderGrenade using the WeaponManager.
            Vector3 spawnPos = Vector3.zero;
            Quaternion spawnRot = Quaternion.identity;
            ThunderGrenade thunderGrenade = weaponManager.SpawnWeapon(WeaponManager.WeaponType.ThunderGrenade, spawnPos, spawnRot) as ThunderGrenade;
            Assert.IsNotNull(thunderGrenade, "Spawned ThunderGrenade should not be null");

            // Simulate grenade explosion.
            thunderGrenade.Explode();

            // Verify that the explosion logic works (e.g., damage is applied).
            Assert.IsTrue(thunderGrenade.HasExploded, "ThunderGrenade should have exploded");
        }

        [UnityTest]
        public IEnumerator TestWeaponReloading_RestoresAmmo()
        {
            // Spawn the dummy weapon.
            Vector3 spawnPos = Vector3.zero;
            Quaternion spawnRot = Quaternion.identity;
            VRWeapon weapon = weaponManager.SpawnWeapon(WeaponManager.WeaponType.Pistol, spawnPos, spawnRot) as VRWeapon;
            Assert.IsNotNull(weapon, "Spawned weapon should not be null");

            // Simulate firing all rounds.
            for (int i = 0; i < weapon.currentAmmo; i++)
            {
                weapon.Fire();
            }
            Assert.AreEqual(0, weapon.currentAmmo, "Ammo should be zero after firing all rounds");

            // Call Reload; assuming the Reload() method is implemented as per diff.
            weapon.Reload();
            // Wait for longer than reloadTime (plus a small delta to allow coroutine to complete).
            yield return new WaitForSeconds(weaponManager.weaponDatabase.weapons[0].reloadTime + 0.2f);

            Assert.AreEqual(weaponManager.weaponDatabase.weapons[0].magazineSize, weapon.currentAmmo, "Ammo should be restored to magazine size after reloading");
        }

        [Test]
        public void TestWeaponSwitching_ActiveWeaponsListUpdates()
        {
            // Spawn first weapon.
            Vector3 spawnPos = new Vector3(0, 0, 0);
            Quaternion spawnRot = Quaternion.identity;
            VRWeapon weapon1 = weaponManager.SpawnWeapon(WeaponManager.WeaponType.Pistol, spawnPos, spawnRot) as VRWeapon;
            Assert.IsNotNull(weapon1, "First spawned weapon should not be null");

            // Spawn second weapon (simulate switching by spawning another weapon of same type for test purposes).
            Vector3 spawnPos2 = new Vector3(1, 0, 0);
            VRWeapon weapon2 = weaponManager.SpawnWeapon(WeaponManager.WeaponType.Pistol, spawnPos2, spawnRot) as VRWeapon;
            Assert.IsNotNull(weapon2, "Second spawned weapon should not be null");

            // Check that the activeWeapons list contains both weapons.
            Assert.IsTrue(weaponManager.activeWeapons.Contains(weapon1), "Active weapons should include the first spawned weapon");
            Assert.IsTrue(weaponManager.activeWeapons.Contains(weapon2), "Active weapons should include the second spawned weapon");

            // Simulate switching by destroying the first weapon.
            weaponManager.DestroyWeapon(weapon1);
            Assert.IsFalse(weaponManager.activeWeapons.Contains(weapon1), "Active weapons should not include the destroyed weapon");
            Assert.IsTrue(weaponManager.activeWeapons.Contains(weapon2), "Active weapons should still include the second spawned weapon");
        }
    }

    // For testing purposes, we create a simple ScriptableObject version of the WeaponDatabase.
    public class DummyWeaponDatabaseScriptable : ScriptableObject
    {
        public List<WeaponManager.WeaponData> weapons;
    }
}
</code>