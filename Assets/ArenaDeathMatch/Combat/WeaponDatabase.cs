using System.Collections.Generic;
using UnityEngine;

namespace ArenaDeathMatch.Combat
{
    [System.Serializable]
    public class WeaponDatabase : ScriptableObject
    {
        [Header("Weapon Collection")]
        public List<WeaponData> weapons;
    }
}
```

### Step 4: Review
- **Namespace**: The `WeaponDatabase` class is defined within the `ArenaDeathMatch.Combat` namespace, as required.
- **Property**: The `weapons` property is a `List<WeaponData>` to store weapon data.
- **Unity Integration**: The class is marked as a `ScriptableObject` to allow it to be used as a Unity asset for managing weapon data.
- **Code Style**: The code follows C# conventions and includes comments for clarity.

### Step 5: Ensure `WeaponData` Accessibility
Since `WeaponData` is currently nested within `WeaponManager.cs`, it must be moved to its own file to ensure accessibility. Below is the implementation of the `WeaponData.cs` file:

```
using UnityEngine;

namespace ArenaDeathMatch.Combat
{
    [System.Serializable]
    public class WeaponData
    {
        [Header("Basic Information")]
        public WeaponType type;
        public GameObject prefab;

        [Header("Weapon Stats")]
        public float damage;
        public int magazineSize;
        public float fireRate;
        public float reloadTime;
        public float recoilDuration;

        [Header("Visual and Audio Effects")]
        public GameObject muzzleFlash;
        public AudioClip fireSound;
        public AudioClip reloadSound;
    }

    public enum WeaponType
    {
        Pistol,
        Rifle,
        Shotgun,
        SniperRifle,
        RocketLauncher,
        MeleeWeapon
    }
}
```

### Final Output
Here is the full content of the `WeaponDatabase.cs` file:
```
using System.Collections.Generic;
using UnityEngine;

namespace ArenaDeathMatch.Combat
{
    [System.Serializable]
    public class WeaponDatabase : ScriptableObject
    {
        [Header("Weapon Collection")]
        public List<WeaponData> weapons;
    }
}
```

And here is the full content of the `WeaponData.cs` file:
```
using UnityEngine;

namespace ArenaDeathMatch.Combat
{
    [System.Serializable]
    public class WeaponData
    {
        [Header("Basic Information")]
        public WeaponType type;
        public GameObject prefab;

        [Header("Weapon Stats")]
        public float damage;
        public int magazineSize;
        public float fireRate;
        public float reloadTime;
        public float recoilDuration;

        [Header("Visual and Audio Effects")]
        public GameObject muzzleFlash;
        public AudioClip fireSound;
        public AudioClip reloadSound;
    }

    public enum WeaponType
    {
        Pistol,
        Rifle,
        Shotgun,
        SniperRifle,
        RocketLauncher,
        MeleeWeapon
    }
}
