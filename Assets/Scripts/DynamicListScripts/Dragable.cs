using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

[RequireComponent(typeof(BoxCollider2D))]
public class Dragable : MonoBehaviour
{

    [SerializeField] Transform slotLocation;
    private bool _isDragging, _placed = false;
    private Vector2 _originalPos;
    private Vector2 _offset;
    public Action<Vector3> freeUpAvaiableSlot;

    public void Init(Vector3 slotLocation, Action<Vector3> onPlacedCallback)
    {
        _originalPos = slotLocation;
        freeUpAvaiableSlot = onPlacedCallback;
    }

    void Update()
    {
        if (_placed) return;

        if (!_isDragging) return;

        var mousePosition = GetMousePos();
        transform.position = mousePosition - _offset;

    }

    Vector2 GetMousePos()
    {
        return (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    void OnMouseUp()
    {
        EventMediator.Instance.Publish(CursorInputs.MouseUp);

        //Debug.Log("Mouse Up 1");

        //if this is close to the slot that it is targeting
        //if (Vector2.Distance(transform.position, _slot.transform.position) < 3)
        //{
        //    transform.position = _slot.transform.position;
        //    _slot.Placed();
        //    _placed = true;
        //    Debug.Log("Mouse Up 2");

        //    return;
        //}
        _isDragging = false;


        //if not correct position
        //transform.position = _originalPos;


        //else
        //Debug.Log("Mouse Up 3");
        freeUpAvaiableSlot?.Invoke(_originalPos);

        //todo this should not go here I am just doing this for testing 
        Destroy(this);
    }

    void OnMouseDown()
    {
        EventMediator.Instance.Publish(CursorInputs.MouseDown);
        _isDragging = true;
        _offset = GetMousePos() - (Vector2)transform.position;
    }

    private void OnDestroy()
    {
        freeUpAvaiableSlot = null;
    }

}
