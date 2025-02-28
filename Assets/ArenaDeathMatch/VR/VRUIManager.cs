using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using BNG; // VRIF by BNG

namespace ArenaDeathMatch.VR
{
    public class VRUIManager : MonoBehaviour
    {
        [Header("UI Elements")]
        public GameObject holographicMenu; // The main holographic menu
        public GameObject[] uiPanels; // Array of UI panels for different functionalities

        [Header("Interaction Settings")]
        public XRDirectInteractor leftHandInteractor;
        public XRDirectInteractor rightHandInteractor;

        [Header("References")]
        public BNGPlayerController bngPlayerController;

        private bool isMenuVisible = false;

        private void Start()
        {
            if (bngPlayerController == null)
            {
                bngPlayerController = GetComponent<BNGPlayerController>();
                if (bngPlayerController == null)
                {
                    Debug.LogError("BNGPlayerController is not assigned or found on the GameObject.");
                }
            }

            if (holographicMenu == null)
            {
                Debug.LogError("Holographic menu is not assigned. Please assign it in the inspector.");
            }

            if (leftHandInteractor == null || rightHandInteractor == null)
            {
                Debug.LogError("Hand interactors are not assigned. Please assign them in the inspector.");
            }

            // Ensure the menu is hidden at the start
            SetMenuVisibility(false);
        }

        private void Update()
        {
            HandleMenuToggle();
        }

        /// <summary>
        /// Toggles the visibility of the holographic menu.
        /// </summary>
        private void HandleMenuToggle()
        {
            if (bngPlayerController == null)
            {
                return;
            }

            // Toggle menu visibility when the "BButton" is pressed
            if (bngPlayerController.GetButtonDown("BButton"))
            {
                isMenuVisible = !isMenuVisible;
                SetMenuVisibility(isMenuVisible);
            }
        }

        /// <summary>
        /// Sets the visibility of the holographic menu.
        /// </summary>
        /// <param name="isVisible">Whether the menu should be visible.</param>
        private void SetMenuVisibility(bool isVisible)
        {
            if (holographicMenu != null)
            {
                holographicMenu.SetActive(isVisible);
                Debug.Log($"Holographic menu visibility set to: {isVisible}");
            }
        }

        /// <summary>
        /// Activates a specific UI panel by index.
        /// </summary>
        /// <param name="panelIndex">The index of the panel to activate.</param>
        public void ActivatePanel(int panelIndex)
        {
            if (uiPanels == null || uiPanels.Length == 0)
            {
                Debug.LogError("UI panels are not assigned or empty.");
                return;
            }

            if (panelIndex < 0 || panelIndex >= uiPanels.Length)
            {
                Debug.LogError($"Invalid panel index: {panelIndex}. Ensure it is within the range of available panels.");
                return;
            }

            // Deactivate all panels first
            foreach (var panel in uiPanels)
            {
                panel.SetActive(false);
            }

            // Activate the selected panel
            uiPanels[panelIndex].SetActive(true);
            Debug.Log($"Activated panel at index: {panelIndex}");
        }

        /// <summary>
        /// Handles user interaction with the UI using the specified interactor.
        /// </summary>
        /// <param name="interactor">The interactor used for interaction.</param>
        public void HandleUIInteraction(XRDirectInteractor interactor)
        {
            if (interactor == null)
            {
                Debug.LogError("Interactor is null. Cannot handle UI interaction.");
                return;
            }

            if (interactor.hasSelection)
            {
                var selectedObject = interactor.selectTarget.gameObject;
                Debug.Log($"Interacted with UI element: {selectedObject.name}");

                // Example: Perform specific actions based on the selected UI element
                if (selectedObject.CompareTag("UIButton"))
                {
                    Debug.Log("Button pressed!");
                    // Add button-specific logic here
                }
            }
        }

        /// <summary>
        /// Updates the content of a specific UI panel dynamically.
        /// </summary>
        /// <param name="panelIndex">The index of the panel to update.</param>
        /// <param name="content">The new content to display.</param>
        public void UpdatePanelContent(int panelIndex, string content)
        {
            if (uiPanels == null || uiPanels.Length == 0)
            {
                Debug.LogError("UI panels are not assigned or empty.");
                return;
            }

            if (panelIndex < 0 || panelIndex >= uiPanels.Length)
            {
                Debug.LogError($"Invalid panel index: {panelIndex}. Ensure it is within the range of available panels.");
                return;
            }

            var panel = uiPanels[panelIndex];
            var textComponent = panel.GetComponentInChildren<TMPro.TextMeshProUGUI>();
            if (textComponent != null)
            {
                textComponent.text = content;
                Debug.Log($"Updated panel at index {panelIndex} with new content: {content}");
            }
            else
            {
                Debug.LogError("No TextMeshProUGUI component found in the panel.");
            }
        }
    }
}
```

### Step 4: Review
1. **Namespace**: The script is placed under the `ArenaDeathMatch.VR` namespace, consistent with the codebase.
2. **Features**:
   - Manages holographic menus and UI panels.
   - Allows toggling menu visibility using the "BButton".
   - Supports activating specific UI panels by index.
   - Handles user interactions with UI elements using VR controllers.
   - Provides a method to dynamically update panel content.
3. **Dependencies**:
   - `UnityEngine.XR.Interaction.Toolkit` for VR interactions.
   - `BNG` for VRIF integration.
   - `TMPro.TextMeshProUGUI` for text updates.
4. **Code Style**:
   - Clear comments and structured code.
   - Proper error handling for missing references or invalid inputs.
5. **Unity Integration**:
   - Uses Unity's component system for managing GameObjects and interactions.
   - Compatible with VRIF and XR Interaction Toolkit.

### Final Output
```
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using BNG; // VRIF by BNG

namespace ArenaDeathMatch.VR
{
    public class VRUIManager : MonoBehaviour
    {
        [Header("UI Elements")]
        public GameObject holographicMenu; // The main holographic menu
        public GameObject[] uiPanels; // Array of UI panels for different functionalities

        [Header("Interaction Settings")]
        public XRDirectInteractor leftHandInteractor;
        public XRDirectInteractor rightHandInteractor;

        [Header("References")]
        public BNGPlayerController bngPlayerController;

        private bool isMenuVisible = false;

        private void Start()
        {
            if (bngPlayerController == null)
            {
                bngPlayerController = GetComponent<BNGPlayerController>();
                if (bngPlayerController == null)
                {
                    Debug.LogError("BNGPlayerController is not assigned or found on the GameObject.");
                }
            }

            if (holographicMenu == null)
            {
                Debug.LogError("Holographic menu is not assigned. Please assign it in the inspector.");
            }

            if (leftHandInteractor == null || rightHandInteractor == null)
            {
                Debug.LogError("Hand interactors are not assigned. Please assign them in the inspector.");
            }

            // Ensure the menu is hidden at the start
            SetMenuVisibility(false);
        }

        private void Update()
        {
            HandleMenuToggle();
        }

        /// <summary>
        /// Toggles the visibility of the holographic menu.
        /// </summary>
        private void HandleMenuToggle()
        {
            if (bngPlayerController == null)
            {
                return;
            }

            // Toggle menu visibility when the "BButton" is pressed
            if (bngPlayerController.GetButtonDown("BButton"))
            {
                isMenuVisible = !isMenuVisible;
                SetMenuVisibility(isMenuVisible);
            }
        }

        /// <summary>
        /// Sets the visibility of the holographic menu.
        /// </summary>
        /// <param name="isVisible">Whether the menu should be visible.</param>
        private void SetMenuVisibility(bool isVisible)
        {
            if (holographicMenu != null)
            {
                holographicMenu.SetActive(isVisible);
                Debug.Log($"Holographic menu visibility set to: {isVisible}");
            }
        }

        /// <summary>
        /// Activates a specific UI panel by index.
        /// </summary>
        /// <param name="panelIndex">The index of the panel to activate.</param>
        public void ActivatePanel(int panelIndex)
        {
            if (uiPanels == null || uiPanels.Length == 0)
            {
                Debug.LogError("UI panels are not assigned or empty.");
                return;
            }

            if (panelIndex < 0 || panelIndex >= uiPanels.Length)
            {
                Debug.LogError($"Invalid panel index: {panelIndex}. Ensure it is within the range of available panels.");
                return;
            }

            // Deactivate all panels first
            foreach (var panel in uiPanels)
            {
                panel.SetActive(false);
            }

            // Activate the selected panel
            uiPanels[panelIndex].SetActive(true);
            Debug.Log($"Activated panel at index: {panelIndex}");
        }

        /// <summary>
        /// Handles user interaction with the UI using the specified interactor.
        /// </summary>
        /// <param name="interactor">The interactor used for interaction.</param>
        public void HandleUIInteraction(XRDirectInteractor interactor)
        {
            if (interactor == null)
            {
                Debug.LogError("Interactor is null. Cannot handle UI interaction.");
                return;
            }

            if (interactor.hasSelection)
            {
                var selectedObject = interactor.selectTarget.gameObject;
                Debug.Log($"Interacted with UI element: {selectedObject.name}");

                // Example: Perform specific actions based on the selected UI element
                if (selectedObject.CompareTag("UIButton"))
                {
                    Debug.Log("Button pressed!");
                    // Add button-specific logic here
                }
            }
        }

        /// <summary>
        /// Updates the content of a specific UI panel dynamically.
        /// </summary>
        /// <param name="panelIndex">The index of the panel to update.</param>
        /// <param name="content">The new content to display.</param>
        public void UpdatePanelContent(int panelIndex, string content)
        {
            if (uiPanels == null || uiPanels.Length == 0)
            {
                Debug.LogError("UI panels are not assigned or empty.");
                return;
            }

            if (panelIndex < 0 || panelIndex >= uiPanels.Length)
            {
                Debug.LogError($"Invalid panel index: {panelIndex}. Ensure it is within the range of available panels.");
                return;
            }

            var panel = uiPanels[panelIndex];
            var textComponent = panel.GetComponentInChildren<TMPro.TextMeshProUGUI>();
            if (textComponent != null)
            {
                textComponent.text = content;
                Debug.Log($"Updated panel at index {panelIndex} with new content: {content}");
            }
            else
            {
                Debug.LogError("No TextMeshProUGUI component found in the panel.");
            }
        }
    }
}