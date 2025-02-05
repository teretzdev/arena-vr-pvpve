using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

namespace ArenaDeathMatch.Core
// File moved to 'Assets/ArenaDeathMatch/Core/GameManager.cs' to suit Unity3D project structure.
{
    /// <summary>
    /// Core game manager responsible for managing game flow, state transitions, and integrating networking via NetworkManager.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        public static GameManager Instance { get; private set; }

        [Header("Game Configuration")]
        public GameSettings gameSettings;
        public GameState currentState;
        public GameMode currentMode;

        [Header("State Management")]
        public StateManager stateManager;
        public RoundManager roundManager;
        public ScoreManager scoreManager;
        public MatchmakingManager matchmaking;

        [Header("Events")]
        public GameEventSystem eventSystem;

        private void Awake()
        {
            Instance = this;
            InitializeGame();
        }

        #region Game Management

        [System.Serializable]
        public class GameSettings
        {
            public float roundTime = 300f; // 5 minutes
            public int scoreToWin = 50;
            public int maxPlayers = 8;
            public float respawnTime = 5f;
            public bool allowLateJoin = true;
            public DifficultyLevel difficulty = DifficultyLevel.Normal;
        }

        /// <summary>
        /// Initializes game components including state managers, round manager, score manager, matchmaking, events, and network connection.
        /// </summary>
        private void InitializeGame()
        {
            stateManager.Initialize();
            roundManager.Initialize(gameSettings);
            scoreManager.Initialize();
            matchmaking.Initialize(gameSettings.maxPlayers);
            eventSystem.Initialize();
            if (ArenaDeathMatch.Networking.NetworkManager.Instance != null) { ArenaDeathMatch.Networking.NetworkManager.Instance.ConnectToServer(); }
        }

        /// <summary>
        /// Starts the game by transitioning state, starting the first round and triggering the game start event.
        /// </summary>
        public void StartGame()
        {
            if (!CanStartGame())
                return;
            
            stateManager.TransitionTo(GameState.Starting);
            roundManager.StartFirstRound();
            eventSystem.TriggerEvent(GameEventType.GameStart);
        }

        private bool CanStartGame()
        {
            return PhotonNetwork.CurrentRoom != null &&
                   PhotonNetwork.CurrentRoom.PlayerCount >= 2 &&
                   currentState == GameState.Lobby;
        }

        /// <summary>
        /// Ends the game by transitioning state, processing game results and triggering the game end event.
        /// </summary>
        public void EndGame(EndGameReason reason)
        {
            stateManager.TransitionTo(GameState.Ending);
            ProcessGameResults();
            eventSystem.TriggerEvent(GameEventType.GameEnd, reason);
        }

        #endregion

        #region State Management

        public class StateManager
        {
            private Dictionary<GameState, IGameState> states;
            private IGameState currentState;

            public void Initialize()
            {
                states = new Dictionary<GameState, IGameState>
                {
                    { GameState.Lobby, new LobbyState() },
                    { GameState.Starting, new StartingState() },
                    { GameState.Playing, new PlayingState() },
                    { GameState.Paused, new PausedState() },
                    { GameState.Ending, new EndingState() }
                };
            }

            public void TransitionTo(GameState newState)
            {
                currentState?.OnExit();
                currentState = states[newState];
                currentState.OnEnter();
                GameManager.Instance.currentState = newState;
            }

            public void UpdateState()
            {
                currentState?.OnUpdate();
            }
        }

        public interface IGameState
        {
            void OnEnter();
            void OnUpdate();
            void OnExit();
        }

        #endregion

        #region Round Management

        public class RoundManager
        {
            public int currentRound { get; private set; }
            public float roundTimeRemaining { get; private set; }
            private GameSettings settings;
            private bool isRoundActive;

            public void Initialize(GameSettings gameSettings)
            {
                settings = gameSettings;
                currentRound = 0;
            }

            public void StartRound()
            {
                currentRound++;
                roundTimeRemaining = settings.roundTime;
                isRoundActive = true;
                SpawnPlayers();
                GameManager.Instance.eventSystem.TriggerEvent(GameEventType.RoundStart, currentRound);
            }
            
            public void StartFirstRound()
            {
                StartRound();
            }

            private void UpdateRound()
            {
                if (!isRoundActive) return;

                roundTimeRemaining -= Time.deltaTime;
                if (roundTimeRemaining <= 0)
                {
                    EndRound(RoundEndReason.TimeUp);
                }
            }

            private void EndRound(RoundEndReason reason)
            {
                isRoundActive = false;
                GameManager.Instance.eventSystem.TriggerEvent(GameEventType.RoundEnd, reason);
                ProcessRoundResults();
            }

            private void SpawnPlayers()
            {
                foreach (var player in PhotonNetwork.PlayerList)
                {
                    if (player.IsLocal)
                    {
                        GameManager.Instance.matchmaking.SpawnPlayer();
                    }
                }
            }
        }

        #endregion

        #region Score Management

        public class ScoreManager
        {
            private Dictionary<int, PlayerScore> playerScores;
            private List<ScoreEvent> scoreHistory;

            public void Initialize()
            {
                playerScores = new Dictionary<int, PlayerScore>();
                scoreHistory = new List<ScoreEvent>();
            }

            public void AddScore(int playerId, int points, ScoreType type)
            {
                if (!playerScores.ContainsKey(playerId))
                {
                    playerScores[playerId] = new PlayerScore(playerId);
                }

                playerScores[playerId].AddPoints(points);
                RecordScoreEvent(playerId, points, type);
                CheckWinCondition(playerId);
            }

            private void RecordScoreEvent(int playerId, int points, ScoreType type)
            {
                scoreHistory.Add(new ScoreEvent
                {
                    playerId = playerId,
                    points = points,
                    type = type,
                    timestamp = Time.time
                });

                GameManager.Instance.eventSystem.TriggerEvent(GameEventType.ScoreUpdate, 
                    new ScoreUpdateData(playerId, points, type));
            }

            private void CheckWinCondition(int playerId)
            {
                if (playerScores[playerId].totalScore >= GameManager.Instance.gameSettings.scoreToWin)
                {
                    GameManager.Instance.EndGame(EndGameReason.ScoreReached);
                }
            }
        }

        #endregion

        #region Matchmaking

        public class MatchmakingManager
        {
            private int maxPlayers;
            private Queue<PlayerMatchData> matchmakingQueue;
            private Dictionary<int, PlayerMatchData> activePlayers;

            public void Initialize(int maxPlayerCount)
            {
                maxPlayers = maxPlayerCount;
                matchmakingQueue = new Queue<PlayerMatchData>();
                activePlayers = new Dictionary<int, PlayerMatchData>();
            }

            public void QueuePlayer(PlayerMatchData playerData)
            {
                matchmakingQueue.Enqueue(playerData);
                TryMatchPlayers();
            }

            private void TryMatchPlayers()
            {
                if (matchmakingQueue.Count >= 2)
                {
                    CreateMatch();
                }
            }

            private void CreateMatch()
            {
                List<PlayerMatchData> matchedPlayers = new List<PlayerMatchData>();
                while (matchedPlayers.Count < maxPlayers && matchmakingQueue.Count > 0)
                {
                    matchedPlayers.Add(matchmakingQueue.Dequeue());
                }

                StartMatch(matchedPlayers);
            }
        }

        #endregion

        #region Event System

        public class GameEventSystem
        {
            private Dictionary<GameEventType, System.Action<object>> eventHandlers;

            public void Initialize()
            {
                eventHandlers = new Dictionary<GameEventType, System.Action<object>>();
            }

            public void Subscribe(GameEventType eventType, System.Action<object> handler)
            {
                if (!eventHandlers.ContainsKey(eventType))
                {
                    eventHandlers[eventType] = handler;
                }
                else
                {
                    eventHandlers[eventType] += handler;
                }
            }

            public void Unsubscribe(GameEventType eventType, System.Action<object> handler)
            {
                if (eventHandlers.ContainsKey(eventType))
                {
                    eventHandlers[eventType] -= handler;
                }
            }

            public void TriggerEvent(GameEventType eventType, object data = null)
            {
                if (eventHandlers.ContainsKey(eventType))
                {
                    eventHandlers[eventType]?.Invoke(data);
                }
            }
        }

        #endregion

        #region Data Structures

        [System.Serializable]
        public class PlayerScore
        {
            public int playerId;
            public int totalScore;
            public int kills;
            public int deaths;
            public int assists;
            public float accuracy;

            public PlayerScore(int id)
            {
                playerId = id;
            }

            public void AddPoints(int points)
            {
                totalScore += points;
            }
        }

        public struct ScoreEvent
        {
            public int playerId;
            public int points;
            public ScoreType type;
            public float timestamp;
        }

        public class PlayerMatchData
        {
            public int playerId;
            public float skillRating;
            public int ping;
            public Region preferredRegion;
        }

        #endregion

        #region Enums

        public enum GameState
        {
            Lobby,
            Starting,
            Playing,
            Paused,
            Ending
        }

        public enum GameMode
        {
            Deathmatch,
            TeamDeathmatch,
            CaptureTheFlag,
            Domination
        }

        public enum ScoreType
        {
            Kill,
            Assist,
            Objective,
            Bonus
        }

        public enum EndGameReason
        {
            ScoreReached,
            TimeUp,
            ServerShutdown,
            TeamVictory
        }

        public enum DifficultyLevel
        {
            Easy,
            Normal,
            Hard,
            Expert
        }

        #endregion
    }
}