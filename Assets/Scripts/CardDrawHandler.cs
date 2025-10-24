using UnityEngine;
using UnityEngine.EventSystems;

public class CardDrawHandler : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Clicked: " + name);
    }
}