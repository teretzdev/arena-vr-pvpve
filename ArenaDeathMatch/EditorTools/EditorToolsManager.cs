using System;

namespace ArenaDeathMatch.EditorTools
{
    public class EditorToolsManager
    {
        private LevelEditorWindow levelEditorWindow;
        private GameSettingsWindow gameSettingsWindow;
        private DebugTools debugTools;

        public EditorToolsManager()
        {
            levelEditorWindow = new LevelEditorWindow();
            gameSettingsWindow = new GameSettingsWindow();
            debugTools = new DebugTools();
        }

        public void UpdateEditorTools()
        {
            levelEditorWindow.Update();
            gameSettingsWindow.Update();
            debugTools.Update();
        }
    }

    public class LevelEditorWindow
    {
        public void Open()
        {
            Console.WriteLine("Level Editor Window opened.");
        }

        public void Close()
        {
            Console.WriteLine("Level Editor Window closed.");
        }

        public void Update()
        {
            // Update level editor logic
            Console.WriteLine("Updating Level Editor Window.");
        }
    }

    public class GameSettingsWindow
    {
        public void Open()
        {
            Console.WriteLine("Game Settings Window opened.");
        }

        public void Close()
        {
            Console.WriteLine("Game Settings Window closed.");
        }

        public void Update()
        {
            // Update game settings logic
            Console.WriteLine("Updating Game Settings Window.");
        }
    }

    public class DebugTools
    {
        public void EnableDebugMode()
        {
            Console.WriteLine("Debug mode enabled.");
        }

        public void DisableDebugMode()
        {
            Console.WriteLine("Debug mode disabled.");
        }

        public void Update()
        {
            // Update debug tools logic
            Console.WriteLine("Updating Debug Tools.");
        }
    }
}
