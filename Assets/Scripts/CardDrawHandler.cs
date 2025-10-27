using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Random = UnityEngine.Random;

public class CardDrawHandler : MonoBehaviour, IPointerClickHandler
{
    public State state;
    private readonly List<Sprite> _allCards =  new List<Sprite>();

    public void Start()
    {
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
        if (_allCards.Count == 0) return;
        if (state.playerCards.Count >= state.maxHandSize) return;
        
        Sprite card = DrawCard();
        state.playerCards.Add(card);
    }

    public void TriggerCardDraw()
    {
         
    }

    private Sprite DrawCard()
    {
        int cardIndex = Random.Range(0, _allCards.Count);
        
        try
        {
            Sprite card = _allCards[cardIndex];
            state.playerCards.Add(card);
            _allCards.RemoveAt(cardIndex);
            return card;
        }
        catch (ArgumentOutOfRangeException e)
        {
            Debug.Log(e.Message);
            Debug.Log(cardIndex);
        }
        return null;
    }
}