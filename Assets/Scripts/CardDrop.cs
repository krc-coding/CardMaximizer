using UnityEngine;
using UnityEngine.EventSystems;

public class CardDrop : MonoBehaviour, IDropHandler
{
    public State state;
    
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Drop");
        if (eventData.pointerDrag != null)
        {
            GameObject card = eventData.pointerDrag.gameObject;
            Sprite sprite = card.GetComponent<SpriteRenderer>().sprite;
            
            state.DiscardCard(sprite);
        }
    }
}
