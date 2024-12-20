using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Base_Enemy enemy_to_spawn;
    private float spawn_timer;
    public float spawn_timer_length;

    private void Start()
    {
        Instantiate(enemy_to_spawn, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);
        spawn_timer = spawn_timer_length;
    }

    private void Update()
    {
        if (!GameManager.enemies_dead)
        {
            if (spawn_timer > 0)
            {
                spawn_timer -= Time.deltaTime;
            }
            else
            {
                Instantiate(enemy_to_spawn, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);
                spawn_timer = spawn_timer_length;
            }
        }
        
    }
}
