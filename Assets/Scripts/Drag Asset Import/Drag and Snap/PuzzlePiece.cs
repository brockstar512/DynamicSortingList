using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{

    private bool _isDragging,_placed = false;
    private Vector2 _offset;
    private Vector2 _originalPos;
    private PuzzleSlot _slot;
    [SerializeField] SpriteRenderer _renderer;

    [SerializeField] AudioClip _pickUpClip;
    [SerializeField] AudioClip _droppedClip;
    private AudioSource _source;


    void Awake()
    {
        _originalPos = transform.position;
        _source = this.transform.gameObject.AddComponent<AudioSource>();
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
        //if this is close to the slot that it is targeting
        if (Vector2.Distance(transform.position, _slot.transform.position) < 3)
        {
            transform.position = _slot.transform.position;
            _slot.Placed();
            _placed = true;
            return;
        }

        transform.position = _originalPos;
        _isDragging = false;

        _source?.PlayOneShot(_droppedClip);

    }

    void OnMouseDown()
    {
        _isDragging = true;
        _offset = GetMousePos() - (Vector2)transform.position;
        _source?.PlayOneShot(_pickUpClip);
    }

    public void Init(PuzzleSlot slot)
    {
        _renderer.sprite = slot.rendr.sprite;
        _slot = slot;
    }
}
