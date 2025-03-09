using UnityEngine;

namespace ArenaDeathMatch.VR
{
    /// <summary>
    /// Handles Quest-specific performance optimizations, including dynamic resolution scaling, foveated rendering, and level of detail management.
    /// </summary>
    public class QuestPerformanceOptimizer : MonoBehaviour
    {
        [Header("Dynamic Resolution Settings")]
        [Tooltip("Enable or disable dynamic resolution scaling.")]
        public bool enableDynamicResolution = true;
        [Tooltip("Target resolution scale for dynamic resolution.")]
        [Range(0.5f, 1.5f)]
        public float targetResolutionScale = 1.0f;

        [Header("Foveated Rendering Settings")]
        [Tooltip("Enable or disable foveated rendering.")]
        public bool enableFoveatedRendering = true;
        [Tooltip("Level of foveated rendering to apply.")]
        public OVRManager.TiledMultiResLevel foveatedRenderingLevel = OVRManager.TiledMultiResLevel.LMSHigh;

        [Header("Level of Detail Settings")]
        [Tooltip("Enable or disable automatic level of detail management.")]
        public bool enableLODManagement = true;
        [Tooltip("Maximum LOD level to enforce.")]
        [Range(0, 3)]
        public int maxLODLevel = 1;

        private void Start()
        {
            ApplyDynamicResolution();
            ApplyFoveatedRendering();
            ApplyLODManagement();
        }

        /// <summary>
        /// Configures dynamic resolution scaling based on the target resolution scale.
        /// </summary>
        private void ApplyDynamicResolution()
        {
            if (enableDynamicResolution)
            {
                XRSettings.eyeTextureResolutionScale = targetResolutionScale;
                Debug.Log($"Dynamic resolution scaling enabled. Target resolution scale: {targetResolutionScale}");
            }
            else
            {
                Debug.Log("Dynamic resolution scaling disabled.");
            }
        }

        /// <summary>
        /// Configures foveated rendering based on the selected level.
        /// </summary>
        private void ApplyFoveatedRendering()
        {
            if (enableFoveatedRendering)
            {
                OVRManager.tiledMultiResLevel = foveatedRenderingLevel;
                Debug.Log($"Foveated rendering enabled. Level: {foveatedRenderingLevel}");
            }
            else
            {
                OVRManager.tiledMultiResLevel = OVRManager.TiledMultiResLevel.Off;
                Debug.Log("Foveated rendering disabled.");
            }
        }

        /// <summary>
        /// Configures level of detail management by enforcing the maximum LOD level.
        /// </summary>
        private void ApplyLODManagement()
        {
            if (enableLODManagement)
            {
                QualitySettings.maximumLODLevel = maxLODLevel;
                Debug.Log($"LOD management enabled. Maximum LOD level: {maxLODLevel}");
            }
            else
            {
                QualitySettings.maximumLODLevel = 0;
                Debug.Log("LOD management disabled.");
            }
        }
    }
}