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
}
