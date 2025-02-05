using UnityEngine;
using ArenaDeathMatch.Combat;
using EmeraldAI;
using BNG; // VR Interaction Framework namespace

namespace ArenaDeathMatch.Combat
{
    public class VRWeapon : MonoBehaviour, IVRGrabbable
    {
        [Header("Weapon Configuration")]
        public WeaponManager.WeaponData weaponData;
        public Transform muzzle;
        public Transform ejectionPort;
        public ParticleSystem muzzleFlash;
        public AudioClip fireSound;
        public AudioClip reloadSound;

        [Header("VR Interaction")]
        public VRGrabbable grabbable;
        public VRInteractable interactable;

        [Header("Weapon State")]
        public int currentAmmo;
        public bool isReloading;

        private AudioSource audioSource;

        private void Awake()
        {
            InitializeWeapon();
        }

        private void InitializeWeapon()
        {
            if (weaponData == null)
            {
                Debug.LogError("WeaponData is not assigned!");
                return;
            }

            currentAmmo = weaponData.magazineSize;
            audioSource = GetComponent<AudioSource>();

            if (grabbable == null)
            {
                grabbable = gameObject.AddComponent<VRGrabbable>();
            }

            if (interactable == null)
            {
                interactable = gameObject.AddComponent<VRInteractable>();
            }

            SetupInteractions();
        }

        private void SetupInteractions()
        {
            // Use VRIF's updated event system for interaction handling
            interactable.TriggerDownEvent += Fire;
            interactable.TriggerUpEvent += StopFiring;
            interactable.GripDownEvent += Reload;

            // Add Meta VR device-specific input handling
            if (MetaVRDevice.IsConnected)
            {
                MetaVRDevice.OnTriggerPressed += Fire;
                MetaVRDevice.OnGripPressed += Reload;
            }
        }

        public void Fire()
        {
            if (isReloading || currentAmmo <= 0)
            {
                Debug.Log("Cannot fire: Reloading or out of ammo.");
                return;
            }

            currentAmmo--;
            PlayMuzzleFlash();
            PlayFireSound();
            SpawnBullet();

            if (currentAmmo == 0)
            {
                Debug.Log("Out of ammo! Reload required.");
            }
        }

        private void StopFiring()
        {
            // Logic for stopping continuous fire (if applicable)
        }

        public void Reload()
        {
            if (isReloading)
            {
                Debug.Log("Already reloading.");
                return;
            }

            isReloading = true;
            PlayReloadSound();

            StartCoroutine(ReloadCoroutine());
        }

        private IEnumerator ReloadCoroutine()
        {
            yield return new WaitForSeconds(weaponData.reloadTime);
            currentAmmo = weaponData.magazineSize;
            isReloading = false;
            Debug.Log("Reload complete.");
        }

        private void PlayMuzzleFlash()
        {
            // Play muzzle flash effect if available
            if (muzzleFlash != null)
            {
                muzzleFlash.Play();
            }
        }

        private void PlayFireSound()
        {
            if (fireSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(fireSound);
            }
        }

        private void PlayReloadSound()
        {
            if (reloadSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(reloadSound);
            }
        }

        private void SpawnBullet()
        {
            if (WeaponManager.Instance == null)
            {
                Debug.LogError("WeaponManager instance not found!");
                return;
            }

            WeaponManager.Bullet bullet = WeaponManager.Instance.SpawnBullet(muzzle.position, muzzle.forward, weaponData.damage);
            if (bullet != null)
            {
                bullet.Launch();
            }
        }

        public void OnGrab(VRHand hand)
        {
            // Log grab event and ensure compatibility with VRIF
            Debug.Log($"{gameObject.name} grabbed by {hand.name}");
            grabbable.OnGrab(hand);
        }

        public void OnRelease(VRHand hand)
        {
            // Log release event and ensure compatibility with VRIF
            Debug.Log($"{gameObject.name} released by {hand.name}");
            grabbable.OnRelease(hand);
        }

        public bool CanGrab(VRHand hand)
        {
            return true; // Add logic if specific conditions are required for grabbing
        }

        private void OnTriggerEnter(Collider other)
        {
            // Handle interaction with Emerald AI 2024 NPCs
            EmeraldAISystem emeraldAI = other.GetComponent<EmeraldAISystem>();
            if (emeraldAI != null)
            {
                emeraldAI.Damage(weaponData.damage, EmeraldAISystem.TargetType.Player, transform.position);
                Debug.Log($"Dealt {weaponData.damage} damage to {emeraldAI.name}");
            }
        }
    }
}