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
    }
}