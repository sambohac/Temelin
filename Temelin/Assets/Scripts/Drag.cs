using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Drag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Vector3 offset;
    List<Transform> collidings;

    private void Start()
    {
        collidings = new List<Transform>();
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("TRIGGER!!");
        collidings.Add(collision.transform);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collidings.Remove(collision.transform);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        Debug.Log(collidings.Count);
    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = GetMouseWorldPosition(eventData) + offset;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        offset = transform.position - GetMouseWorldPosition(eventData);
    }

    private Vector3 GetMouseWorldPosition(PointerEventData eventData)
    {
        Vector3 mouseScreenPos = eventData.position;
        // Maintain the same z-distance from camera
        mouseScreenPos.z = Camera.main.WorldToScreenPoint(transform.position).z;
        return Camera.main.ScreenToWorldPoint(mouseScreenPos);
    }
}