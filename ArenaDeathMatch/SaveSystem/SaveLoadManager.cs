using System;
using System.IO;
using UnityEngine;

namespace ArenaDeathMatch.SaveSystem
{
    /// <summary>
    /// SaveLoadManager handles game data persistence using JSON serialization and file I/O.
    /// It provides functionality to save, load, and delete game save data.
    /// </summary>
    public class SaveLoadManager : MonoBehaviour
    {
        public static SaveLoadManager Instance { get; private set; }
        private string saveFilePath;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
                saveFilePath = Path.Combine(Application.persistentDataPath, "savegame.json");
            }
            else
            {
                Destroy(gameObject);
            }
        }

        /// <summary>
        /// Saves the provided game data to a JSON file.
        /// </summary>
        /// <param name="data">The game data to be saved.</param>
        public void SaveGame(SaveData data)
        {
            try
            {
                string jsonData = JsonUtility.ToJson(data, true);
                File.WriteAllText(saveFilePath, jsonData);
                Debug.Log("Game data saved successfully to " + saveFilePath);
            }
            catch (Exception ex)
            {
                Debug.LogError("Failed to save game data: " + ex.Message);
            }
        }

        /// <summary>
        /// Loads the game data from the JSON file.
        /// </summary>
        /// <returns>The loaded game data, or null if no save file is found or an error occurs.</returns>
        public SaveData LoadGame()
        {
            try
            {
                if (File.Exists(saveFilePath))
                {
                    string jsonData = File.ReadAllText(saveFilePath);
                    SaveData data = JsonUtility.FromJson<SaveData>(jsonData);
                    Debug.Log("Game data loaded successfully from " + saveFilePath);
                    return data;
                }
                else
                {
                    Debug.LogWarning("Save file not found at " + saveFilePath);
                    return null;
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Failed to load game data: " + ex.Message);
                return null;
            }
        }

        /// <summary>
        /// Deletes the current save file.
        /// </summary>
        public void DeleteSave()
        {
            try
            {
                if (File.Exists(saveFilePath))
                {
                    File.Delete(saveFilePath);
                    Debug.Log("Save file deleted: " + saveFilePath);
                }
                else
                {
                    Debug.LogWarning("No save file exists to delete at " + saveFilePath);
                }
            }
            catch (Exception ex)
            {
                Debug.LogError("Failed to delete save file: " + ex.Message);
            }
        }
    }

    /// <summary>
    /// SaveData holds the game state that can be persisted.
    /// Extend this class with additional fields as needed.
    /// </summary>
    [Serializable]
    public class SaveData
    {
        public int playerScore;
        public Vector3 playerPosition;
        public float gameTime;
        // Add additional game state members as required.
    }
}
