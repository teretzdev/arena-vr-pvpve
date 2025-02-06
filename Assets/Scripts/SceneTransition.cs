using UnityEngine;
using UnityEngine.SceneManagement;

namespace ArenaDeathMatch.Utilities
{
    /// <summary>
    /// Handles scene transitions when a player interacts with a PortalMarker.
    /// </summary>
    public class SceneTransition : MonoBehaviour
    {
        [Header("Scene Transition Settings")]
        [Tooltip("The name of the scene to load when the player interacts with this portal.")]
        public string targetSceneName;

        /// <summary>
        /// Detects when a player enters the portal's trigger collider and initiates the scene transition.
        /// </summary>
        /// <param name="other">The collider of the object that entered the trigger.</param>
        private void OnTriggerEnter(Collider other)
        {
            // Check if the object entering the trigger is tagged as "Player"
            if (other.CompareTag("Player"))
            {
                // Ensure the target scene name is valid
                if (!string.IsNullOrEmpty(targetSceneName))
                {
                    Debug.Log($"Player entered portal. Loading scene: {targetSceneName}");
                    LoadTargetScene();
                }
                else
                {
                    Debug.LogError("Target scene name is not set on the PortalMarker.");
                }
            }
        }

        /// <summary>
        /// Loads the target scene specified in the targetSceneName field.
        /// </summary>
        private void LoadTargetScene()
        {
            // Use Unity's SceneManager to load the specified scene
            SceneManager.LoadScene(targetSceneName);
        }
    }
}
```

### Step 4: Review
1. **File Name and Location**: The file is named `SceneTransition.cs` and is located in the `Assets/Scripts/` directory as specified.
2. **Player Interaction Detection**: The script uses Unity's `OnTriggerEnter` method to detect when a player (tagged as "Player") interacts with the `PortalMarker` prefab.
3. **Scene Loading**: The script uses `SceneManager.LoadScene` to load the target scene specified in the `targetSceneName` field.
4. **Prefab Compatibility**: The script is compatible with the `PortalMarker` prefab, which includes a `BoxCollider` (set as a trigger) and a public field for the target scene name.
5. **Error Handling**: The script includes error handling to ensure the `targetSceneName` field is set before attempting to load a scene.

### Final Output
The following is the complete and functional implementation of the `SceneTransition.cs` file:

```
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ArenaDeathMatch.Utilities
{
    /// <summary>
    /// Handles scene transitions when a player interacts with a PortalMarker.
    /// </summary>
    public class SceneTransition : MonoBehaviour
    {
        [Header("Scene Transition Settings")]
        [Tooltip("The name of the scene to load when the player interacts with this portal.")]
        public string targetSceneName;

        /// <summary>
        /// Detects when a player enters the portal's trigger collider and initiates the scene transition.
        /// </summary>
        /// <param name="other">The collider of the object that entered the trigger.</param>
        private void OnTriggerEnter(Collider other)
        {
            // Check if the object entering the trigger is tagged as "Player"
            if (other.CompareTag("Player"))
            {
                // Ensure the target scene name is valid
                if (!string.IsNullOrEmpty(targetSceneName))
                {
                    Debug.Log($"Player entered portal. Loading scene: {targetSceneName}");
                    LoadTargetScene();
                }
                else
                {
                    Debug.LogError("Target scene name is not set on the PortalMarker.");
                }
            }
        }

        /// <summary>
        /// Loads the target scene specified in the targetSceneName field.
        /// </summary>
        private void LoadTargetScene()
        {
            // Use Unity's SceneManager to load the specified scene
            SceneManager.LoadScene(targetSceneName);
        }
    }
}
