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
        float xPosition = (index - (state.playerCards.Count - 1) / 2f) * cardSpacing;
        cardObject.transform.localPosition = new Vector3(xPosition, -0.075f, 0);
        cardObject.transform.localScale = new Vector3(cardXScale, cardYScale);

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