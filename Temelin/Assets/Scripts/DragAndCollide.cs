using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragAndCollide : MonoBehaviour
{
    internal Vector3 startPos;
    private Vector3 offset;
    List<Transform> collidings;

    internal CharacterController originatingCharacter;

    private void Start()
    {
        collidings = new List<Transform>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "event")
            collidings.Add(collision.transform);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.tag == "event")
            collidings.Remove(collision.transform);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log(collidings.Count);

        if (collidings.Count == 0)
        {
            originatingCharacter.MoveRepresentation(startPos);
            return;
        }

        for (int i = 0; i < collidings.Count; i++)
        {
            if (collidings[i].transform.tag == "event")
            {
                EventController ec = collidings[i].transform.GetComponent<EventController>();
                PowerplantEvent collidedEvent = ec.thisEvent;
                originatingCharacter.UseCharacter(collidedEvent, ec);
                originatingCharacter.MoveRepresentation(startPos);
            }
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        // transform.position = GetMouseWorldPosition(eventData) + offset;
        originatingCharacter.MoveRepresentation(GetMouseWorldPosition(eventData));
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        originatingCharacter.MoveRepresentation(GetMouseWorldPosition(eventData));
        // offset = transform.position - GetMouseWorldPosition(eventData);
    }

    private Vector3 GetMouseWorldPosition(PointerEventData eventData)
    {
        Vector3 mouseScreenPos = eventData.position;
        // Maintain the same z-distance from camera
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }
}