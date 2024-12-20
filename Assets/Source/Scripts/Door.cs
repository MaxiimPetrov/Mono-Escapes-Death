using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool is_door_top, is_door_bottom, is_door_left, is_door_right;
    public bool is_boss_door = false;
    private GameObject player_spawn_point;
    private GameObject door_left_object;
    private GameObject door_right_object;
    private SpriteRenderer door_left_sprite;
    private SpriteRenderer door_right_sprite;

    void Start()
    {
        player_spawn_point = transform.Find("PlayerSpawnPoint")?.gameObject;
        door_left_object = transform.Find("DoorClosedLeft")?.gameObject;
        door_left_sprite = door_left_object?.GetComponent<SpriteRenderer>();
        door_right_object = transform.Find("DoorClosedRight")?.gameObject;
        door_right_sprite = door_right_object?.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(is_boss_door)
        {
            if (GameManager.wave_active || !GameManager.purple_key_collected || !GameManager.red_key_collected || !GameManager.yellow_key_collected || !GameManager.green_key_collected)
            {
                door_left_sprite.enabled = true;
                door_right_sprite.enabled = true;
            }
            else
            {
                door_left_sprite.enabled = false;
                door_right_sprite.enabled = false;
            }
        }
        else
        {
            if (GameManager.wave_active)
            {
                door_left_sprite.enabled = true;
                door_right_sprite.enabled = true;
            }
            else
            {
                door_left_sprite.enabled = false;
                door_right_sprite.enabled = false;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (is_boss_door)
        {
            if (!GameManager.wave_active && GameManager.purple_key_collected && GameManager.red_key_collected && GameManager.yellow_key_collected && GameManager.green_key_collected)
            {
                GameManager.player.transform.position = player_spawn_point.transform.position;

                if (is_door_top)
                {
                    GameManager.player_grid_position += Vector2.up;
                    GameManager.UpdateCameraPosition();
                }
                else if (is_door_bottom)
                {
                    GameManager.player_grid_position += Vector2.down;
                    GameManager.UpdateCameraPosition();
                }
                else if (is_door_left)
                {
                    GameManager.player_grid_position += Vector2.left;
                    GameManager.UpdateCameraPosition();
                }
                else if (is_door_right)
                {
                    GameManager.player_grid_position += Vector2.right;
                    GameManager.UpdateCameraPosition();
                }
            }
            else if(!GameManager.wave_active)
            {
                KeyUI.ShowKeysText();
            }
        }
        else
        {
            if (!GameManager.wave_active)
            {
                GameManager.player.transform.position = player_spawn_point.transform.position;

                if (is_door_top)
                {
                    GameManager.player_grid_position += Vector2.up;
                    GameManager.UpdateCameraPosition();
                }
                else if (is_door_bottom)
                {
                    GameManager.player_grid_position += Vector2.down;
                    GameManager.UpdateCameraPosition();
                }
                else if (is_door_left)
                {
                    GameManager.player_grid_position += Vector2.left;
                    GameManager.UpdateCameraPosition();
                }
                else if (is_door_right)
                {
                    GameManager.player_grid_position += Vector2.right;
                    GameManager.UpdateCameraPosition();
                }
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(is_boss_door && !GameManager.wave_active)
        {
            KeyUI.HideKeysText();
        }
    }
}
