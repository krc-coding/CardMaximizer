using UnityEngine;
using UnityEngine.EventSystems;

public class CardDrop : MonoBehaviour, IDropHandler
{
    public State state;
    private GameManager gameManager;
    public bool isDiscardPile = false;

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void OnDrop(PointerEventData eventData)
    {
        // Get the dragged card
        GameObject draggedCard = eventData.pointerDrag;
        if (draggedCard == null) return;

        // Check if we can play more cards
        if (gameManager != null && !gameManager.CanPlayCard())
        {
            Debug.Log($"Cannot play more cards! Max {state.maxActionsPerTurn} per turn.");
            return;
        }

        // Get the card sprite
        SpriteRenderer spriteRenderer = draggedCard.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null && spriteRenderer.sprite != null)
        {
            Sprite cardSprite = spriteRenderer.sprite;

            // Notify game manager
            if (gameManager != null)
            {
                if (isDiscardPile)
                {
                    state.DiscardCard(cardSprite);
                }
                else
                {
                    gameManager.OnCardPlayed(cardSprite);
                }
            }
            else
            {
                // Fallback if no game manager
                state.PlayCard(cardSprite);
            }

            Debug.Log($"Dropped card: {cardSprite.name} on {gameObject.name}");
        }
    }
}