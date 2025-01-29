using System;
using System.Collections.Generic;

namespace ArenaDeathMatch.Core
{
    public class GameManager
    {
        private StateManager stateManager;
        private RoundManager roundManager;
        private ScoreManager scoreManager;
        private MatchmakingManager matchmakingManager;

        public GameManager()
        {
            stateManager = new StateManager();
            roundManager = new RoundManager();
            scoreManager = new ScoreManager();
            matchmakingManager = new MatchmakingManager();
        }

        public void StartGame()
        {
            Console.WriteLine("Game started.");
            stateManager.SetState("Playing");
            roundManager.StartRound();
        }

        public void EndGame()
        {
            Console.WriteLine("Game ended.");
            stateManager.SetState("Ended");
        }

        public void UpdateGame()
        {
            stateManager.Update();
            roundManager.Update();
            scoreManager.Update();
            matchmakingManager.Update();
        }
    }

    public class StateManager
    {
        private string currentState;

        public void SetState(string state)
        {
            currentState = state;
            Console.WriteLine($"State changed to: {currentState}");
        }

        public void Update()
        {
            // Update state logic
            Console.WriteLine("Updating state.");
        }
    }

    public class RoundManager
    {
        private int currentRound;

        public void StartRound()
        {
            currentRound++;
            Console.WriteLine($"Round {currentRound} started.");
        }

        public void Update()
        {
            // Update round logic
            Console.WriteLine("Updating round.");
        }
    }

    public class ScoreManager
    {
        private Dictionary<string, int> playerScores;

        public ScoreManager()
        {
            playerScores = new Dictionary<string, int>();
        }

        public void AddScore(string player, int score)
        {
            if (!playerScores.ContainsKey(player))
            {
                playerScores[player] = 0;
            }
            playerScores[player] += score;
            Console.WriteLine($"Player {player} score updated to: {playerScores[player]}");
        }

        public void Update()
        {
            // Update score logic
            Console.WriteLine("Updating scores.");
        }
    }

    public class MatchmakingManager
    {
        public void FindMatch()
        {
            // Matchmaking logic
            Console.WriteLine("Finding match.");
        }

        public void Update()
        {
            // Update matchmaking logic
            Console.WriteLine("Updating matchmaking.");
        }
    }
}
