using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSpawner : MonoBehaviour
{
    public base_projectile projectile;
    public float shot_cooldown;
    private float shot_timer;
    private Base_Enemy enemyScript;
    private BulletSpawner bullet_spawner;

    void Start()
    {
        shot_timer = shot_cooldown;
        enemyScript = GetComponentInParent<Base_Enemy>();
        bullet_spawner = GetComponent<BulletSpawner>();
    }

    void Update()
    {
        if(enemyScript.enemy_spawned)
        {
            if (shot_timer <= 0)
            {
                bullet_spawner.pool.Get();
                if (GameManager.player_level == 1)
                {
                    shot_timer = shot_cooldown;
                }
                else if (GameManager.player_level == 2)
                {
                    shot_timer = shot_cooldown / 1.2f;
                }
                else if (GameManager.player_level == 3)
                {
                    shot_timer = shot_cooldown / 1.3f;
                }
                else if (GameManager.player_level == 4)
                {
                     shot_timer = shot_cooldown / 1.4f;
                }
                else
                {
                    shot_timer = shot_cooldown / 1.5f;
                }
            }
            else
            {
                shot_timer -= Time.deltaTime;
            }
        }
        
    }
}
