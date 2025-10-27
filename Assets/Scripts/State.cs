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
    public List<Sprite> playerCards;
    public int maxHandSize = 5;
}
