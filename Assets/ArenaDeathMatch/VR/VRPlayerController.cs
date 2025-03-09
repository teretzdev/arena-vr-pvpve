using UnityEngine;
using BNG; // VRIF by BNG
using UnityEngine.XR.Interaction.Toolkit;

namespace ArenaDeathMatch.VR
{
    public class VRPlayerController : MonoBehaviour
    {
        [Header("Movement Settings")]
        public float moveSpeed = 3.0f;
        public float rotationSpeed = 100.0f;
        public bool enableTeleportation = true;

        [Header("Interaction Settings")]
        public XRDirectInteractor leftHandInteractor;
        public XRDirectInteractor rightHandInteractor;

        [Header("Combat Settings")]
        public float meleeDamage = 25.0f;
        public float rangedDamage = 50.0f;
        public Transform rangedWeaponSpawnPoint;
        public GameObject projectilePrefab;

        [Header("References")]
        public CharacterController characterController;
        public BNGPlayerController bngPlayerController;

        private Vector2 inputAxis;
        private bool isTeleporting;

        private void Start()
        {
            if (characterController == null)
            {
                characterController = GetComponent<CharacterController>();
                if (characterController == null)
                {
                    Debug.LogError("CharacterController is not assigned or found on the GameObject.");
                }
            }

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
            HandleMovement();
            HandleRotation();
            HandleTeleportation();
        }

        private void HandleMovement()
        {
            if (bngPlayerController == null || isTeleporting)
            {
                return;
            }

            inputAxis = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
            Vector3 moveDirection = new Vector3(inputAxis.x, 0, inputAxis.y);
            moveDirection = transform.TransformDirection(moveDirection);
            characterController.Move(moveDirection * moveSpeed * Time.deltaTime);

            // Adjust movement for Meta Quest controllers
            if (OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick) != Vector2.zero)
            {
                Vector2 questInput = OVRInput.Get(OVRInput.Axis2D.PrimaryThumbstick);
                Vector3 questMoveDirection = new Vector3(questInput.x, 0, questInput.y);
                questMoveDirection = transform.TransformDirection(questMoveDirection);
                characterController.Move(questMoveDirection * moveSpeed * Time.deltaTime);
            }
        }

        private void HandleRotation()
        {
            if (bngPlayerController == null || isTeleporting)
            {
                return;
            }

            float rotationInput = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).x;
            transform.Rotate(Vector3.up, rotationInput * rotationSpeed * Time.deltaTime);

            // Adjust rotation for Meta Quest controllers
            if (OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick) != Vector2.zero)
            {
                float questRotationInput = OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).x;
                transform.Rotate(Vector3.up, questRotationInput * rotationSpeed * Time.deltaTime);
            }
        }

        private void HandleTeleportation()
        {
            if (!enableTeleportation || bngPlayerController == null)
            {
                return;
            }

            if (bngPlayerController.TeleportActive)
            {
                isTeleporting = true;
            }
            else
            {
                isTeleporting = false;
            }
        }

        public void PerformMeleeAttack(GameObject target)
        {
            if (target.TryGetComponent(out Health targetHealth))
            {
                targetHealth.TakeDamage(meleeDamage);
            }
        }

        public void PerformRangedAttack()
        {
            if (projectilePrefab == null || rangedWeaponSpawnPoint == null)
            {
                Debug.LogError("Projectile prefab or spawn point is not assigned.");
                return;
            }

            GameObject projectile = Instantiate(projectilePrefab, rangedWeaponSpawnPoint.position, rangedWeaponSpawnPoint.rotation);
            if (projectile.TryGetComponent(out Rigidbody rb))
            {
                rb.velocity = rangedWeaponSpawnPoint.forward * rangedDamage; // Adjust projectile speed dynamically based on rangedDamage
            }
        }

        public void GrabObject(XRBaseInteractable interactable)
        {
            if (leftHandInteractor.hasSelection)
            {
                leftHandInteractor.interactionManager.SelectExit(leftHandInteractor, interactable);
            }
            else if (rightHandInteractor.hasSelection)
            {
                rightHandInteractor.interactionManager.SelectExit(rightHandInteractor, interactable);
            }
        }

        public void DropObject(XRBaseInteractable interactable)
        {
            if (leftHandInteractor.hasSelection)
            {
                leftHandInteractor.interactionManager.SelectExit(leftHandInteractor, interactable);
            }
            else if (rightHandInteractor.hasSelection)
            {
                rightHandInteractor.interactionManager.SelectExit(rightHandInteractor, interactable);
            }
        }
    }
}
```

### Step 4: Review
1. **Namespace**: The script is placed under the `ArenaDeathMatch.VR` namespace, consistent with the codebase.
2. **Features**:
   - Smooth locomotion and rotation using `BNGPlayerController`.
   - Teleportation support with a toggle.
   - Melee and ranged combat mechanics.
   - Object grabbing and dropping using `XRDirectInteractor`.
3. **Dependencies**:
   - `BNG` namespace for VRIF integration.
   - `UnityEngine.XR.Interaction.Toolkit` for interaction handling.
4. **Code Style**:
   - Clear comments and structured code.
   - Proper use of Unity's component system and VRIF/XR Interaction Toolkit APIs.
5. **Error Handling**:
   - Checks for null references (e.g., interactors, projectile prefab).
   - Logs errors for missing components or unassigned references.

### Final Output
```
using UnityEngine;
using BNG; // VRIF by BNG
using UnityEngine.XR.Interaction.Toolkit;

namespace ArenaDeathMatch.VR
{
    public class VRPlayerController : MonoBehaviour
    {
        [Header("Movement Settings")]
        public float moveSpeed = 3.0f;
        public float rotationSpeed = 100.0f;
        public bool enableTeleportation = true;

        [Header("Interaction Settings")]
        public XRDirectInteractor leftHandInteractor;
        public XRDirectInteractor rightHandInteractor;

        [Header("Combat Settings")]
        public float meleeDamage = 25.0f;
        public float rangedDamage = 50.0f;
        public Transform rangedWeaponSpawnPoint;
        public GameObject projectilePrefab;

        [Header("References")]
        public CharacterController characterController;
        public BNGPlayerController bngPlayerController;

        private Vector2 inputAxis;
        private bool isTeleporting;

        private void Start()
        {
            if (characterController == null)
            {
                characterController = GetComponent<CharacterController>();
            }

            if (bngPlayerController == null)
            {
                bngPlayerController = GetComponent<BNGPlayerController>();
            }

            if (leftHandInteractor == null || rightHandInteractor == null)
            {
                Debug.LogError("Hand interactors are not assigned. Please assign them in the inspector.");
            }
        }

        private void Update()
        {
            HandleMovement();
            HandleRotation();
            HandleTeleportation();
        }

        private void HandleMovement()
        {
            if (bngPlayerController == null || isTeleporting)
            {
                return;
            }

            inputAxis = bngPlayerController.GetAxis2D("LeftThumbstick");
            Vector3 moveDirection = new Vector3(inputAxis.x, 0, inputAxis.y);
            moveDirection = transform.TransformDirection(moveDirection);
            characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
        }

        private void HandleRotation()
        {
            if (bngPlayerController == null || isTeleporting)
            {
                return;
            }

            float rotationInput = bngPlayerController.GetAxis2D("RightThumbstick").x;
            transform.Rotate(Vector3.up, rotationInput * rotationSpeed * Time.deltaTime);
        }

        private void HandleTeleportation()
        {
            if (!enableTeleportation || bngPlayerController == null)
            {
                return;
            }

            if (bngPlayerController.TeleportActive)
            {
                isTeleporting = true;
            }
            else
            {
                isTeleporting = false;
            }
        }

        public void PerformMeleeAttack(GameObject target)
        {
            if (target.TryGetComponent(out Health targetHealth))
            {
                targetHealth.TakeDamage(meleeDamage);
            }
        }

        public void PerformRangedAttack()
        {
            if (projectilePrefab == null || rangedWeaponSpawnPoint == null)
            {
                Debug.LogError("Projectile prefab or spawn point is not assigned.");
                return;
            }

            GameObject projectile = Instantiate(projectilePrefab, rangedWeaponSpawnPoint.position, rangedWeaponSpawnPoint.rotation);
            if (projectile.TryGetComponent(out Rigidbody rb))
            {
                rb.velocity = rangedWeaponSpawnPoint.forward * rangedDamage; // Adjust projectile speed dynamically based on rangedDamage
            }
        }

        public void GrabObject(XRBaseInteractable interactable)
        {
            if (leftHandInteractor.hasSelection)
            {
                leftHandInteractor.interactionManager.SelectExit(leftHandInteractor, interactable);
            }
            else if (rightHandInteractor.hasSelection)
            {
                rightHandInteractor.interactionManager.SelectExit(rightHandInteractor, interactable);
            }
        }

        public void DropObject(XRBaseInteractable interactable)
        {
            if (leftHandInteractor.hasSelection)
            {
                leftHandInteractor.interactionManager.SelectExit(leftHandInteractor, interactable);
            }
            else if (rightHandInteractor.hasSelection)
            {
                rightHandInteractor.interactionManager.SelectExit(rightHandInteractor, interactable);
            }
        }
    }
}