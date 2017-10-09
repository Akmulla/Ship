using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorChange : MonoBehaviour
{
    public Texture2D cursorDefault;
    public Texture2D cursorPressed;
    public Vector2 offset;
    // Use this for initialization
    void Start()
    {
        Cursor.SetCursor(cursorDefault, offset, CursorMode.Auto);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Cursor.SetCursor(cursorPressed, offset, CursorMode.Auto);
        }
        else
        {
            Cursor.SetCursor(cursorDefault, offset, CursorMode.Auto);
        }
    }

}