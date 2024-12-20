using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class TrackerSpawner : MonoBehaviour
{
    public base_projectile projectile;
    private Vector2 direction;
    private float shot_cooldown_timer;
    public float cooldown_time;
    private Base_Enemy enemyScript;
    private bool first_shot = false;
    private BulletSpawner bullet_spawner;

    void Start()
    {
        shot_cooldown_timer = cooldown_time;
        enemyScript = GetComponentInParent<Base_Enemy>();
        bullet_spawner = GetComponent<BulletSpawner>();
    }

    void Update()
    {
        if(enemyScript.enemy_spawned)
        {
            if(!first_shot)
            {
                if (bullet_spawner != null)
                {
                    bullet_spawner.pool.Get();
                    first_shot = true;
                }
            }

            if (shot_cooldown_timer <= 0)
            {
                if (bullet_spawner != null)
                {
                    bullet_spawner.pool.Get();

                    if (GameManager.player_level == 1)
                    {
                        shot_cooldown_timer = cooldown_time;
                    }
                    else if (GameManager.player_level == 2)
                    {
                        shot_cooldown_timer = cooldown_time / 1.1f;
                    }
                    else if (GameManager.player_level == 3)
                    {
                        shot_cooldown_timer = cooldown_time / 1.2f;
                    }
                    else if (GameManager.player_level == 4)
                    {
                        shot_cooldown_timer = cooldown_time / 1.3f;
                    }
                    else
                    {
                        shot_cooldown_timer = cooldown_time / 1.4f;
                    }
                }
            }
            else
            {
                shot_cooldown_timer -= Time.deltaTime;
            }
        }
    }
}
