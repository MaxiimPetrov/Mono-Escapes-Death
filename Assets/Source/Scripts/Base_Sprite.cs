using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Base_Sprite : MonoBehaviour
{
    private SpriteRenderer sprite;
    private GameObject middle_point;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        middle_point = GameObject.Find("MiddlePoint");
    }
    
    void Update()
    {
        if(!PauseMenu.game_paused)
        {
            Vector3 mouse_position = Input.mousePosition;
            mouse_position = Camera.main.ScreenToWorldPoint(mouse_position);

            if (middle_point.transform.position.x > mouse_position.x)
            {
                sprite.flipY = true;
            }
            else
            {
                sprite.flipY = false;
            }
        }
    }
}
