using System.Collections.Generic;
using UnityEngine;

public class PlayerHandHandler : MonoBehaviour
{
    public State state;
    public bool shouldShowCards = true;
    public Sprite cardBack;
    public GameObject cardPrefab;

    [Header("Card Display Settings")] [SerializeField]
    private float cardSpacing = 1.5f;

    [SerializeField] private float cardXScale = 1f;
    [SerializeField] private float cardYScale = 1f;

    [Header("Layering Settings")]
    [SerializeField] private int maxCardsBeforeLayering = 5;
    [SerializeField] private float minCardSpacing = 0.3f; // Minimum spacing when heavily layered
    [SerializeField] private float spacingCompressionDivisor = 10f;
    
    private List<GameObject> instantiatedCards = new List<GameObject>();

    void Start()
    {
        // Clear any existing children
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
    }

    void Update()
    {
        // Check if the hand needs to be updated
        if (state != null && HasHandChanged())
        {
            UpdateHand();
        }
    }

    private bool HasHandChanged()
    {
        // Check if number of cards has changed
        if (instantiatedCards.Count != state.playerCards.Count)
            return true;

        if (shouldShowCards == false)
            return false;

        // Check if any card is different
        for (int i = 0; i < state.playerCards.Count; i++)
        {
            if (i >= instantiatedCards.Count ||
                instantiatedCards[i] == null ||
                instantiatedCards[i].GetComponent<SpriteRenderer>().sprite != state.playerCards[i])
            {
                return true;
            }
        }

        return false;
    }

    private void UpdateHand()
    {
        // Clear existing cards
        ClearHand();

        // Create new card GameObjects for each sprite in the hand
        for (int i = 0; i < state.playerCards.Count; i++)
        {
            if (shouldShowCards) CreateCard(state.playerCards[i], i);
            else CreateCard(cardBack, i);
        }
    }

    private void CreateCard(Sprite cardSprite, int index)
    {
        // Create a new GameObject for the card
        GameObject cardObject = Instantiate(cardPrefab, transform);
        cardObject.name = "Card " + index;

        // Configure SpriteRenderer
        if (shouldShowCards)
        {
            cardObject.GetComponent<SpriteRenderer>().sprite = cardSprite;
        }
        else
        {
            cardObject.GetComponent<SpriteRenderer>().sprite = cardBack;
        }

        // Position the card
        //float xPosition = (index - (state.playerCards.Count - 1) / 2f) * cardSpacing;
        //cardObject.transform.localPosition = new Vector3(xPosition, -0.075f, 0);
        //cardObject.transform.localScale = new Vector3(cardXScale, cardYScale);
        
        // Calculate dynamic spacing based on number of cards
        float dynamicSpacing = CalculateCardSpacing(state.playerCards.Count);
        Debug.Log("Dynamic spacing: " + dynamicSpacing);
        // Position the card
        float xPosition = (index - (state.playerCards.Count - 1) / 2f) * dynamicSpacing;
        cardObject.transform.localPosition = new Vector3(xPosition, -0.075f, 0);
        cardObject.transform.localScale = new Vector3(cardXScale, cardYScale);

        // Set sorting order so cards layer correctly (left to right)
        SpriteRenderer spriteRenderer = cardObject.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.sortingOrder = index;
        }

        // Store reference
        instantiatedCards.Add(cardObject);
    }

    private void ClearHand()
    {
        // Destroy all existing card GameObjects
        foreach (GameObject card in instantiatedCards)
        {
            if (card != null)
            {
                Destroy(card);
            }
        }

        instantiatedCards.Clear();
    }
    
    private float CalculateCardSpacing(int cardCount)
    {
        // If 5 or fewer cards, use normal spacing
        if (cardCount <= maxCardsBeforeLayering)
        {
            return cardSpacing;
        }

        // Gradually reduce spacing as cards increase beyond 5
        // This creates a smooth transition to layered cards
        float excessCards = cardCount - maxCardsBeforeLayering;
        
        // Calculate interpolation factor (0 to 1)
        // Adjust the divisor to control how quickly cards compress
        float compressionFactor = Mathf.Clamp01(excessCards / spacingCompressionDivisor);
        
        // Lerp between normal spacing and minimum spacing
        return Mathf.Lerp(cardSpacing, minCardSpacing, compressionFactor);
    }

    /// <summary>
    /// Remove a card from the state by index. Call this when a card is played.
    /// </summary>
    public void RemoveCard(int index)
    {
        if (state != null && index >= 0 && index < state.playerCards.Count)
        {
            state.playerCards.RemoveAt(index);
            // Hand will auto-update in next Update() call
        }
    }

    /// <summary>
    /// Remove a specific card from the state. Call this when a card is played.
    /// </summary>
    public void RemoveCard(Sprite cardSprite)
    {
        if (state != null && state.playerCards.Contains(cardSprite))
        {
            state.playerCards.Remove(cardSprite);
            // Hand will auto-update in next Update() call
        }
    }

    /// <summary>
    /// Add a card to the state. Call this when drawing a card.
    /// </summary>
    public void AddCard(Sprite cardSprite)
    {
        if (state != null && cardSprite != null)
        {
            state.playerCards.Add(cardSprite);
            // Hand will auto-update in next Update() call
        }
    }
}