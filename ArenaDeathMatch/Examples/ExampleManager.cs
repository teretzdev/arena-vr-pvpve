using System;

namespace ArenaDeathMatch.Examples
{
    public class ExampleManager
    {
        private GameplayTutorial gameplayTutorial;
        private WeaponSystemDemo weaponSystemDemo;

        public ExampleManager()
        {
            gameplayTutorial = new GameplayTutorial();
            weaponSystemDemo = new WeaponSystemDemo();
        }

        public void UpdateExamples()
        {
            gameplayTutorial.Update();
            weaponSystemDemo.Update();
        }
    }

    public class GameplayTutorial
    {
        public void StartTutorial()
        {
            Console.WriteLine("Starting gameplay tutorial.");
            // Logic to start the tutorial
        }

        public void EndTutorial()
        {
            Console.WriteLine("Ending gameplay tutorial.");
            // Logic to end the tutorial
        }

        public void Update()
        {
            // Update tutorial logic
            Console.WriteLine("Updating gameplay tutorial.");
        }
    }

    public class WeaponSystemDemo
    {
        public void StartDemo()
        {
            Console.WriteLine("Starting weapon system demo.");
            // Logic to start the demo
        }

        public void EndDemo()
        {
            Console.WriteLine("Ending weapon system demo.");
            // Logic to end the demo
        }

        public void Update()
        {
            // Update demo logic
            Console.WriteLine("Updating weapon system demo.");
        }
    }
}
