using UnityEngine;
using ArenaDeathMatch.AdventureCreator;
using UnityEngine.InputSystem;

namespace ArenaDeathMatch.Characters
{
    /// <summary>
    /// PlayerController handles the player’s movement and interactions.
    /// It has been updated to integrate with the Adventure Creator plugin so that
    /// players can trigger dialogues and interactive events during gameplay.
    /// </summary>
    [RequireComponent(typeof(CharacterController))]
    public class PlayerController : MonoBehaviour
    {
        [Header("Movement Settings")]
        [Tooltip("Movement speed of the player.")]
        public float moveSpeed = 5f;
        public InputAction moveAction;
        public InputAction dialogueAction;
        public InputAction interactiveAction;
        public InputAction cutsceneAction;
        private CharacterController characterController;

        private void Awake()
        {
            characterController = GetComponent<CharacterController>();
            if (characterController == null)
            {
                characterController = gameObject.AddComponent<CharacterController>();
            }
        }

        private void OnEnable()
        {
            moveAction?.Enable();
            dialogueAction?.Enable();
            interactiveAction?.Enable();
            cutsceneAction?.Enable();
            dialogueAction.performed += OnDialoguePerformed;
            interactiveAction.performed += OnInteractivePerformed;
            cutsceneAction.performed += OnCutscenePerformed;
        }

        private void OnDisable()
        {
            moveAction?.Disable();
            dialogueAction.performed -= OnDialoguePerformed;
            interactiveAction.performed -= OnInteractivePerformed;
            cutsceneAction.performed -= OnCutscenePerformed;
            dialogueAction?.Disable();
            interactiveAction?.Disable();
            cutsceneAction?.Disable();
        }

        private void Update()
        {
            ProcessMovement();
        }

        /// <summary>
        /// Processes player movement using standard WASD or arrow keys.
        /// </summary>
        private void ProcessMovement()
        {
            Vector2 movementInput = moveAction.ReadValue<Vector2>();
            float horizontal = movementInput.x;
            float vertical = movementInput.y;

            // Create move vector in local space
            Vector3 moveDirection = transform.right * horizontal + transform.forward * vertical;
            if (moveDirection.magnitude > 1f)
            {
                moveDirection.Normalize();
            }

            // Move the character controller
            characterController.Move(moveDirection * moveSpeed * Time.deltaTime);
        }

        private void ProcessDialogue()
        {
            if (AdventureCreatorManager.Instance != null)
            {
                AdventureCreatorManager.Instance.StartDialogue("PlayerInteractionDialogue");
            }
            else
            {
                Debug.LogWarning("AdventureCreatorManager instance is not available to start dialogue.");
            }
        }
        
        private void ProcessInteractive()
        {
            if (AdventureCreatorManager.Instance != null)
            {
                AdventureCreatorManager.Instance.StartInteractiveEvent("PlayerInteractionEvent");
            }
            else
            {
                Debug.LogWarning("AdventureCreatorManager instance is not available to trigger interactive event.");
            }
        }
        
        private void ProcessCutscene()
        {
            if (AdventureCreatorManager.Instance != null)
            {
                AdventureCreatorManager.Instance.TriggerCutscene("PlayerCutscene");
            }
            else
            {
                Debug.LogWarning("AdventureCreatorManager instance is not available to trigger cutscene.");
            }
        }
        
        private void OnDialoguePerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            ProcessDialogue();
        }
        
        private void OnInteractivePerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            ProcessInteractive();
        }
        
        private void OnCutscenePerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
        {
            ProcessCutscene();
        }
    }
}