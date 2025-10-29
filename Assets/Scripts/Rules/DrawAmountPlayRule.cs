using UnityEngine;

namespace Rules
{
    /// <summary>
    /// Modifies the number of cards drawn per turn
    /// </summary>
    public class DrawAmountPlayRule : BasePlayRule
    {
        public override string RuleName => $"Draw {drawAmount} Card(s)";

        private int drawAmount;
        private State gameState;

        public DrawAmountPlayRule()
        {
            // Randomly choose between 2-4 cards
            drawAmount = Random.Range(2, 5);
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
            gameState.cardsDrawnPerTurn = drawAmount;
            Debug.Log($"Cards drawn per turn set to: {drawAmount}");
        }

        public override void Execute()
        {
            // This rule modifies state on initialization
            // Execute can be used if you need to dynamically change it
        }

        public override void Cleanup()
        {
            base.Cleanup();
            if (gameState != null)
            {
                gameState.cardsDrawnPerTurn = 1; // Reset to default
            }
        }
    }
}