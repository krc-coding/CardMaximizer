using UnityEngine;

namespace Rules
{
    /// <summary>
    /// Adds a time limit to each turn
    /// </summary>
    public class TurnTimerPlayRule : BasePlayRule
    {
        public override string RuleName => $"{timeLimit} Second Timer";

        private int timeLimit;
        private State gameState;

        public TurnTimerPlayRule()
        {
            // Randomly choose a time limit
            int[] possibleTimes = { 10, 15, 20, 30 };
            timeLimit = possibleTimes[Random.Range(0, possibleTimes.Length)];
        }

        public override void Initialize()
        {
            base.Initialize();

            gameState = Object.FindObjectOfType<State>();
            if (gameState == null)
            {
                Debug.LogError("State not found in scene!");
                return;
            }

            // Modify the game state
            gameState.turnTimeLimit = timeLimit;
            Debug.Log($"Turn time limit set to: {timeLimit} seconds");
        }

        public override void Execute()
        {
            // Actual timer logic would be handled by a TurnManager
        }

        public override void Cleanup()
        {
            base.Cleanup();
            if (gameState != null)
            {
                gameState.turnTimeLimit = 0f; // Reset to no limit
            }
        }
    }
}