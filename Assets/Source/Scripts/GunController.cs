using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GunController : MonoBehaviour
{
    public Base_Gun equipped_gun;
    private GameObject gun_spawn_location;
    private SpriteRenderer gun_sprite;

    private void Start()
    {
        gun_spawn_location = GameObject.Find("GunSpawnLocation");

        gun_sprite = equipped_gun.GetComponentInChildren<SpriteRenderer>();

        Instantiate(equipped_gun, gun_spawn_location.transform.position, gun_spawn_location.transform.rotation, gun_spawn_location.transform);
    }

    void Update()
    {
        if(!PauseMenu.game_paused)
        {
            Vector3 mouse_position = Input.mousePosition;
            mouse_position = Camera.main.ScreenToWorldPoint(mouse_position);

            Vector2 direction = new Vector2(mouse_position.x - transform.position.x, mouse_position.y - transform.position.y);
            transform.up = direction;
        }
    }
}
