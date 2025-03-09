using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using Oculus.Interaction;

namespace ArenaDeathMatch.VR
{
    /// <summary>
    /// Handles Meta Quest SDK integration for Quest-specific features such as hand tracking, controller input mapping, and performance optimization.
    /// </summary>
    public class MetaQuestIntegration : MonoBehaviour
    {
        [Header("Hand Tracking Settings")]
        public bool enableHandTracking = true; // Enable or disable hand tracking
        public GameObject leftHandPrefab; // Prefab for the left hand
        public GameObject rightHandPrefab; // Prefab for the right hand

        [Header("Controller Input Mapping")]
        public XRController leftController; // Reference to the left controller
        public XRController rightController; // Reference to the right controller
        public bool enableControllerHaptics = true; // Enable or disable haptic feedback for controllers

        [Header("Performance Optimization")]
        public bool enableDynamicResolution = true; // Enable or disable dynamic resolution scaling
        public bool enableFoveatedRendering = true; // Enable or disable foveated rendering
        public int targetFrameRate = 72; // Target frame rate for the Quest platform

        private void Awake()
        {
            InitializeHandTracking();
            InitializeControllerInput();
            ApplyPerformanceOptimizations();
        }

        /// <summary>
        /// Initializes hand tracking if enabled.
        /// </summary>
        private void InitializeHandTracking()
        {
            if (enableHandTracking)
            {
                if (leftHandPrefab != null)
                {
                    Instantiate(leftHandPrefab, transform);
                }
                else
                {
                    Debug.LogWarning("Left hand prefab is not assigned.");
                }

                if (rightHandPrefab != null)
                {
                    Instantiate(rightHandPrefab, transform);
                }
                else
                {
                    Debug.LogWarning("Right hand prefab is not assigned.");
                }

                Debug.Log("Hand tracking initialized.");
            }
        }

        /// <summary>
        /// Configures controller input mapping for the Quest controllers.
        /// </summary>
        private void InitializeControllerInput()
        {
            if (leftController == null || rightController == null)
            {
                Debug.LogError("Controller references are not assigned.");
                return;
            }

            // Example: Map the primary button to a custom action
            leftController.inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool leftPrimaryButton);
            rightController.inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out bool rightPrimaryButton);

            if (enableControllerHaptics)
            {
                leftController.SendHapticImpulse(0.5f, 0.1f); // Example haptic feedback
                rightController.SendHapticImpulse(0.5f, 0.1f); // Example haptic feedback
            }
            Debug.Log($"Controller input mapping initialized. Left Primary Button: {leftPrimaryButton}, Right Primary Button: {rightPrimaryButton}");
        }

        /// <summary>
        /// Applies performance optimizations specific to the Quest platform.
        /// </summary>
        private void ApplyPerformanceOptimizations()
        {
            if (enableDynamicResolution)
            {
                XRSettings.eyeTextureResolutionScale = 1.0f; // Adjust as needed for dynamic resolution scaling
                Debug.Log("Dynamic resolution scaling enabled.");
            }

            if (enableFoveatedRendering)
            {
                OVRManager.tiledMultiResLevel = OVRManager.TiledMultiResLevel.LMSHigh; // Adjust as needed
                Debug.Log("Foveated rendering enabled.");
            }

            Application.targetFrameRate = targetFrameRate;
            Debug.Log($"Target frame rate set to {targetFrameRate} FPS.");
        }
    }
}