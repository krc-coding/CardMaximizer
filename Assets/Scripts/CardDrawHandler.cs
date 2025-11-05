using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class CardDrawHandler : MonoBehaviour, IPointerClickHandler
{
    public State state;
    private readonly List<Sprite> _allCards = new List<Sprite>();
    private GameManager gameManager;

    public void Start()
    {
        gameManager = FindObjectOfType<GameManager>();

        // Jokers (blank cards)
        _allCards.Add(state.clubs[0]);
        _allCards.Add(state.hearts[0]);
        _allCards.Add(state.spades[0]);
        _allCards.Add(state.diamonds[0]);

        // Clubs
        for (int i = 1; i < state.clubs.Length; i++)
        {
            _allCards.Add(state.clubs[i]);
        }

        // Hearts
        for (int i = 1; i < state.hearts.Length; i++)
        {
            _allCards.Add(state.hearts[i]);
        }

        // Spades
        for (int i = 1; i < state.spades.Length; i++)
        {
            _allCards.Add(state.spades[i]);
        }

        // Diamonds
        for (int i = 1; i < state.diamonds.Length; i++)
        {
            _allCards.Add(state.diamonds[i]);
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!gameManager.CanPlayCard()) return;
        if (GetRemainingCards() == 0)
        {
            Debug.Log("Deck is empty!");
            return;
        }

        if (state.playerCards.Count >= state.maxHandSize)
        {
            Debug.Log("Hand is full!");
            return;
        }
        
        DrawCard(true);
    }

    public void DrawCard(bool forPlayer = true)
    {
        List<Sprite> targetHand = forPlayer ? state.playerCards : state.opponentCards;

        if (targetHand.Count >= state.maxHandSize)
        {
            Debug.Log($"{(forPlayer ? "Player" : "AI")} hand is full!");
            return;
        }

        if (GetRemainingCards() == 0)
        {
            Debug.Log("Deck is empty!");
            return;
        }

        // Draw random card from deck
        int randomIndex = Random.Range(0, _allCards.Count);
        Sprite drawnCard = _allCards[randomIndex];
        _allCards.RemoveAt(randomIndex);
        targetHand.Add(drawnCard);
        state.actionsThisTurn++;

        Debug.Log($"{(forPlayer ? "Player" : "AI")} drew: {drawnCard.name}");
    }

    public int GetRemainingCards()
    {
        return _allCards.Count;
    }
}