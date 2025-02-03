using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

namespace ArenaDeathMatch.Networking
{
    /// <summary>
    /// NetworkManager handles all multiplayer functionality using Photon PUN 2.
    /// It connects to the Photon server, joins lobbies and rooms, and handles player spawning.
    /// </summary>
    public class NetworkManager : MonoBehaviourPunCallbacks
    {
        public static NetworkManager Instance { get; private set; }

        [Header("Photon Settings")]
        [Tooltip("Game version for Photon connections.")]
        public string gameVersion = "1.0";
        [Tooltip("Name of the player prefab to spawn.")]
        public string playerPrefabName = "PlayerPrefab";

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
            ConnectToServer();
        }

        /// <summary>
        /// Initiates connection to the Photon server.
        /// </summary>
        public void ConnectToServer()
        {
            if (!PhotonNetwork.IsConnected && !PhotonNetwork.IsConnecting)
            {
                PhotonNetwork.GameVersion = gameVersion;
                InitializePhotonConnection();
            }
        }

        /// <summary>
        /// Initializes the Photon connection with streamlined settings.
        /// </summary>
        private void InitializePhotonConnection()
        {
            PhotonNetwork.ConnectUsingSettings();
            Debug.Log("Initializing Photon Connection: Connecting to Photon server with game version " + gameVersion);
        }
        
        /// <summary>
        /// Callback when successfully connected to the Photon Master Server.
        /// </summary>
        public override void OnConnectedToMaster()
        {
            Debug.Log("Connected to Photon Master Server.");
            PhotonNetwork.JoinLobby();
        }

        /// <summary>
        /// Callback when successfully joined a lobby.
        /// </summary>
        public override void OnJoinedLobby()
        {
            Debug.Log("Joined the lobby successfully.");
            // Optionally, you can auto-join a room here or wait for user input.
        }

        /// <summary>
        /// Joins or creates a room with the specified name.
        /// </summary>
        /// <param name="roomName">Name of the room to join.</param>
        public void JoinRoom(string roomName)
        {
            if (PhotonNetwork.InLobby)
            {
                RoomOptions roomOptions = new RoomOptions { MaxPlayers = 8 };
                PhotonNetwork.JoinOrCreateRoom(roomName, roomOptions, TypedLobby.Default);
                Debug.Log("Attempting to join or create room: " + roomName);
            }
            else
            {
                Debug.LogWarning("Not in lobby. Cannot join room.");
            }
        }

        /// <summary>
        /// Callback when successfully joined a room.
        /// </summary>
        public override void OnJoinedRoom()
        {
            Debug.Log("Joined room: " + PhotonNetwork.CurrentRoom.Name);
            SpawnPlayer();
        }

        /// <summary>
        /// Spawns the local player using PhotonNetwork.Instantiate.
        /// </summary>
        public void SpawnPlayer()
        {
            if (PhotonNetwork.IsConnected && PhotonNetwork.InRoom)
            {
                Vector3 spawnPosition = GetSpawnPosition();
                Quaternion spawnRotation = Quaternion.identity;
                PhotonNetwork.Instantiate(playerPrefabName, spawnPosition, spawnRotation);
                Debug.Log("Player spawned at position: " + spawnPosition);
            }
            else
            {
                Debug.LogWarning("Cannot spawn player; not connected or not in room.");
            }
        }

        /// <summary>
        /// Provides a spawn position for the player.
        /// </summary>
        /// <returns>Random spawn position within defined range.</returns>
        private Vector3 GetSpawnPosition()
        {
            return new Vector3(Random.Range(-5f, 5f), 1f, Random.Range(-5f, 5f));
        }

        /// <summary>
        /// Callback when disconnected from Photon.
        /// </summary>
        /// <param name="cause">Reason for disconnection.</param>
        public override void OnDisconnected(DisconnectCause cause)
        {
            Debug.LogWarning("Disconnected from Photon server: " + cause);
        }
    }
}