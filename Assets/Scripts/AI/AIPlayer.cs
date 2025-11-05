using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Rules;

namespace AI
{
    public class AIPlayer : MonoBehaviour
    {
        [Header("References")] [SerializeField]
        private State state;

        [SerializeField] private RuleManager ruleManager;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private CardDrop cardDropZone;

        [Header("AI Settings")] [SerializeField]
        private float thinkingDelay = 1f;

        [SerializeField] private float cardPlayDelay = 0.5f;
        [SerializeField] private AIPersonality personality = AIPersonality.Balanced;

        public enum AIPersonality
        {
            Aggressive, // Prioritizes damage/points
            Defensive, // Prioritizes survival
            Balanced // Mix of both
        }

        private void Start()
        {
            if (state == null) state = FindObjectOfType<State>();
            if (ruleManager == null) ruleManager = FindObjectOfType<RuleManager>();
            if (gameManager == null) gameManager = FindObjectOfType<GameManager>();
            if (cardDropZone == null) cardDropZone = FindObjectOfType<CardDrop>();
        }

        public void TakeTurn()
        {
            Debug.Log("=== AI Turn Starting ===");
            StartCoroutine(ExecuteTurn());
        }

        private IEnumerator ExecuteTurn()
        {
            // Simulate thinking time
            yield return new WaitForSeconds(thinkingDelay);

            // Keep playing cards while we can and have good moves
            while (state.CanPlayMoreCards() && state.opponentCards.Count > 0)
            {
                List<CardMove> possibleMoves = EvaluateAllMoves();

                if (possibleMoves.Count == 0)
                {
                    Debug.Log("AI: No valid moves available");
                    break;
                }

                // Choose best move
                CardMove bestMove = ChooseBestMove(possibleMoves);

                if (bestMove.score < GetMinimumAcceptableScore())
                {
                    Debug.Log($"AI: Best move score ({bestMove.score:F2}) below threshold. Ending turn.");
                    break;
                }

                // Execute the move
                yield return StartCoroutine(PlayCard(bestMove.card));

                yield return new WaitForSeconds(cardPlayDelay);
            }

            Debug.Log("AI: Ending turn");
            yield return new WaitForSeconds(0.5f);
            gameManager.EndTurn();
        }

        private List<CardMove> EvaluateAllMoves()
        {
            List<CardMove> moves = new List<CardMove>();
            var setupRule = ruleManager.GetSetupRule();

            foreach (var card in state.opponentCards)
            {
                float score = EvaluateCard(card, setupRule);
                moves.Add(new CardMove { card = card, score = score });
            }

            return moves.OrderByDescending(m => m.score).ToList();
        }

        private float EvaluateCard(Sprite card, ISetupRule setupRule)
        {
            float score = 0f;

            if (setupRule is BattleModeSetup battleMode)
            {
                score = EvaluateBattleModeCard(card, battleMode);
            }
            else if (setupRule is PointRaceModeSetup pointRace)
            {
                score = EvaluatePointRaceCard(card, pointRace);
            }

            // Apply personality modifiers
            score = ApplyPersonalityModifier(score, card, setupRule);

            return score;
        }

        private float EvaluateBattleModeCard(Sprite card, BattleModeSetup battleMode)
        {
            float score = 0f;

            // Check if it's an Ace (healing card)
            if (battleMode.IsAce(card))
            {
                // Value healing more when low on health
                float healthPercent = (float)state.opponentLife / 20f;
                if (healthPercent < 0.3f)
                {
                    score = 15f; // High priority when low health
                }
                else if (healthPercent < 0.6f)
                {
                    score = 8f; // Medium priority
                }
                else
                {
                    score = 3f; // Low priority when healthy
                }

                return score;
            }

            // Check if it's a blank card (dangerous!)
            if (battleMode.IsBlankCard(card))
            {
                // Blank cards are risky - only play if we have no other cards this turn
                return state.actionsThisTurn == 0 ? -10f : -20f;
            }

            // Calculate damage value
            int damage = battleMode.CalculateCardDamage(card);
            score = damage * 2f; // Base score is damage * 2

            // Bonus for lethal damage
            if (state.playerLife - damage <= 0)
            {
                score += 20f; // Go for the kill!
            }

            // Bonus for high-value cards
            if (damage >= 10)
            {
                score += 5f;
            }

            return score;
        }

        private float EvaluatePointRaceCard(Sprite card, PointRaceModeSetup pointRace)
        {
            // In Point Race, we need to evaluate card combos
            // For now, simulate points by adding card to a temp list
            List<Sprite> tempCards = new List<Sprite>(state.opponentPlayedCards) { card };
            int pointsEarned = pointRace.CalculatePoints(tempCards);

            float score = pointsEarned * 3f;

            // Bonus if this gets us close to winning
            if (state.opponentPoints + pointsEarned >= state.pointsToWin * 0.8f)
            {
                score += 10f;
            }

            // Bonus for cards that complete sets or patterns
            // This is game-specific - adjust based on your rules
            if (pointsEarned > 5)
            {
                score += 5f;
            }

            return score;
        }

        private float ApplyPersonalityModifier(float baseScore, Sprite card, ISetupRule setupRule)
        {
            switch (personality)
            {
                case AIPersonality.Aggressive:
                    // Aggressive AI values damage/points more
                    if (setupRule is BattleModeSetup battleMode)
                    {
                        int damage = battleMode.CalculateCardDamage(card);
                        baseScore += damage * 0.5f;
                    }

                    break;

                case AIPersonality.Defensive:
                    // Defensive AI values survival
                    if (setupRule is BattleModeSetup defensivebattleMode)
                    {
                        if (defensivebattleMode.IsAce(card))
                        {
                            baseScore += 5f; // Extra value on healing
                        }

                        float healthPercent = (float)state.opponentLife / 20f;
                        if (healthPercent < 0.5f)
                        {
                            baseScore *= 1.3f; // Play more conservatively when hurt
                        }
                    }

                    break;

                case AIPersonality.Balanced:
                    // No modifier - use base scores
                    break;
            }

            return baseScore;
        }

        private CardMove ChooseBestMove(List<CardMove> moves)
        {
            // Add some randomness so AI isn't perfectly predictable
            float randomFactor = Random.Range(0.8f, 1.2f);

            // Top moves with randomness
            CardMove bestMove = moves[0];
            bestMove.score *= randomFactor;

            // Occasionally choose second-best move for variety
            if (moves.Count > 1 && Random.value < 0.15f)
            {
                return moves[1];
            }

            return bestMove;
        }

        private float GetMinimumAcceptableScore()
        {
            // Determine if we should stop playing cards
            // Based on game state and personality

            if (state.maxActionsPerTurn == 1)
            {
                return 0f; // If we can only play one card, always play something
            }

            float threshold = 5f;

            if (personality == AIPersonality.Aggressive)
            {
                threshold = 3f; // More willing to play cards
            }
            else if (personality == AIPersonality.Defensive)
            {
                threshold = 7f; // More conservative
            }

            return threshold;
        }

        private IEnumerator PlayCard(Sprite card)
        {
            Debug.Log($"AI: Playing card {card.name}");

            // Remove from AI hand
            state.opponentCards.Remove(card);

            // Add to played cards
            state.opponentPlayedCards.Add(card);
            state.actionsThisTurn++;

            // You may want to add visual feedback here
            // For example, instantiate a card visual that moves to the play area

            yield return null;
        }

        private struct CardMove
        {
            public Sprite card;
            public float score;
        }
    }
}