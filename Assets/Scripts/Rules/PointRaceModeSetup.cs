using UnityEngine;
using System.Collections.Generic;

namespace Rules
{
    /// <summary>
    /// Point Race Mode: Race to 50 points
    /// </summary>
    public class PointRaceModeSetup : BaseSetupRule
    {
        public override string[] rulesList => new string[]
        {
            "Race to 50 points",
            "Cards gives 2 points", 
            "Picture cards multiply your score",
            "Blank cards does nothing",
            "you can play up to 3 cards per turn",
            "You start with 1 card"
        };

        public override string RuleName => "Point Race";

        public override string Description =>
            "Race to 50 points! Each card gives 2 points, picture cards multiply your score, " +
            "and you can play up to 3 cards per turn!";

        private State gameState;

        public override void SetupGame()
        {
            gameState = Object.FindObjectOfType<State>();
            if (gameState == null)
            {
                Debug.LogError("State not found in scene!");
                return;
            }

            Debug.Log($"Setting up: {RuleName}");

            // Configure game state for point race mode
            gameState.startingHandSize = 0; // Start with 0 cards
            gameState.maxActionsPerTurn = 3;
            gameState.winConditionType = State.WinConditionType.FirstToPoints;
            gameState.pointsToWin = 50;

            // Reset points
            gameState.playerPoints = 0;
            gameState.opponentPoints = 0;

            // Life not used in point race
            gameState.playerLife = 0;
            gameState.opponentLife = 0;

            Debug.Log("Point Race configured: 0 starting cards, race to 50 points, 3 cards per turn");
        }

        public override void Execute()
        {
            Debug.Log("Executing Point Race scoring");
        }

        /// <summary>
        /// Calculate points for played cards
        /// </summary>
        public int CalculatePoints(List<Sprite> cards)
        {
            if (cards == null || cards.Count == 0)
                return 0;

            int totalPoints = 0;
            int pictureCardMultiplier = 1;

            // First pass: count picture cards for multiplier
            foreach (var card in cards)
            {
                if (IsPictureCard(card))
                {
                    pictureCardMultiplier *= 2;
                }
            }

            // Second pass: calculate points
            foreach (var card in cards)
            {
                if (IsBlankCard(card))
                {
                    // Blank cards do nothing
                    continue;
                }
                else if (IsPictureCard(card))
                {
                    // Picture cards themselves don't give points, just multiply
                    continue;
                }
                else
                {
                    // Regular cards give 2 points
                    totalPoints += 2;
                }
            }

            // Apply multiplier
            totalPoints *= pictureCardMultiplier;

            // Apply game state point modifier (from play rules)
            totalPoints = Mathf.RoundToInt(totalPoints * gameState.pointModifier);

            return totalPoints;
        }

        public bool IsPictureCard(Sprite cardSprite)
        {
            string cardName = cardSprite.name.ToLower();
            return cardName.Contains("jack") || cardName.Contains("queen") ||
                   cardName.Contains("king") || cardName.Contains("knægt") ||
                   cardName.Contains("dame") || cardName.Contains("konge");
        }

        public bool IsBlankCard(Sprite cardSprite)
        {
            string cardName = cardSprite.name.ToLower();
            return cardName.Contains("blank") || cardName.Contains("joker");
        }
    }
}