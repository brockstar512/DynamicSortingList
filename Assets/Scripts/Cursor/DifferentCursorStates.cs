using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct DifferentCursorStates
{
    public Sprite sprite;
    public Vector3 Offset;

    public DifferentCursorStates(Sprite sprite, CursorHotspot cursorHotspot)
    {
        this.sprite = sprite;

        // Calculate offset from sprite center to hotspot in world units
        Vector3 min = sprite.bounds.min;
        Vector3 max = sprite.bounds.max;

        switch (cursorHotspot)
        {
            case CursorHotspot.TopLeftCorner:
                Offset = new Vector3(min.x, max.y, 0);
                break;
            case CursorHotspot.TopRightCorner:
                Offset = new Vector3(max.x, max.y, 0);
                break;
            case CursorHotspot.BottomRightCorner:
                Offset = new Vector3(max.x, min.y, 0);
                break;
            case CursorHotspot.BottomLeftCorner:
                Offset = new Vector3(min.x, min.y, 0);
                break;
            case CursorHotspot.Center:
                Offset = Vector3.zero;
                break;
            default:
                Offset = Vector3.zero;
                break;
        }
    }
}

public enum CursorHotspot
{
    TopLeftCorner,
    TopRightCorner,
    BottomRightCorner,
    BottomLeftCorner,
    Center
}
public enum CursorInputs
{
    MouseUp,
    MouseDown,
    UIHover

}