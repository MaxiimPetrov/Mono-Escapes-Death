using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WinScreen : MonoBehaviour
{
    [SerializeField] private Texture2D cursor_texture;
    private Vector2 cursor_hotspot;
    private GameObject return_button_object;
    private Image return_button_image;
    private Button return_button;
    private TextMeshProUGUI return_button_text;
    private float timer = 8f;

    void Start()
    {
        cursor_hotspot = new Vector2(cursor_texture.width / 2, cursor_texture.height / 2);
        Cursor.SetCursor(cursor_texture, cursor_hotspot, CursorMode.Auto);
        return_button_object = GameObject.Find("ReturnButton");
        return_button_image = return_button_object.GetComponent<Image>();
        return_button = return_button_object.GetComponent<Button>();
        return_button_text = return_button_object.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            if(timer < 0)
            {
                return_button_image.enabled = true;
                return_button.enabled = true;
                return_button_text.enabled = true;
            }
        }
    }
}
