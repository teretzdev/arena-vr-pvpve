using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArenaDeathMatch.GameLogic
{
    /// <summary>
    /// Manages the different phases of the game, such as Initiative, Supporters & Beasts, Soul, Magic, Ranged Combat, Close Combat, etc.
    /// </summary>
    public class GamePhaseManager : MonoBehaviour
    {
        public static GamePhaseManager Instance { get; private set; }

        [Header("Game Phases")]
        public List<GamePhase> gamePhases; // List of all game phases in order
        private int currentPhaseIndex = 0;

        [Header("Phase Timing")]
        public float phaseDuration = 30.0f; // Default duration for each phase in seconds
        private float phaseTimer;

        [Header("Event Callbacks")]
        public Action<GamePhase> OnPhaseStart;
        public Action<GamePhase> OnPhaseEnd;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            if (gamePhases == null || gamePhases.Count == 0)
            {
                Debug.LogError("No game phases defined in GamePhaseManager.");
                return;
            }

            StartPhase(gamePhases[currentPhaseIndex]);
        }

        private void Update()
        {
            if (gamePhases == null || gamePhases.Count == 0)
                return;

            phaseTimer -= Time.deltaTime;

            if (phaseTimer <= 0)
            {
                EndCurrentPhase();
                AdvanceToNextPhase();
            }
        }

        /// <summary>
        /// Starts the specified game phase.
        /// </summary>
        /// <param name="phase">The game phase to start.</param>
        private void StartPhase(GamePhase phase)
        {
            Debug.Log($"Starting phase: {phase.phaseName}");
            phaseTimer = phase.duration > 0 ? phase.duration : phaseDuration;

            // Trigger phase-specific logic
            phase.OnPhaseStart?.Invoke();

            // Notify listeners
            OnPhaseStart?.Invoke(phase);
        }

        /// <summary>
        /// Ends the current game phase.
        /// </summary>
        private void EndCurrentPhase()
        {
            if (currentPhaseIndex < 0 || currentPhaseIndex >= gamePhases.Count)
                return;

            GamePhase currentPhase = gamePhases[currentPhaseIndex];
            Debug.Log($"Ending phase: {currentPhase.phaseName}");

            // Trigger phase-specific logic
            currentPhase.OnPhaseEnd?.Invoke();

            // Notify listeners
            OnPhaseEnd?.Invoke(currentPhase);
        }

        /// <summary>
        /// Advances to the next game phase, looping back to the first phase if necessary.
        /// </summary>
        private void AdvanceToNextPhase()
        {
            currentPhaseIndex = (currentPhaseIndex + 1) % gamePhases.Count;
            StartPhase(gamePhases[currentPhaseIndex]);
        }

        /// <summary>
        /// Manually skips to the next phase.
        /// </summary>
        public void SkipToNextPhase()
        {
            EndCurrentPhase();
            AdvanceToNextPhase();
        }

        /// <summary>
        /// Gets the current game phase.
        /// </summary>
        /// <returns>The current game phase.</returns>
        public GamePhase GetCurrentPhase()
        {
            if (currentPhaseIndex < 0 || currentPhaseIndex >= gamePhases.Count)
                return null;

            return gamePhases[currentPhaseIndex];
        }
    }

    /// <summary>
    /// Represents a single game phase with its associated properties and events.
    /// </summary>
    [System.Serializable]
    public class GamePhase
    {
        [Header("Phase Details")]
        public string phaseName; // Name of the phase
        public float duration; // Duration of the phase in seconds

        [Header("Phase Events")]
        public Action OnPhaseStart; // Event triggered when the phase starts
        public Action OnPhaseEnd; // Event triggered when the phase ends
    }
}
