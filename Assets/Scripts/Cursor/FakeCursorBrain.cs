using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FakeCursorBrain : MonoBehaviour
{
    Camera mainCamera;
    [SerializeField] SpriteRenderer fakeCursor;
    [SerializeField] Sprite defaultCursor;
    [SerializeField] Sprite grabCursor;
    [SerializeField] Sprite reachCursor;
    DifferentCursorStates defaultState;
    DifferentCursorStates reachState;
    DifferentCursorStates grabState;
    DifferentCursorStates currentState { get; set; }

    void Start()
    {
        Init();
    }

    void Init()
    {
        if(!fakeCursor)
        {
            fakeCursor = GetComponent<SpriteRenderer>();
        }
        EventMediator.Instance.Subscribe<CursorInputs>(UpdateCursor);
        mainCamera = Camera.main;
        defaultState = new DifferentCursorStates(defaultCursor, CursorHotspot.TopLeftCorner);
        reachState = new DifferentCursorStates(reachCursor, CursorHotspot.Center);
        grabState = new DifferentCursorStates(grabCursor, CursorHotspot.Center);
        fakeCursor.sortingOrder = 10;
        UpdateCursor(defaultState);
    }

    void Update()
    {
        //MoveByArrows();
        MoveByMouse();
    }

    private void OnDestroy()
    {
        EventMediator.Instance.Unsubscribe<CursorInputs>(UpdateCursor);
    }

    #region Update CursorStates
    void UpdateCursor(DifferentCursorStates updateToState)
    {
        fakeCursor.sprite = updateToState.sprite;
        currentState = updateToState;
    }

    public void UpdateCursor(CursorInputs updateToState)
    {
        switch (updateToState)
        {
            case CursorInputs.MouseUp:
                UpdateCursor(defaultState);
                break;
            case CursorInputs.MouseDown:
                UpdateCursor(grabState);
                break;
            case CursorInputs.UIHover:
                UpdateCursor(defaultState);
                break;



        }
    }

    #endregion

    #region Movement
    void MoveByMouse()
    {
        // Get mouse position in screen space and convert to world space
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        // Keep Z value unchanged (optional if working in 2D)
        mousePosition.z = transform.position.z;

        // Move sprite to mouse position
        transform.position = mousePosition - currentState.Offset;
    }

    void MoveByArrows()
    {
        // Get input from arrow keys (or WASD)
        float moveX = Input.GetAxisRaw("Horizontal"); // Left (-1), Right (+1)
        float moveY = Input.GetAxisRaw("Vertical");   // Down (-1), Up (+1)
        float speed = 5f;
        Vector3 movement = new Vector3(moveX, moveY, 0) * speed * Time.deltaTime;
        transform.position += movement;

        // If you want to keep the hotspot offset applied relative to position:
        // transform.position = transform.position - currentState.Offset;
        // but usually the offset is fixed, so you might apply it once on initial placement instead
    }
    #endregion
}

/*
 it works for mouse... not very clean though. i can see problems arise later. Decouple it tp work with different inputs
 maybe look at this
    https://www.youtube.com/watch?v=j2XyzSAD4VU
 */