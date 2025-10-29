using UnityEngine;

namespace Rules
{
    /// <summary>
    /// Modifies point values (multiplier)
    /// </summary>
    public class PointModifierPlayRule : BasePlayRule
    {
        public override string RuleName => GetRuleName();

        private float multiplier;
        private State gameState;

        public PointModifierPlayRule()
        {
            // Randomly choose a multiplier
            int choice = Random.Range(0, 3);
            multiplier = choice switch
            {
                0 => 0.5f, // Half points
                1 => 1.5f, // 1.5x points
                _ => 2.0f // Double points
            };
        }

        private string GetRuleName()
        {
            if (multiplier < 1f)
                return "Half Points";
            else if (multiplier == 1.5f)
                return "Bonus Points (x1.5)";
            else
                return "Double Points";
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
            gameState.pointModifier = multiplier;
            Debug.Log($"Point multiplier set to: {multiplier}x");
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
                gameState.pointModifier = 1f; // Reset to default
            }
        }

        public override bool IsCompatibleWith(IRule otherRule)
        {
            // Only compatible with Battle Mode (Point Race uses points)
            if (otherRule is PointRaceModeSetup)
                return true;

            // Not compatible with Battle Mode
            if (otherRule is BattleModeSetup)
                return false;

            return true;
        }
    }
}