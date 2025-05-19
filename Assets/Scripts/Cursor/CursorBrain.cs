using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorBrain : MonoBehaviour
{
    [SerializeField] Texture2D DefaultCursor;
    [SerializeField] Texture2D GrabCursor;
    [SerializeField] Texture2D ReachCursor;
    Vector2 CursorHotSpot;
    [SerializeField] Color CursorColor;
    // Start is called before the first frame update
    void Start()
    {
        CursorHotSpot = new Vector2(ReachCursor.width/2, ReachCursor.height/2);
        Cursor.SetCursor(ReachCursor, CursorHotSpot, CursorMode.Auto);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
