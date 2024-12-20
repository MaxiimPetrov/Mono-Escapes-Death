using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Minimap : MonoBehaviour
{
    private RawImage minimap_background;
    private RawImage minimap_camera_display;
    private Image minimap_border;
    private bool fade_out_happened = false;
    private bool fade_in_happened = false;

    void Start()
    {
        minimap_background = transform.Find("MinimapBackground")?.gameObject.GetComponent<RawImage>();
        minimap_camera_display = transform.Find("MinimapCameraDisplay")?.gameObject.GetComponent<RawImage>();
        minimap_border = transform.Find("MinimapBorder")?.gameObject.GetComponent<Image>();
    }

    void Update()
    {
        if(GameManager.wave_active)
        {
            if(!fade_out_happened)
            {
                Color fade_out_background = minimap_background.color;
                fade_out_background.a = 0.15f;
                minimap_background.color = fade_out_background;

                Color fade_out_display = minimap_camera_display.color;
                fade_out_display.a = 0.15f;
                minimap_camera_display.color = fade_out_display;

                Color fade_out_border = minimap_border.color;
                fade_out_border.a = 0.15f;
                minimap_border.color = fade_out_border;

                fade_out_happened = true;
                fade_in_happened = false;
            }
        }
        else
        {
            if (!fade_in_happened)
            {
                Color fade_out_background = minimap_background.color;
                fade_out_background.a = 0.8f;
                minimap_background.color = fade_out_background;

                Color fade_out_display = minimap_camera_display.color;
                fade_out_display.a = 1f;
                minimap_camera_display.color = fade_out_display;

                Color fade_out_border = minimap_border.color;
                fade_out_border.a = 1f;
                minimap_border.color = fade_out_border;

                fade_in_happened = true;
                fade_out_happened = false;
            } 
        }
    }
}
