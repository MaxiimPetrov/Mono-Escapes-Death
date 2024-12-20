using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Unity.VisualScripting;

public class Base_Enemy : MonoBehaviour
{
    public int health;
    public int collision_damage = 0;
    public GameObject delete_effect;
    protected SpriteRenderer sprite;
    protected Vector3 player_pos;
    public bool enemy_spawned = false;
    protected BoxCollider2D body_hitbox;
    protected BoxCollider2D body_hitbox_2;
    private bool died_once = false;
    protected SoundEffectPlayer sound_effect_player;

    protected virtual void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        BoxCollider2D[] colliders = GetComponents<BoxCollider2D>();
        body_hitbox_2 = colliders[0];
        body_hitbox = colliders[1];
        GameManager.num_enemies_active += 1;
        sound_effect_player = GameObject.Find("SoundEffectPlayer").GetComponent<SoundEffectPlayer>();

        if (GameManager.player_level == 1)
        {
            return;
        }
        else if (GameManager.player_level == 2)
        {
            health = (int)Math.Round(health * 1.75);
        }
        else if (GameManager.player_level == 3)
        {
            health = (int)Math.Round(health * 2.75);
        }
        else if (GameManager.player_level == 4)
        {
            health = (int)Math.Round(health * 3.75);
        }
        else
        {
            health = (int)Math.Round(health * 8.25);
        }
    }

    protected virtual void Update()
    {
        if(enemy_spawned)
        {
            player_pos = PlayerController.player_position;
            if (this.transform.position.x > player_pos.x)
            {
                sprite.flipX = true;
            }
            else
            {
                sprite.flipX = false;
            }
        }
        
    }
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(enemy_spawned)
        {
            base_projectile bullet = collision.GetComponent<base_projectile>();

            if (bullet != null)
            {
                health -= bullet.damage;
                death();
            }
            else if (collision.name.Contains("Explosion"))
            {
                var grenade = collision.gameObject.GetComponent<GrenadeExplosion>();
                health -= grenade.damage;
                death();
            }
        }
    }

    protected void death()
    {
        if (health <= 0)
        {
            if(!died_once)
            {
                Instantiate(delete_effect, new Vector3(this.transform.position.x, this.transform.position.y - 0.05f, this.transform.position.z), this.transform.rotation);
                if(!this.transform.name.Contains("Death"))
                {
                    sound_effect_player.PlayEnemyDeath();
                    Destroy(this.gameObject);
                }
                GameManager.num_enemies_active -= 1;
                if (GameManager.num_enemies_active == 0)
                {
                    GameManager.enemies_dead = true;
                }
                died_once = true;
            }
            
        }
    }

    protected void scale_down()
    {
        if (!this.name.Contains("Death"))
        {
            this.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        body_hitbox.enabled = false;
    }

    protected void scale_up()
    {
        if(!this.name.Contains("Death"))
        {
            this.transform.localScale = new Vector3(1.5f, 1.5f, 1f);
        }
        enemy_spawned = true;
        GameManager.enemies_spawned = true;
        body_hitbox.enabled = true;
    }
}
