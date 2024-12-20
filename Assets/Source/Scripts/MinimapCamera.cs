using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapCamera : MonoBehaviour
{
    private Camera minimap_camera;
    private int current_size = 1;

    private void Start()
    {
        GameObject camera_object = GameObject.Find("Minimap Camera");
        minimap_camera = camera_object.GetComponent<Camera>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !GameManager.wave_active && !PauseMenu.game_paused)
        {
            if (current_size == 1)
            {
                minimap_camera.orthographicSize = 8;
                current_size++;
            }
            else if (current_size == 2)
            {
                minimap_camera.orthographicSize = 12;
                current_size++;
            }
            else if(current_size == 3)
            {
                minimap_camera.orthographicSize = 4;
                current_size = 1;
            }
        }
    }
}
