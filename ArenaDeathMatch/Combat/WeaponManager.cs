using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ArenaDeathMatch.Combat;
using ArenaDeathMatch.Combat.ArenaShield;
using ArenaDeathMatch.Combat.ArenaRangedWeapon;
using ArenaDeathMatch.Combat.ArenaMeleeWeapon;

namespace ArenaDeathMatch.Combat
{
    public class WeaponManager : MonoBehaviour
    {
       
            else if (type == WeaponType.Trimmer || type == WeaponType.CleanTrimmer)
            {
                MeleeWeapon trimmer = weaponObj.GetComponent<MeleeWeapon>();
                if (trimmer == null)
                {
                    Debug.LogError($"MeleeWeapon component missing on prefab for {type}");
                    Destroy(weaponObj);
                    return null;
                }
                // Initialize trimmer specific properties if needed
                activeWeapons.Add(trimmer);
                return trimmer;
            }
            else if (type == WeaponType.MetalBat)
            {
                MeleeWeapon metalBat = weaponObj.GetComponent<MeleeWeapon>();
                if (metalBat == null)
                {
                    Debug.LogError($"MeleeWeapon component missing on prefab for {type}");
                    Destroy(weaponObj);
                    return null;
                }
                // Initialize metal bat specific properties if needed
                activeWeapons.Add(metalBat);
                return metalBat;
            }
            else if (type == WeaponType.RocketLauncher)
            {
                RangedWeapon rocketLauncher = weaponObj.GetComponent<RangedWeapon>();
                if (rocketLauncher == null)
                {
                    Debug.LogError($"RangedWeapon component missing on prefab for {type}");
                    Destroy(weaponObj);
                    return null;
                }
                // Initialize rocket launcher specific properties if needed
                activeWeapons.Add(rocketLauncher);
                return rocketLauncher;
            } public static WeaponManager Instance { get; private set; }

        [Header("Weapon Configuration")]
        public WeaponDatabase weaponDatabase;
        public Transform weaponContainer;
        public VRGrabSystem grabSystem;

        [Header("Physics Settings")]
        public PhysicsSettings physicsSettings;
        public BulletManager bulletManager;
        public ImpactManager impactManager;

        private Dictionary<WeaponType, WeaponData> weaponCache = new Dictionary<WeaponType, WeaponData>();
        private List<VRWeapon> activeWeapons = new List<VRWeapon>();

        [System.Serializable]
        public class PhysicsSettings
        {
            public LayerMask collisionLayers;
            public float bulletSpeed = 100f;
            public float bulletLifetime = 5f;
            public bool useAdvancedBallistics = true;
            public float gravityMultiplier = 1f;
            public float airResistance = 0.1f;
        }

        private void Awake()
        {
            Instance = this;
            InitializeWeapons();
        }

        #region Weapon Management

        private void InitializeWeapons()
        {
            foreach (WeaponData weapon in weaponDatabase.weapons)
            {
                weaponCache[weapon.type] = weapon;
            }

            // Add new weapons to the weapon cache
            weaponCache[WeaponType.Trimmer] = weaponDatabase.weapons.Find(w => w.type == WeaponType.Trimmer);
            weaponCache[WeaponType.CleanTrimmer] = weaponDatabase.weapons.Find(w => w.type == WeaponType.CleanTrimmer);
            weaponCache[WeaponType.MetalBat] = weaponDatabase.weapons.Find(w => w.type == WeaponType.MetalBat);
            weaponCache[WeaponType.RocketLauncher] = weaponDatabase.weapons.Find(w => w.type == WeaponType.RocketLauncher);

            // Add new weapons to the weapon cache
            weaponCache[WeaponType.GasCan] = weaponDatabase.weapons.Find(w => w.type == WeaponType.GasCan);
            weaponCache[WeaponType.PropaneTank] = weaponDatabase.weapons.Find(w => w.type == WeaponType.PropaneTank);
            weaponCache[WeaponType.Chainsaw] = weaponDatabase.weapons.Find(w => w.type == WeaponType.Chainsaw);
            weaponCache[WeaponType.Crossbow] = weaponDatabase.weapons.Find(w => w.type == WeaponType.Crossbow);
            weaponCache[WeaponType.CleanCrossbow] = weaponDatabase.weapons.Find(w => w.type == WeaponType.CleanCrossbow);
            weaponCache[WeaponType.MetalBat] = weaponDatabase.weapons.Find(w => w.type == WeaponType.MetalBat);
            weaponCache[WeaponType.WoodenBat] = weaponDatabase.weapons.Find(w => w.type == WeaponType.WoodenBat);
            
            weaponCache[WeaponType.AssaultRifle] = weaponDatabase.weapons.Find(w => w.type == WeaponType.AssaultRifle);
            weaponCache[WeaponType.SniperRifle] = weaponDatabase.weapons.Find(w => w.type == WeaponType.SniperRifle);
            weaponCache[WeaponType.SubmachineGun] = weaponDatabase.weapons.Find(w => w.type == WeaponType.SubmachineGun);
            weaponCache[WeaponType.Nailgun] = weaponDatabase.weapons.Find(w => w.type == WeaponType.Nailgun);
            weaponCache[WeaponType.RocketLauncher] = weaponDatabase.weapons.Find(w => w.type == WeaponType.RocketLauncher);
            weaponCache[WeaponType.HuntingRifle] = weaponDatabase.weapons.Find(w => w.type == WeaponType.HuntingRifle);
            weaponCache[WeaponType.Crossbow] = weaponDatabase.weapons.Find(w => w.type == WeaponType.Crossbow);
            weaponCache[WeaponType.CleanCrossbow] = weaponDatabase.weapons.Find(w => w.type == WeaponType.CleanCrossbow);
            weaponCache[WeaponType.MetalBat] = weaponDatabase.weapons.Find(w => w.type == WeaponType.MetalBat);
            weaponCache[WeaponType.WoodenBat] = weaponDatabase.weapons.Find(w => w.type == WeaponType.WoodenBat);
            weaponCache[WeaponType.Machete] = weaponDatabase.weapons.Find(w => w.type == WeaponType.Machete);
            weaponCache[WeaponType.Katana] = weaponDatabase.weapons.Find(w => w.type == WeaponType.Katana);
            weaponCache[WeaponType.FireAxe] = weaponDatabase.weapons.Find(w => w.type == WeaponType.FireAxe);
            weaponCache[WeaponType.PipeWrench] = weaponDatabase.weapons.Find(w => w.type == WeaponType.PipeWrench);
            weaponCache[WeaponType.GolfClub] = weaponDatabase.weapons.Find(w => w.type == WeaponType.GolfClub);
            weaponCache[WeaponType.Spear] = weaponDatabase.weapons.Find(w => w.type == WeaponType.Spear);

            bulletManager.Initialize(physicsSettings);
            impactManager.Initialize();
        }

        public VRWeapon SpawnWeapon(WeaponType type, Vector3 position, Quaternion rotation)
        {
            if (!weaponCache.ContainsKey(type))
                return null;

            WeaponData data = weaponCache[type];
            GameObject weaponObj = Instantiate(data.prefab, position, rotation, weaponContainer);
            if (type == WeaponType.MagicWeapon)
            {
                MagicWeapon magicWeapon = weaponObj.GetComponent<MagicWeapon>();
                if (magicWeapon == null)
                {
                    Debug.LogError($"MagicWeapon component missing on prefab for {type}");
                    Destroy(weaponObj);
                    return null;
                }
                // Initialize magic weapon specific properties if needed
                activeWeapons.Add(magicWeapon);
                return magicWeapon;
            }
            else if (type == WeaponType.ThunderGrenade)
            {
                ThunderGrenade specialWeapon = weaponObj.GetComponent<ThunderGrenade>();
                if (specialWeapon == null)
                {
                    Debug.LogError($"ThunderGrenade component missing on prefab for {type}");
                    Destroy(weaponObj);
                    return null;
                }
                // Initialize special weapon specific properties if needed
                activeWeapons.Add(specialWeapon);
                return specialWeapon;
            }
            else if (type == WeaponType.Shield)
            {
                Shield shield = weaponObj.GetComponent<Shield>();
                if (shield == null)
                {
                    Debug.LogError($"Shield component missing on prefab for {type}");
                    Destroy(weaponObj);
                    return null;
                }
                // Initialize shield specific properties if needed
                activeWeapons.Add(shield);
                return shield;
            }
            else if (type == WeaponType.RangedWeapon)
            {
                RangedWeapon rangedWeapon = weaponObj.GetComponent<RangedWeapon>();
                if (rangedWeapon == null)
                {
                    Debug.LogError($"RangedWeapon component missing on prefab for {type}");
                    Destroy(weaponObj);
                    return null;
                }
                // Initialize ranged weapon specific properties if needed
                activeWeapons.Add(rangedWeapon);
                return rangedWeapon;
            }
            else if (type == WeaponType.MeleeWeapon)
            {
                MeleeWeapon meleeWeapon = weaponObj.GetComponent<MeleeWeapon>();
                if (meleeWeapon == null)
                {
                    Debug.LogError($"MeleeWeapon component missing on prefab for {type}");
                    Destroy(weaponObj);
                    return null;
                }
                // Initialize melee weapon specific properties if needed
                activeWeapons.Add(meleeWeapon);
                return meleeWeapon;
            }
            else if (type == WeaponType.GasCan || type == WeaponType.PropaneTank)
            {
                VRWeapon explosiveWeapon = weaponObj.GetComponent<VRWeapon>();
                if (explosiveWeapon == null)
                {
                    Debug.LogError($"VRWeapon component missing on prefab for {type}");
                    Destroy(weaponObj);
                    return null;
                }
                // Initialize explosive weapon specific properties if needed
                activeWeapons.Add(explosiveWeapon);
                return explosiveWeapon;
            }
            else if (type == WeaponType.Chainsaw)
            {
                MeleeWeapon chainsaw = weaponObj.GetComponent<MeleeWeapon>();
                if (chainsaw == null)
                {
                    Debug.LogError($"MeleeWeapon component missing on prefab for {type}");
                    Destroy(weaponObj);
                    return null;
                }
                // Initialize chainsaw specific properties if needed
                activeWeapons.Add(chainsaw);
                return chainsaw;
            }
            else if (type == WeaponType.Crossbow || type == WeaponType.CleanCrossbow)
            {
                RangedWeapon crossbow = weaponObj.GetComponent<RangedWeapon>();
                if (crossbow == null)
                {
                    Debug.LogError($"RangedWeapon component missing on prefab for {type}");
                    Destroy(weaponObj);
                    return null;
                }
                // Initialize crossbow specific properties if needed
                activeWeapons.Add(crossbow);
                return crossbow;
            }
            else if (type == WeaponType.MetalBat || type == WeaponType.WoodenBat)
            {
                MeleeWeapon bat = weaponObj.GetComponent<MeleeWeapon>();
                if (bat == null)
                {
                    Debug.LogError($"MeleeWeapon component missing on prefab for {type}");
                    Destroy(weaponObj);
                    return null;
                }
                // Initialize bat specific properties if needed
                activeWeapons.Add(bat);
                return bat;
            }
            else if (type == WeaponType.AssaultRifle || type == WeaponType.SniperRifle || type == WeaponType.SubmachineGun ||
                     type == WeaponType.Nailgun || type == WeaponType.RocketLauncher || type == WeaponType.HuntingRifle ||
                     type == WeaponType.Crossbow || type == WeaponType.CleanCrossbow)
            {
                RangedWeapon rangedWeapon = weaponObj.GetComponent<RangedWeapon>();
                if (rangedWeapon == null)
                {
                    Debug.LogError($"RangedWeapon component missing on prefab for {type}");
                    Destroy(weaponObj);
                    return null;
                }
                activeWeapons.Add(rangedWeapon);
                return rangedWeapon;
            }
            else if (type == WeaponType.MetalBat || type == WeaponType.WoodenBat || type == WeaponType.Machete ||
                     type == WeaponType.Katana || type == WeaponType.FireAxe || type == WeaponType.PipeWrench ||
                     type == WeaponType.GolfClub || type == WeaponType.Spear)
            {
                MeleeWeapon meleeWeapon = weaponObj.GetComponent<MeleeWeapon>();
                if (meleeWeapon == null)
                {
                    Debug.LogError($"MeleeWeapon component missing on prefab for {type}");
                    Destroy(weaponObj);
                    return null;
                }
                activeWeapons.Add(meleeWeapon);
                return meleeWeapon;
            }
            else
            {
                VRWeapon weapon = weaponObj.GetComponent<VRWeapon>();
                weapon.Initialize(data);
                activeWeapons.Add(weapon);
                return weapon;
            }
        }

        public void DestroyWeapon(VRWeapon weapon)
        {
            activeWeapons.Remove(weapon);
            Destroy(weapon.gameObject);
        }
        // Provides access to all weapon data for generating statistical tables.
        public List<WeaponData> GetAllWeaponData()
        {
            if (weaponDatabase != null && weaponDatabase.weapons != null)
                return weaponDatabase.weapons;
            return new List<WeaponData>();
        }
        #endregion

        #region Weapon Classes
        public class VRWeapon : MonoBehaviour, IVRGrabbable
        {
            [Header("Weapon Components")]
            public WeaponType type;
            public Transform muzzle;
            public Transform ejectionPort;
            public Transform magazineSlot;
            public VRGrabbable grabbable;
            
            [Header("Weapon State")]
            public WeaponState currentState;
            public int currentAmmo;
            public bool isReloading;
            
            private WeaponData data;
            private Queue<Bullet> bulletPool = new Queue<Bullet>();

            public void Initialize(WeaponData weaponData)
            {
                data = weaponData;
                currentAmmo = data.magazineSize;
                SetupInteractions();
                InitializeBulletPool();
            }

            public void Fire()
            {
                if (!CanFire())
                    return;

                ProcessShot();
                UpdateAmmo();
                PlayEffects();
                if(AudioSystem.Instance != null) { AudioSystem.Instance.eventManager.TriggerEvent("weapon_fire", muzzle.position); }
                ApplyRecoil();
            }
            
            public void Reload()
            {
                if (isReloading)
                    return;
                isReloading = true;
                if(AudioSystem.Instance != null) { AudioSystem.Instance.eventManager.TriggerEvent("weapon_reload", transform.position); }
                StartCoroutine(ReloadCoroutine());
            }
            
            private IEnumerator ReloadCoroutine()
            {
                yield return new WaitForSeconds(data.reloadTime);
                currentAmmo = data.magazineSize;
                isReloading = false;
            }
            
            private bool CanFire()
            {
                return currentAmmo > 0 && 
                       !isReloading && 
                       currentState == WeaponState.Ready;
            }

            private void ProcessShot()
            {
                Bullet bullet = GetBulletFromPool();
                bullet.Launch(muzzle.position, muzzle.forward, data.damage);
                
                if (WeaponManager.Instance.physicsSettings.useAdvancedBallistics)
                {
                    bullet.EnableAdvancedBallistics(
                        WeaponManager.Instance.physicsSettings.gravityMultiplier,
                        WeaponManager.Instance.physicsSettings.airResistance
                    );
                }
            }

            private void ApplyRecoil()
            {
                Vector3 recoilForce = CalculateRecoilForce();
                StartCoroutine(RecoilCoroutine(recoilForce));
            }

            private IEnumerator RecoilCoroutine(Vector3 force)
            {
                float elapsed = 0f;
                Vector3 originalPosition = transform.localPosition;
                Quaternion originalRotation = transform.localRotation;

                while (elapsed < data.recoilDuration)
                {
                    elapsed += Time.deltaTime;
                    float t = elapsed / data.recoilDuration;

                    // Apply position recoil
                    Vector3 recoilPosition = originalPosition + force * data.recoilCurve.Evaluate(t);
                    transform.localPosition = recoilPosition;

                    // Apply rotational recoil
                    Vector3 recoilRotation = force * data.recoilRotationCurve.Evaluate(t);
                    transform.localRotation = originalRotation * Quaternion.Euler(recoilRotation);

                    yield return null;
                }

                // Reset position and rotation
                transform.localPosition = originalPosition;
                transform.localRotation = originalRotation;
            }
        }

        public class Bullet : MonoBehaviour
        {
            private Rigidbody rb;
            private TrailRenderer trail;
            private float damage;
            private bool isActive;
            private Vector3 previousPosition;

            private void Awake()
            {
                rb = GetComponent<Rigidbody>();
                trail = GetComponent<TrailRenderer>();
            }

            public void Launch(Vector3 position, Vector3 direction, float damageAmount)
            {
                transform.position = position;
                transform.forward = direction;
                damage = damageAmount;
                isActive = true;
                previousPosition = position;

                rb.velocity = direction * WeaponManager.Instance.physicsSettings.bulletSpeed;
                if (trail) trail.Clear();

                StartCoroutine(DeactivateAfterTime());
            }

            private void FixedUpdate()
            {
                if (!isActive) return;

                // Perform raycast between previous and current position to prevent tunneling
                Vector3 direction = (transform.position - previousPosition).normalized;
                float distance = Vector3.Distance(transform.position, previousPosition);

                if (Physics.Raycast(previousPosition, direction, out RaycastHit hit, distance, 
                    WeaponManager.Instance.physicsSettings.collisionLayers))
                {
                    HandleImpact(hit);
                }

                previousPosition = transform.position;
            }

            private void HandleImpact(RaycastHit hit)
            {
                // Process damage
                IDamageable damageable = hit.collider.GetComponent<IDamageable>();
                damageable?.TakeDamage(new DamageInfo
                {
                    amount = damage,
                    point = hit.point,
                    normal = hit.normal,
                    source = transform.position
                });

                // Spawn impact effects
                WeaponManager.Instance.impactManager.SpawnImpact(hit);

                // Deactivate bullet
                Deactivate();
            }

            public void EnableAdvancedBallistics(float gravityMult, float airResistance)
            {
                StartCoroutine(AdvancedBallisticsCoroutine(gravityMult, airResistance));
            }

            private IEnumerator AdvancedBallisticsCoroutine(float gravityMult, float airResistance)
            {
                while (isActive)
                {
                    // Apply gravity
                    rb.velocity += Physics.gravity * gravityMult * Time.fixedDeltaTime;

                    // Apply air resistance
                    rb.velocity *= (1f - airResistance * Time.fixedDeltaTime);

                    yield return new WaitForFixedUpdate();
                }
            }
        }
        #endregion

        #region Impact System
        public class ImpactManager : MonoBehaviour
        {
            [System.Serializable]
            public class ImpactEffect
            {
                public PhysicMaterial material;
                public GameObject impactPrefab;
                public AudioClip[] impactSounds;
            }

            public List<ImpactEffect> impactEffects;
            private Dictionary<PhysicMaterial, ImpactEffect> effectsCache;

            public void Initialize()
            {
                effectsCache = new Dictionary<PhysicMaterial, ImpactEffect>();
                foreach (var effect in impactEffects)
                {
                    effectsCache[effect.material] = effect;
                }
            }

            public void SpawnImpact(RaycastHit hit)
            {
                PhysicMaterial material = hit.collider.sharedMaterial;
                if (material != null && effectsCache.TryGetValue(material, out ImpactEffect effect))
                {
                    // Spawn visual effect
                    GameObject impactObj = Instantiate(effect.impactPrefab, hit.point, 
                        Quaternion.LookRotation(hit.normal));
                    Destroy(impactObj, 5f);

                    // Play sound
                    if (effect.impactSounds.Length > 0)
                    {
                        AudioClip randomSound = effect.impactSounds[Random.Range(0, effect.impactSounds.Length)];
                        AudioSource.PlayClipAtPoint(randomSound, hit.point);
                    }
                }
            }
        }
        #endregion

        #region Data Structures
        [System.Serializable]
        public class WeaponData
        {
            public WeaponType type;
            public GameObject prefab;
            public float damage;
            public int magazineSize;
            public float fireRate;
            public float reloadTime;
            public float recoilDuration;
            public AnimationCurve recoilCurve;
            public AnimationCurve recoilRotationCurve;
            public ParticleSystem muzzleFlash;
            public AudioClip fireSound;
            public AudioClip reloadSound;
        }

        public enum WeaponState
        {
            Ready,
            Firing,
            Reloading,
            Empty
        }

        public enum WeaponType
        {
            Pistol,
            Rifle,
            Shotgun,
            SpecialWeapon,
            MagicWeapon, // Add MagicWeapon type
            ThunderGrenade, // Add ThunderGrenade type
            Shield,
            RangedWeapon,
            MeleeWeapon,
            AssaultRifle,
            SniperRifle,
            SubmachineGun,
            Nailgun,
            RocketLauncher,
            HuntingRifle,
            Crossbow,
            CleanCrossbow,
            MetalBat,
            WoodenBat,
            Machete,
            Katana,
            FireAxe,
            PipeWrench,
            GolfClub,
            Spear
        }

        public struct DamageInfo
        {
            public float amount;
            public Vector3 point;
            public Vector3 normal;
            public Vector3 source;
        }
        #endregion
    }

    public interface IVRGrabbable
    {
        void OnGrab(VRHand hand);
        void OnRelease(VRHand hand);
        bool CanGrab(VRHand hand);
    }

    public interface IDamageable
    {
        void TakeDamage(DamageInfo damageInfo);
    }
}