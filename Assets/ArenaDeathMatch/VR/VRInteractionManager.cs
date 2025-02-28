using UnityEngine;
using BNG; // VRIF by BNG
using UnityEngine.XR.Interaction.Toolkit;

namespace ArenaDeathMatch.VR
{
    public class VRInteractionManager : MonoBehaviour
    {
        [Header("Interaction Settings")]
        public XRDirectInteractor leftHandInteractor;
        public XRDirectInteractor rightHandInteractor;

        [Header("Grab Settings")]
        public LayerMask grabbableLayer;
        public float grabRange = 2.0f;

        [Header("References")]
        public BNGPlayerController bngPlayerController;

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

            if (leftHandInteractor == null || rightHandInteractor == null)
            {
                Debug.LogError("Hand interactors are not assigned. Please assign them in the inspector.");
            }
        }

        private void Update()
        {
            HandleGrabInteraction();
        }

        private void HandleGrabInteraction()
        {
            if (bngPlayerController == null)
            {
                return;
            }

            // Check for grab input
            if (bngPlayerController.GetButtonDown("LeftGrip"))
            {
                TryGrabObject(leftHandInteractor);
            }
            else if (bngPlayerController.GetButtonDown("RightGrip"))
            {
                TryGrabObject(rightHandInteractor);
            }

            // Check for drop input
            if (bngPlayerController.GetButtonDown("LeftTrigger"))
            {
                TryDropObject(leftHandInteractor);
            }
            else if (bngPlayerController.GetButtonDown("RightTrigger"))
            {
                TryDropObject(rightHandInteractor);
            }
        }

        private void TryGrabObject(XRDirectInteractor interactor)
        {
            if (interactor == null)
            {
                Debug.LogError("Interactor is null. Cannot grab object.");
                return;
            }

            Collider[] colliders = Physics.OverlapSphere(interactor.transform.position, grabRange, grabbableLayer);
            if (colliders.Length > 0)
            {
                foreach (var collider in colliders)
                {
                    if (collider.TryGetComponent(out XRBaseInteractable interactable))
                    {
                        interactor.interactionManager.SelectEnter(interactor, interactable);
                        Debug.Log($"Grabbed object: {collider.gameObject.name}");
                        return;
                    }
                }
            }
            else
            {
                Debug.Log("No grabbable object found within range.");
            }
        }

        private void TryDropObject(XRDirectInteractor interactor)
        {
            if (interactor == null)
            {
                Debug.LogError("Interactor is null. Cannot drop object.");
                return;
            }

            if (interactor.hasSelection)
            {
                interactor.interactionManager.SelectExit(interactor, interactor.selectTarget);
                Debug.Log("Dropped object.");
            }
            else
            {
                Debug.Log("No object to drop.");
            }
        }

        public void UseItem(GameObject item)
        {
            if (item == null)
            {
                Debug.LogError("Item is null. Cannot use item.");
                return;
            }

            if (item.TryGetComponent(out IUsable usable))
            {
                usable.Use();
                Debug.Log($"Used item: {item.name}");
            }
            else
            {
                Debug.LogError($"The object {item.name} does not implement the IUsable interface.");
            }
        }

        public void InteractWithEnvironment(GameObject target)
        {
            if (target == null)
            {
                Debug.LogError("Target is null. Cannot interact with environment.");
                return;
            }

            if (target.TryGetComponent(out IInteractable interactable))
            {
                interactable.Interact();
                Debug.Log($"Interacted with: {target.name}");
            }
            else
            {
                Debug.LogError($"The object {target.name} does not implement the IInteractable interface.");
            }
        }
    }

    public interface IUsable
    {
        void Use();
    }

    public interface IInteractable
    {
        void Interact();
    }
}
```

---

### Step 4: Review
1. **Namespace**: The script is placed under the `ArenaDeathMatch.VR` namespace, consistent with the codebase.
2. **Features**:
   - Handles grabbing and dropping objects using `XRDirectInteractor`.
   - Supports item usage through the `IUsable` interface.
   - Enables interaction with the environment using the `IInteractable` interface.
   - Uses `BNGPlayerController` for input handling.
3. **Dependencies**:
   - `BNG` namespace for VRIF integration.
   - `UnityEngine.XR.Interaction.Toolkit` for interaction handling.
4. **Code Style**:
   - Clear comments and structured code.
   - Proper use of Unity's component system and VRIF/XR Interaction Toolkit APIs.
5. **Error Handling**:
   - Checks for null references (e.g., interactors, player controller).
   - Logs errors for missing components or unassigned references.

---

### Final Output
```
using UnityEngine;
using BNG; // VRIF by BNG
using UnityEngine.XR.Interaction.Toolkit;

namespace ArenaDeathMatch.VR
{
    public class VRInteractionManager : MonoBehaviour
    {
        [Header("Interaction Settings")]
        public XRDirectInteractor leftHandInteractor;
        public XRDirectInteractor rightHandInteractor;

        [Header("Grab Settings")]
        public LayerMask grabbableLayer;
        public float grabRange = 2.0f;

        [Header("References")]
        public BNGPlayerController bngPlayerController;

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

            if (leftHandInteractor == null || rightHandInteractor == null)
            {
                Debug.LogError("Hand interactors are not assigned. Please assign them in the inspector.");
            }
        }

        private void Update()
        {
            HandleGrabInteraction();
        }

        private void HandleGrabInteraction()
        {
            if (bngPlayerController == null)
            {
                return;
            }

            // Check for grab input
            if (bngPlayerController.GetButtonDown("LeftGrip"))
            {
                TryGrabObject(leftHandInteractor);
            }
            else if (bngPlayerController.GetButtonDown("RightGrip"))
            {
                TryGrabObject(rightHandInteractor);
            }

            // Check for drop input
            if (bngPlayerController.GetButtonDown("LeftTrigger"))
            {
                TryDropObject(leftHandInteractor);
            }
            else if (bngPlayerController.GetButtonDown("RightTrigger"))
            {
                TryDropObject(rightHandInteractor);
            }
        }

        private void TryGrabObject(XRDirectInteractor interactor)
        {
            if (interactor == null)
            {
                Debug.LogError("Interactor is null. Cannot grab object.");
                return;
            }

            Collider[] colliders = Physics.OverlapSphere(interactor.transform.position, grabRange, grabbableLayer);
            if (colliders.Length > 0)
            {
                foreach (var collider in colliders)
                {
                    if (collider.TryGetComponent(out XRBaseInteractable interactable))
                    {
                        interactor.interactionManager.SelectEnter(interactor, interactable);
                        Debug.Log($"Grabbed object: {collider.gameObject.name}");
                        return;
                    }
                }
            }
            else
            {
                Debug.Log("No grabbable object found within range.");
            }
        }

        private void TryDropObject(XRDirectInteractor interactor)
        {
            if (interactor == null)
            {
                Debug.LogError("Interactor is null. Cannot drop object.");
                return;
            }

            if (interactor.hasSelection)
            {
                interactor.interactionManager.SelectExit(interactor, interactor.selectTarget);
                Debug.Log("Dropped object.");
            }
            else
            {
                Debug.Log("No object to drop.");
            }
        }

        public void UseItem(GameObject item)
        {
            if (item == null)
            {
                Debug.LogError("Item is null. Cannot use item.");
                return;
            }

            if (item.TryGetComponent(out IUsable usable))
            {
                usable.Use();
                Debug.Log($"Used item: {item.name}");
            }
            else
            {
                Debug.LogError($"The object {item.name} does not implement the IUsable interface.");
            }
        }

        public void InteractWithEnvironment(GameObject target)
        {
            if (target == null)
            {
                Debug.LogError("Target is null. Cannot interact with environment.");
                return;
            }

            if (target.TryGetComponent(out IInteractable interactable))
            {
                interactable.Interact();
                Debug.Log($"Interacted with: {target.name}");
            }
            else
            {
                Debug.LogError($"The object {target.name} does not implement the IInteractable interface.");
            }
        }
    }

    public interface IUsable
    {
        void Use();
    }

    public interface IInteractable
    {
        void Interact();
    }
}
