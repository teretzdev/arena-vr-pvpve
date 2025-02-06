using UnityEngine;

namespace ArenaDeathMatch.Physics
{
    /// <summary>
    /// PhysicsManager centralizes physics settings and interactions.
    /// It initializes physics parameters such as gravity, collision layer interactions, and default contact offsets.
    /// </summary>
    public class PhysicsManager : MonoBehaviour
    {
        public static PhysicsManager Instance { get; private set; }

        [Header("General Physics Settings")]
        [Tooltip("Custom gravity vector to be applied in the game world.")]
        public Vector3 customGravity = new Vector3(0f, -9.81f, 0f);

        [Tooltip("Default contact offset used by the physics engine.")]
        public float contactOffset = 0.01f;

        [Header("Collision Layer Settings")]
        [Tooltip("Define pairs of collision layers and whether they should ignore collisions.")]
        public LayerCollision[] layerCollisions;

        /// <summary>
        /// Struct for configuring collision interactions between two layers.
        /// </summary>
        [System.Serializable]
        public struct LayerCollision
        {
            [Tooltip("First layer index.")]
            public int layer1;
            [Tooltip("Second layer index.")]
            public int layer2;
            [Tooltip("Set to true to ignore collisions between these layers.")]
            public bool ignoreCollision;
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            InitializePhysicsSettings();
        }

        /// <summary>
        /// Initializes all physics settings including gravity, contact offset, and layer collision rules.
        /// </summary>
        public void InitializePhysicsSettings()
        {
            SetGravity();
            SetContactOffset();
            ConfigureCollisionLayers();
            Debug.Log("PhysicsManager: Gravity set to " + customGravity + 
                      ", Contact Offset set to " + contactOffset + 
                      ", and collision layers configured.");
        }

        /// <summary>
        /// Sets the global gravity vector to the custom gravity specified.
        /// </summary>
        private void SetGravity()
        {
            Physics.gravity = customGravity;
            Debug.Log("PhysicsManager: Gravity set to " + customGravity);
        }

        /// <summary>
        /// Sets the default contact offset for the physics simulation.
        /// </summary>
        private void SetContactOffset()
        {
            Physics.defaultContactOffset = contactOffset;
            Debug.Log("PhysicsManager: Default contact offset set to " + contactOffset);
        }

        /// <summary>
        /// Configures collision rules between specified layers using Physics.IgnoreLayerCollision.
        /// </summary>
        private void ConfigureCollisionLayers()
        {
            if (layerCollisions != null)
            {
                foreach (LayerCollision collision in layerCollisions)
                {
                    Physics.IgnoreLayerCollision(collision.layer1, collision.layer2, collision.ignoreCollision);
                    Debug.Log("PhysicsManager: Collision between layers " + collision.layer1 + " and " + collision.layer2 +
                              " set to ignore: " + collision.ignoreCollision);
                }
            }
        }
    }
}using UnityEngine;

namespace ArenaDeathMatch.Physics
{
    /// <summary>
    /// PhysicsManager centralizes physics settings and interactions.
    /// It initializes physics parameters such as gravity, collision layer interactions, and default contact offsets.
    /// </summary>
    public class PhysicsManager : MonoBehaviour
    {
        public static PhysicsManager Instance { get; private set; }

        [Header("General Physics Settings")]
        [Tooltip("Custom gravity vector to be applied in the game world.")]
        public Vector3 customGravity = new Vector3(0f, -9.81f, 0f);

        [Tooltip("Default contact offset used by the physics engine.")]
        public float contactOffset = 0.01f;

        [Header("Collision Layer Settings")]
        [Tooltip("Define pairs of collision layers and whether they should ignore collisions.")]
        public LayerCollision[] layerCollisions;

        /// <summary>
        /// Struct for configuring collision interactions between two layers.
        /// </summary>
        [System.Serializable]
        public struct LayerCollision
        {
            [Tooltip("First layer index.")]
            public int layer1;
            [Tooltip("Second layer index.")]
            public int layer2;
            [Tooltip("Set to true to ignore collisions between these layers.")]
            public bool ignoreCollision;
        }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            InitializePhysicsSettings();
        }

        /// <summary>
        /// Initializes all physics settings including gravity, contact offset, and layer collision rules.
        /// </summary>
        public void InitializePhysicsSettings()
        {
            SetGravity();
            SetContactOffset();
            ConfigureCollisionLayers();
            Debug.Log("PhysicsManager: All physics settings have been initialized.");
        }

        /// <summary>
        /// Sets the global gravity vector to the custom gravity specified.
        /// </summary>
        private void SetGravity()
        {
            Physics.gravity = customGravity;
            Debug.Log("PhysicsManager: Gravity set to " + customGravity);
        }

        /// <summary>
        /// Sets the default contact offset for the physics simulation.
        /// </summary>
        private void SetContactOffset()
        {
            Physics.defaultContactOffset = contactOffset;
            Debug.Log("PhysicsManager: Default contact offset set to " + contactOffset);
        }

        /// <summary>
        /// Configures collision rules between specified layers using Physics.IgnoreLayerCollision.
        /// </summary>
        private void ConfigureCollisionLayers()
        {
            if (layerCollisions != null)
            {
                foreach (LayerCollision collision in layerCollisions)
                {
                    Physics.IgnoreLayerCollision(collision.layer1, collision.layer2, collision.ignoreCollision);
                    Debug.Log("PhysicsManager: Collision between layers " + collision.layer1 + " and " + collision.layer2 +
                              " set to ignore: " + collision.ignoreCollision);
                }
            }
        }

        /// <summary>
        /// Resets all physics settings to Unity's default values.
        /// </summary>
        public void ResetPhysicsSettings()
        {
            Physics.gravity = new Vector3(0f, -9.81f, 0f);
            Physics.defaultContactOffset = 0.01f;

            if (layerCollisions != null)
            {
                foreach (LayerCollision collision in layerCollisions)
                {
                    Physics.IgnoreLayerCollision(collision.layer1, collision.layer2, false);
                }
            }

            Debug.Log("PhysicsManager: Physics settings have been reset to default values.");
        }

        /// <summary>
        /// Updates the global gravity vector dynamically.
        /// </summary>
        /// <param name="newGravity">The new gravity vector to apply.</param>
        public void UpdateGravity(Vector3 newGravity)
        {
            customGravity = newGravity;
            SetGravity();
            Debug.Log("PhysicsManager: Gravity updated to " + newGravity);
        }

        /// <summary>
        /// Updates the default contact offset dynamically.
        /// </summary>
        /// <param name="newContactOffset">The new contact offset to apply.</param>
        public void UpdateContactOffset(float newContactOffset)
        {
            contactOffset = newContactOffset;
            SetContactOffset();
            Debug.Log("PhysicsManager: Contact offset updated to " + newContactOffset);
        }
    }
}