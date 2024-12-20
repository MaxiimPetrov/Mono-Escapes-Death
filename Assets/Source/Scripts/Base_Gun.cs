using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_Gun : MonoBehaviour
{
    public float fire_rate;
    public base_projectile projectile;
    public GameObject spawn_location;
    private SpriteRenderer gun_sprite;
    private float shoot_timer = 0;
    private BulletSpawner bullet_spawner;

    private void Start()
    {
        spawn_location = GameObject.Find("SpawnLocation");
        gun_sprite = GetComponent<SpriteRenderer>();
        bullet_spawner = GetComponent<BulletSpawner>();
    }

    private void Update()
    {
        if (Input.GetMouseButton(0) && shoot_timer <= 0f && !PauseMenu.game_paused)
        {
            shoot(projectile);
            if(GameManager.player_level == 1)
            {
                shoot_timer = fire_rate;
            }
            else if(GameManager.player_level == 2)
            {
                shoot_timer = fire_rate / 2;
            }
            else if(GameManager.player_level == 3)
            {
                shoot_timer = fire_rate / 4;
            }
            else if (GameManager.player_level == 4)
            {
                shoot_timer = fire_rate / 8;
            }
            else
            {
                shoot_timer = fire_rate / 16;
            }
        }
        else
        {
            shoot_timer -= Time.deltaTime;
        }
    }

    public void shoot(base_projectile projectile)
    {
        bullet_spawner.pool.Get();
    }
}
