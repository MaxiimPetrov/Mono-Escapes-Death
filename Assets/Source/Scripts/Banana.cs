using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class banana : MonoBehaviour
{
    private float timer = 1.5f;
    private bool activate_timer = false;
    private CircleCollider2D body_hitbox;
    PlayerController player_script;

    private void Awake()
    {
        CircleCollider2D[] colliders = GetComponents<CircleCollider2D>();
        body_hitbox = colliders[0];
        body_hitbox.enabled = false;
    }

    private void Update()
    {
        if(activate_timer)
        {
            timer -= Time.deltaTime;
            if(timer <= 0)
            {
                player_script.health = 10;
                Health.SetHealthUI(player_script.health);
                if(!GameManager.boss_spawned)
                {
                    GameManager.wave_active = false;
                }
                Destroy(this.gameObject);
                
            }
        }

        if(GameManager.boss_dead)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        activate_timer = true;
        player_script = collision.GetComponent<PlayerController>();
    }

    public void ScaleDown()
    {
        this.transform.localScale = Vector3.one;
    }

    public void ScaleUp()
    {
        this.transform.localScale = new Vector3(2, 2, 1);
        body_hitbox.enabled = true;
    }
}
