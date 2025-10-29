using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class CardDragAndDrop : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private Vector3 _startPosition;
    
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mousePos = Mouse.current.position.ReadValue();
        mousePos.z = 10;
        
        // Convert screen position to world position
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        worldPosition.z = 0;
        transform.position = worldPosition;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _startPosition = transform.position;
        GetComponent<BoxCollider2D>().enabled = false;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = _startPosition;
        GetComponent<BoxCollider2D>().enabled = true;
    }
}
