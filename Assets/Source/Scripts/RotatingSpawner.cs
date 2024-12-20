using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingSpawner : MonoBehaviour
{
    public base_projectile projectile;
    private float shot_cooldown_timer;
    public float cooldown_time;
    private GameObject[] spawners;
    private int spawner_count;
    public float rotation_speed;
    private Base_Enemy enemyScript;
    private BulletSpawner bullet_spawner;
    [SerializeField] public bool active = true;

    void Start()
    {
        spawner_count = transform.childCount;
        spawners = new GameObject[spawner_count];
        enemyScript = GetComponentInParent<Base_Enemy>();
        bullet_spawner = GetComponent<BulletSpawner>();
        for (int i = 0; i < spawner_count; i++)
        {
            spawners[i] = transform.GetChild(i).gameObject;  
        }
    }
    void Update()
    {
        if(enemyScript.enemy_spawned && active)
        {
            if (shot_cooldown_timer <= 0)
            {
                for (int i = 0; i < spawner_count; i++)
                {
                    bullet_spawner.rotate_spawner_position = spawners[i].transform.position;
                    bullet_spawner.rotate_spawner_rotation = spawners[i].transform.rotation.eulerAngles;
                    bullet_spawner.pool.Get();

                    if (GameManager.player_level == 1)
                    {
                        shot_cooldown_timer = cooldown_time;
                    }
                    else if (GameManager.player_level == 2)
                    {
                        shot_cooldown_timer = cooldown_time / 1.2f;
                    }
                    else if (GameManager.player_level == 3)
                    {
                        shot_cooldown_timer = cooldown_time / 1.25f;
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

    private void FixedUpdate()
    {
        if (enemyScript.enemy_spawned)
        {
            Quaternion rotation = Quaternion.Euler(0, 0, rotation_speed * Time.fixedDeltaTime);
            this.transform.rotation *= rotation;
        }
    }
}

