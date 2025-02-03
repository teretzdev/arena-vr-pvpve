using System.Collections.Generic;
using UnityEngine;

namespace ArenaDeathMatch.UI
{
    public class UIManager : MonoBehaviour
    {
        public static UIManager Instance { get; private set; }

        [Header("UI References")]
        public Canvas mainCanvas;
        public MenuController menuController;
        public HUDController hudController;
        public VRUIInteractionSystem vrInteraction;

        [Header("Menu Screens")]
        public Dictionary<UIScreenType, UIScreen> screens;
        public UIScreen currentScreen;
        public Stack<UIScreen> screenHistory;

        #region Initialization

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                InitializeUI();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void InitializeUI()
        {
            screenHistory = new Stack<UIScreen>();
            RegisterScreens();
            menuController.Initialize();
            hudController.Initialize();
            vrInteraction.Initialize();
            // Initialize Adventure Creator UI components with dialogue, menu, and interactive prompt support
            if (AdventureCreatorUI != null)
            {
                AdventureCreatorUI.Initialize();
                AdventureCreatorUI.SubscribeEvents();
            }
            else
            {
                Debug.LogWarning("AdventureCreatorUI is not available. Please ensure the Adventure Creator plugin is properly installed.");
            }
        }

        private void RegisterScreens()
        {
            screens = new Dictionary<UIScreenType, UIScreen>();
            foreach (UIScreen screen in GetComponentsInChildren<UIScreen>(true))
            {
                screens[screen.screenType] = screen;
                screen.Initialize();
            }
        }
        
        public void ShowStatisticsScreen(UITransitionData transitionData = null)
        {
            ShowScreen(UIScreenType.StatisticsScreen, transitionData);
        }
        
        #endregion

        #region Screen Management

        public void ShowScreen(UIScreenType screenType, UITransitionData transitionData = null)
        {
            if (!screens.ContainsKey(screenType)) return;

            if (currentScreen != null)
            {
                screenHistory.Push(currentScreen);
                currentScreen.Hide();
            }

            currentScreen = screens[screenType];
            currentScreen.Show(transitionData);
        }

        public void GoBack()
        {
            if (screenHistory.Count == 0) return;

            currentScreen?.Hide();
            currentScreen = screenHistory.Pop();
            currentScreen.Show();
        }

        #endregion

        #region HUD System

        public class HUDController : MonoBehaviour
        {
            [Header("HUD Elements")]
            public HealthDisplay healthDisplay;
            public AmmoCounter ammoCounter;
            public ScoreDisplay scoreDisplay;
            public MinimapSystem minimap;
            public ObjectiveMarker objectiveMarker;
            public DamageIndicator damageIndicator;

            public void Initialize()
            {
                InitializeHUDElements();
                SubscribeCombatEvents();
            }

            private void InitializeHUDElements()
            {
                healthDisplay.Initialize();
                ammoCounter.Initialize();
                scoreDisplay.Initialize();
                minimap.Initialize();
                objectiveMarker.Initialize();
                damageIndicator.Initialize();
            }
            
            private void SubscribeCombatEvents()
            {
                // Subscribe to player damage event from the Combat System
                CombatEvents.OnPlayerDamaged += OnPlayerDamaged;
            }
            
            public void UpdateHUD(HUDUpdateData data)
            {
                switch (data.updateType)
                {
                    case HUDUpdateType.Health:
                        healthDisplay.UpdateHealth(data.value);
                        break;
                    case HUDUpdateType.Ammo:
                        ammoCounter.UpdateAmmo(data.value);
                        break;
                    case HUDUpdateType.Score:
                        scoreDisplay.UpdateScore(data.value);
                        break;
                }
            }

            public void ShowDamageIndicator(Vector3 damageDirection)
            {
                damageIndicator.ShowDamageFrom(damageDirection);
            }
            
            private void OnPlayerDamaged(WeaponManager.DamageInfo damageInfo)
            {
                // Update health display; assuming PlayerHealth.Instance.currentHealth holds the current health value.
                healthDisplay.UpdateHealth(PlayerHealth.Instance.currentHealth);
                // Show damage indicator using the damage direction (using the hit normal as an example)
                ShowDamageIndicator(damageInfo.normal);
            }
        }

        #endregion

        #region VR UI Interaction

        public class VRUIInteractionSystem : MonoBehaviour
        {
            [Header("VR References")]
            public VRHand leftHand;
            public VRHand rightHand;
            public LayerMask uiLayer;
            public float interactionDistance = 5f;

            private List<IVRInteractable> interactables;
            private VRUIPointer activePointer;

            public void Initialize()
            {
                interactables = new List<IVRInteractable>();
                RegisterInteractables();
                SetupVRPointers();
            }

            private void Update()
            {
                UpdatePointerStates();
                CheckInteractions();
            }

            private void UpdatePointerStates()
            {
                leftHand.UpdatePointer();
                rightHand.UpdatePointer();
                UpdateActivePointer();
            }

            private void CheckInteractions()
            {
                if (activePointer == null) return;

                RaycastHit hit;
                if (Physics.Raycast(activePointer.transform.position, 
                    activePointer.transform.forward, out hit, 
                    interactionDistance, uiLayer))
                {
                    IVRInteractable interactable = hit.collider.GetComponent<IVRInteractable>();
                    if (interactable != null)
                    {
                        HandleInteraction(interactable);
                    }
                }
            }
        }

        #endregion
    }

    #region Interfaces

    public interface IVRInteractable
    {
        void OnPointerEnter(VRPointer pointer);
        void OnPointerExit(VRPointer pointer);
        void OnPointerClick(VRPointer pointer);
    }

    #endregion
}