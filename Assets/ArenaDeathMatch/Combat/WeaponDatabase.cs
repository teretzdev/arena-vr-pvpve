using System.Collections.Generic;
using UnityEngine;

namespace ArenaDeathMatch.Combat
{
    public class WeaponDatabase : MonoBehaviour
    {
        public static WeaponDatabase Instance { get; private set; }

        [Header("Weapon Configuration")]
        public List<WeaponData> weapons;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                // Uncomment the following line if the WeaponDatabase should persist between scenes.
                // DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        [System.Serializable]
        public class WeaponData
        {
            public string type;
            public GameObject prefab;
            public float damage;
            public int magazineSize;
            public float fireRate;
            public float reloadTime;
            public float recoilDuration;
            public AnimationCurve recoilCurve;
            public AnimationCurve recoilRotationCurve;
            public GameObject muzzleFlash;
            public AudioClip fireSound;
            public AudioClip reloadSound;
            public string description;
        }

        private void Start()
        {
            // Initialize the weapons list with the provided weapon data
            weapons = new List<WeaponData>
            {
                new WeaponData
                {
                    type = "SM_Wep_Nailgun_01",
                    prefab = null, // Assign the appropriate prefab here
                    damage = 20f,
                    magazineSize = 30,
                    fireRate = 5f,
                    reloadTime = 2.0f,
                    recoilDuration = 0.4f,
                    recoilCurve = AnimationCurve.Linear(0, 0, 1, 1),
                    recoilRotationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1),
                    muzzleFlash = null, // Assign the appropriate muzzle flash prefab here
                    fireSound = null, // Assign the appropriate fire sound here
                    reloadSound = null, // Assign the appropriate reload sound here
                    description = "A construction tool repurposed as a weapon, firing nails at high speed."
                },
                new WeaponData
                {
                    type = "SM_Wep_Nailgun_Clean_01",
                    prefab = null, // Assign the appropriate prefab here
                    damage = 20f,
                    magazineSize = 30,
                    fireRate = 5f,
                    reloadTime = 2.0f,
                    recoilDuration = 0.4f,
                    recoilCurve = AnimationCurve.Linear(0, 0, 1, 1),
                    recoilRotationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1),
                    muzzleFlash = null, // Assign the appropriate muzzle flash prefab here
                    fireSound = null, // Assign the appropriate fire sound here
                    reloadSound = null, // Assign the appropriate reload sound here
                    description = "A cleaner version of the Nailgun with identical functionality."
                },
                new WeaponData
                {
                    type = "SM_Wep_Pistol_01",
                    prefab = null, // Assign the appropriate prefab here
                    damage = 15f,
                    magazineSize = 12,
                    fireRate = 2f,
                    reloadTime = 1.5f,
                    recoilDuration = 0.3f,
                    recoilCurve = AnimationCurve.EaseInOut(0, 0, 1, 1),
                    recoilRotationCurve = AnimationCurve.Linear(0, 0, 1, 1),
                    muzzleFlash = null, // Assign the appropriate muzzle flash prefab here
                    fireSound = null, // Assign the appropriate fire sound here
                    reloadSound = null, // Assign the appropriate reload sound here
                    description = "A standard semi-automatic pistol with moderate damage."
                },
                new WeaponData
                {
                    type = "SM_Wep_Revolver_01",
                    prefab = null, // Assign the appropriate prefab here
                    damage = 40f,
                    magazineSize = 6,
                    fireRate = 1f,
                    reloadTime = 3.0f,
                    recoilDuration = 0.6f,
                    recoilCurve = AnimationCurve.EaseInOut(0, 0, 1, 1),
                    recoilRotationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1),
                    muzzleFlash = null, // Assign the appropriate muzzle flash prefab here
                    fireSound = null, // Assign the appropriate fire sound here
                    reloadSound = null, // Assign the appropriate reload sound here
                    description = "A high-damage revolver with a slow reload time."
                },
                new WeaponData
                {
                    type = "SM_Wep_Revolver_02",
                    prefab = null, // Assign the appropriate prefab here
                    damage = 45f,
                    magazineSize = 6,
                    fireRate = 1.2f,
                    reloadTime = 2.8f,
                    recoilDuration = 0.5f,
                    recoilCurve = AnimationCurve.EaseInOut(0, 0, 1, 1),
                    recoilRotationCurve = AnimationCurve.Linear(0, 0, 1, 1),
                    muzzleFlash = null, // Assign the appropriate muzzle flash prefab here
                    fireSound = null, // Assign the appropriate fire sound here
                    reloadSound = null, // Assign the appropriate reload sound here
                    description = "An upgraded revolver with improved accuracy."
                },
                new WeaponData
                {
                    type = "SM_Wep_Rifle_01",
                    prefab = null, // Assign the appropriate prefab here
                    damage = 25f,
                    magazineSize = 20,
                    fireRate = 3f,
                    reloadTime = 2.5f,
                    recoilDuration = 0.4f,
                    recoilCurve = AnimationCurve.Linear(0, 0, 1, 1),
                    recoilRotationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1),
                    muzzleFlash = null, // Assign the appropriate muzzle flash prefab here
                    fireSound = null, // Assign the appropriate fire sound here
                    reloadSound = null, // Assign the appropriate reload sound here
                    description = "A semi-automatic rifle with balanced stats."
                },
                new WeaponData
                {
                    type = "SM_Wep_Rifle_02",
                    prefab = null, // Assign the appropriate prefab here
                    damage = 35f,
                    magazineSize = 15,
                    fireRate = 2.5f,
                    reloadTime = 3.0f,
                    recoilDuration = 0.5f,
                    recoilCurve = AnimationCurve.EaseInOut(0, 0, 1, 1),
                    recoilRotationCurve = AnimationCurve.Linear(0, 0, 1, 1),
                    muzzleFlash = null, // Assign the appropriate muzzle flash prefab here
                    fireSound = null, // Assign the appropriate fire sound here
                    reloadSound = null, // Assign the appropriate reload sound here
                    description = "A high-damage rifle with a slower fire rate."
                },
                new WeaponData
                {
                    type = "SM_Wep_Rifle_03",
                    prefab = null, // Assign the appropriate prefab here
                    damage = 50f,
                    magazineSize = 10,
                    fireRate = 1.5f,
                    reloadTime = 3.5f,
                    recoilDuration = 0.6f,
                    recoilCurve = AnimationCurve.EaseInOut(0, 0, 1, 1),
                    recoilRotationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1),
                    muzzleFlash = null, // Assign the appropriate muzzle flash prefab here
                    fireSound = null, // Assign the appropriate fire sound here
                    reloadSound = null, // Assign the appropriate reload sound here
                    description = "A precision rifle with exceptional accuracy."
                }
            };
        }
    }
}using System.Collections.Generic;
using UnityEngine;

namespace ArenaDeathMatch.Combat
{
    public class WeaponDatabase : MonoBehaviour
    {
        public static WeaponDatabase Instance { get; private set; }

        [Header("Weapon Configuration")]
        public List<WeaponData> weapons;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                // Uncomment the following line if the WeaponDatabase should persist between scenes.
                // DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        [System.Serializable]
        public class WeaponData
        {
            public string type;
            public GameObject prefab;
            public float damage;
            public int magazineSize;
            public float fireRate;
            public float reloadTime;
            public float recoilDuration;
            public AnimationCurve recoilCurve;
            public AnimationCurve recoilRotationCurve;
            public GameObject muzzleFlash;
            public AudioClip fireSound;
            public AudioClip reloadSound;
            public string description;
        }

        private void Start()
        {
            // Initialize the weapons list with the provided weapon data
            weapons = new List<WeaponData>
            {
                new WeaponData
                {
                    type = "SM_Wep_Nailgun_01",
                    prefab = null, // Assign the appropriate prefab here
                    damage = 20f,
                    magazineSize = 30,
                    fireRate = 5f,
                    reloadTime = 2.0f,
                    recoilDuration = 0.4f,
                    recoilCurve = AnimationCurve.Linear(0, 0, 1, 1),
                    recoilRotationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1),
                    muzzleFlash = null, // Assign the appropriate muzzle flash prefab here
                    fireSound = null, // Assign the appropriate fire sound here
                    reloadSound = null, // Assign the appropriate reload sound here
                    description = "A construction tool repurposed as a weapon, firing nails at high speed."
                },
                new WeaponData
                {
                    type = "SM_Wep_Nailgun_Clean_01",
                    prefab = null, // Assign the appropriate prefab here
                    damage = 20f,
                    magazineSize = 30,
                    fireRate = 5f,
                    reloadTime = 2.0f,
                    recoilDuration = 0.4f,
                    recoilCurve = AnimationCurve.Linear(0, 0, 1, 1),
                    recoilRotationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1),
                    muzzleFlash = null, // Assign the appropriate muzzle flash prefab here
                    fireSound = null, // Assign the appropriate fire sound here
                    reloadSound = null, // Assign the appropriate reload sound here
                    description = "A cleaner version of the Nailgun with identical functionality."
                },
                new WeaponData
                {
                    type = "SM_Wep_Pistol_01",
                    prefab = null, // Assign the appropriate prefab here
                    damage = 15f,
                    magazineSize = 12,
                    fireRate = 2f,
                    reloadTime = 1.5f,
                    recoilDuration = 0.3f,
                    recoilCurve = AnimationCurve.EaseInOut(0, 0, 1, 1),
                    recoilRotationCurve = AnimationCurve.Linear(0, 0, 1, 1),
                    muzzleFlash = null, // Assign the appropriate muzzle flash prefab here
                    fireSound = null, // Assign the appropriate fire sound here
                    reloadSound = null, // Assign the appropriate reload sound here
                    description = "A standard semi-automatic pistol with moderate damage."
                },
                new WeaponData
                {
                    type = "SM_Wep_Revolver_01",
                    prefab = null, // Assign the appropriate prefab here
                    damage = 40f,
                    magazineSize = 6,
                    fireRate = 1f,
                    reloadTime = 3.0f,
                    recoilDuration = 0.6f,
                    recoilCurve = AnimationCurve.EaseInOut(0, 0, 1, 1),
                    recoilRotationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1),
                    muzzleFlash = null, // Assign the appropriate muzzle flash prefab here
                    fireSound = null, // Assign the appropriate fire sound here
                    reloadSound = null, // Assign the appropriate reload sound here
                    description = "A high-damage revolver with a slow reload time."
                },
                new WeaponData
                {
                    type = "SM_Wep_Revolver_02",
                    prefab = null, // Assign the appropriate prefab here
                    damage = 45f,
                    magazineSize = 6,
                    fireRate = 1.2f,
                    reloadTime = 2.8f,
                    recoilDuration = 0.5f,
                    recoilCurve = AnimationCurve.EaseInOut(0, 0, 1, 1),
                    recoilRotationCurve = AnimationCurve.Linear(0, 0, 1, 1),
                    muzzleFlash = null, // Assign the appropriate muzzle flash prefab here
                    fireSound = null, // Assign the appropriate fire sound here
                    reloadSound = null, // Assign the appropriate reload sound here
                    description = "An upgraded revolver with improved accuracy."
                },
                new WeaponData
                {
                    type = "SM_Wep_Rifle_01",
                    prefab = null, // Assign the appropriate prefab here
                    damage = 25f,
                    magazineSize = 20,
                    fireRate = 3f,
                    reloadTime = 2.5f,
                    recoilDuration = 0.4f,
                    recoilCurve = AnimationCurve.Linear(0, 0, 1, 1),
                    recoilRotationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1),
                    muzzleFlash = null, // Assign the appropriate muzzle flash prefab here
                    fireSound = null, // Assign the appropriate fire sound here
                    reloadSound = null, // Assign the appropriate reload sound here
                    description = "A semi-automatic rifle with balanced stats."
                },
                new WeaponData
                {
                    type = "SM_Wep_Rifle_02",
                    prefab = null, // Assign the appropriate prefab here
                    damage = 35f,
                    magazineSize = 15,
                    fireRate = 2.5f,
                    reloadTime = 3.0f,
                    recoilDuration = 0.5f,
                    recoilCurve = AnimationCurve.EaseInOut(0, 0, 1, 1),
                    recoilRotationCurve = AnimationCurve.Linear(0, 0, 1, 1),
                    muzzleFlash = null, // Assign the appropriate muzzle flash prefab here
                    fireSound = null, // Assign the appropriate fire sound here
                    reloadSound = null, // Assign the appropriate reload sound here
                    description = "A high-damage rifle with a slower fire rate."
                },
                new WeaponData
                {
                    type = "SM_Wep_Rifle_03",
                    prefab = null, // Assign the appropriate prefab here
                    damage = 50f,
                    magazineSize = 10,
                    fireRate = 1.5f,
                    reloadTime = 3.5f,
                    recoilDuration = 0.6f,
                    recoilCurve = AnimationCurve.EaseInOut(0, 0, 1, 1),
                    recoilRotationCurve = AnimationCurve.EaseInOut(0, 0, 1, 1),
                    muzzleFlash = null, // Assign the appropriate muzzle flash prefab here
                    fireSound = null, // Assign the appropriate fire sound here
                    reloadSound = null, // Assign the appropriate reload sound here
                    description = "A precision rifle with exceptional accuracy."
                }
            };
        }
    }
}