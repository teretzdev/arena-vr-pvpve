#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace ArenaDeathMatch.EditorTools
{
    public static class EditorToolsManager
    {
        [MenuItem("Arena Death Match/Tools/Level Editor")]
        public static void OpenLevelEditor()
        {
            LevelEditorWindow.ShowWindow();
        }

        [MenuItem("Arena Death Match/Tools/Game Settings")]
        public static void OpenGameSettings()
        {
            GameSettingsWindow.ShowWindow();
        }

        [MenuItem("Arena Death Match/Tools/Debug Tools")]
        public static void OpenDebugTools()
        {
            DebugToolsWindow.ShowWindow();
        }
    }

    #region Level Editor

    public class LevelEditorWindow : EditorWindow
    {
        private Vector2 scrollPosition;

        public static void ShowWindow()
        {
            GetWindow<LevelEditorWindow>("Level Editor");
        }

        private void OnGUI()
        {
            scrollPosition = EditorGUILayout.BeginScrollView(scrollPosition);
            EditorGUILayout.LabelField("Level Editor", EditorStyles.boldLabel);
            // Add GUI elements for level editing
            EditorGUILayout.EndScrollView();
        }
    }

    #endregion

    #region Game Settings Editor

    public class GameSettingsWindow : EditorWindow
    {
        public static void ShowWindow()
        {
            GetWindow<GameSettingsWindow>("Game Settings");
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Game Settings", EditorStyles.boldLabel);
            // Add GUI elements for game settings configuration
        }
    }

    #endregion

    #region Debug Tools

    public class DebugToolsWindow : EditorWindow
    {
        public static void ShowWindow()
        {
            GetWindow<DebugToolsWindow>("Debug Tools");
        }

        private void OnGUI()
        {
            EditorGUILayout.LabelField("Debug Tools", EditorStyles.boldLabel);
            // Add GUI elements for debug tools
        }
    }

    #endregion
}
#endif
