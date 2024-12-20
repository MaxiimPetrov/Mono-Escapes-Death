using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCursor : MonoBehaviour
{
    [SerializeField] private Texture2D cursor_texture;
    private Vector2 cursor_hotspot;

    void Start()
    {
        cursor_hotspot = new Vector2(cursor_texture.width / 2, cursor_texture.height / 2);
        Cursor.SetCursor(cursor_texture, cursor_hotspot, CursorMode.Auto);
    }
}
