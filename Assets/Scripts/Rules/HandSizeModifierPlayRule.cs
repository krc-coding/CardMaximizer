using UnityEngine;

namespace Rules
{
    /// <summary>
    /// Modifies maximum hand size
    /// </summary>
    public class HandSizePlayRule : BasePlayRule
    {
        public override string RuleName => GetRuleName();

        private int handSizeModifier;
        private State gameState;
        private int originalMaxHandSize;

        public HandSizePlayRule()
        {
            // Randomly increase or decrease hand size
            handSizeModifier = Random.Range(-2, 4); // -2 to +3
        }

        private string GetRuleName()
        {
            if (handSizeModifier > 0)
                return $"Larger Hand (+{handSizeModifier})";
            else if (handSizeModifier < 0)
                return $"Smaller Hand ({handSizeModifier})";
            else
                return "Normal Hand Size";
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

            originalMaxHandSize = gameState.maxHandSize;
            gameState.maxHandSize = Mathf.Max(1, originalMaxHandSize + handSizeModifier);
            Debug.Log($"Max hand size set to: {gameState.maxHandSize}");
        }

        public override void Execute()
        {
            // This rule modifies state on initialization
        }

        public override void Cleanup()
        {
            base.Cleanup();
            if (gameState != null)
            {
                gameState.maxHandSize = originalMaxHandSize;
            }
        }
    }
}