using System.Collections.Generic;
using UnityEngine;

public class State : MonoBehaviour
{
    // Blank cards replace joker card.
    public int drawPileCount = 56;
    public Sprite[] pileSprites;
    public Sprite[] clubs;
    public Sprite[] hearts;
    public Sprite[] spades;
    public Sprite[] diamonds;
    public int maxHandSize = 5;
    public List<Sprite> playerCards;
    public List<Sprite> opponentCards;
    public List<Sprite> discardPile;
    public List<Sprite> playedCards;

    [Header("Game State - Modified by Rules")]
    public int startingHandSize = 4;
    public int playerLife = 20;
    public int opponentLife = 20;
    public int playerPoints = 0;
    public int opponentPoints = 0;
    public int cardsDrawnPerTurn = 1;
    public float pointModifier = 1f;
    public int maxCardsPlayablePerTurn = 1;
    public float turnTimeLimit = 0f; // 0 = no limit
    public int cardsPlayedThisTurn = 0;
    
    public enum WinConditionType { LastManStanding, FirstToPoints }
    [Header("Win Conditions")]
    public WinConditionType winConditionType = WinConditionType.LastManStanding;
    public int pointsToWin = 50;

    public void DiscardCard(Sprite cardSprite)
    {
        Debug.Log("Discarding card: " + cardSprite.name);
        
        // Find and remove the card from playerCards
        for (int i = 0; i < playerCards.Count; i++)
        {
            if (playerCards[i].name == cardSprite.name)
            {
                Debug.Log("Found card in hand");
                playerCards.RemoveAt(i);
                discardPile.Add(cardSprite);
                Debug.Log("Cards left in hand: " + playerCards.Count);
                return;
            }
        }
        
        Debug.LogWarning("Card not found in player's hand: " + cardSprite.name);
    }
    
    public void PlayCard(Sprite cardSprite)
    {
        Debug.Log("Playing card: " + cardSprite.name);
        
        // Find and remove the card from playerCards
        for (int i = 0; i < playerCards.Count; i++)
        {
            if (playerCards[i].name == cardSprite.name)
            {
                Debug.Log("Found card in hand");
                playerCards.RemoveAt(i);
                playedCards.Add(cardSprite);
                cardsPlayedThisTurn++;
                Debug.Log("Cards left in hand: " + playerCards.Count);
                return;
            }
        }
        
        Debug.LogWarning("Card not found in player's hand: " + cardSprite.name);
    }
    
    public bool CanPlayMoreCards()
    {
        return cardsPlayedThisTurn < maxCardsPlayablePerTurn;
    }
    
    public void ResetTurn()
    {
        cardsPlayedThisTurn = 0;
    }
    
    public void ResetGameState()
    {
        playerPoints = 0;
        opponentPoints = 0;
        cardsPlayedThisTurn = 0;
        playerCards.Clear();
        opponentCards.Clear();
        playedCards.Clear();
        discardPile.Clear();
    }
}
