using UnityEngine;

namespace Rules
{
    /// <summary>
    /// Battle Mode: Last man standing combat system
    /// </summary>
    public class BattleModeSetup : BaseSetupRule
    {
        public override string[] rulesList => new string[]
        {
            "Cards deal 1 damage", 
            "Picture cards deal 2 damage", 
            "Aces heal 2 life", 
            "Blank cards reflect damage back",
            "4 starting cards", 
            "Start with 20 life", 
            "1 card per turn"
        };

        public override string RuleName => "Battle Mode";

        public override string Description =>
            "Combat mode where players fight with cards until one player's life reaches zero. " +
            "Cards deal damage, Aces heal, and blank cards reflect damage back!";

        private State gameState;

        public override void SetupGame()
        {
            gameState = Object.FindFirstObjectByType<State>();
            if (gameState == null)
            {
                Debug.LogError("State not found in scene!");
                return;
            }

            Debug.Log($"Setting up: {RuleName}");

            // Configure game state for battle mode
            gameState.startingHandSize = 4;
            gameState.playerLife = 20;
            gameState.opponentLife = 20;
            gameState.maxActionsPerTurn = 1;
            gameState.winConditionType = State.WinConditionType.LastManStanding;

            // Reset points (not used in battle mode)
            gameState.playerPoints = 0;
            gameState.opponentPoints = 0;

            Debug.Log("Battle Mode configured: 4 starting cards, 20 life, 1 card per turn");
        }

        public override void Execute()
        {
            // This would be called during card resolution to apply battle rules
            Debug.Log("Executing Battle Mode card resolution");
            // Card damage logic would go here
        }

        /// <summary>
        /// Calculate damage for a card in battle mode
        /// </summary>
        public int CalculateCardDamage(Sprite cardSprite)
        {
            string cardName = cardSprite.name.ToLower();

            // Blank cards don't deal damage (they reflect)
            if (cardName.Contains("blank") || cardName.Contains("joker"))
            {
                return 0;
            }

            int damage = 1; // Base damage

            // Picture cards deal +2 extra damage (total 3)
            if (cardName.Contains("jack") || cardName.Contains("queen") || cardName.Contains("king"))
            {
                damage += 2;
            }

            return damage;
        }

        /// <summary>
        /// Check if card is an Ace (heals 2 life)
        /// </summary>
        public bool IsAce(Sprite cardSprite)
        {
            return cardSprite.name.ToLower().Contains("ace") || cardSprite.name.ToLower().Contains("es");
        }

        /// <summary>
        /// Check if card is blank (reflects damage)
        /// </summary>
        public bool IsBlankCard(Sprite cardSprite)
        {
            string cardName = cardSprite.name.ToLower();
            return cardName.Contains("blank") || cardName.Contains("joker");
        }
    }
}