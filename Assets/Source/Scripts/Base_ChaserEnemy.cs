using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base_ChaserEnemy : Base_Enemy
{

    protected Rigidbody2D rb;
    public float movement_speed;
    Vector2 move_direction;
    public float knockback_force;
    private bool can_move = true;
    private float knockback_timer = 0.2f;
    public bool apply_knockback = true;
    protected float cooldown_timer;
    public float cooldown_length;
    protected bool in_cooldown = false;
    protected Collider2D[] colliders;
    private bool reset_enemy_collider = false;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
        colliders = this.gameObject.GetComponentsInChildren<Collider2D>();
        colliders[0].enabled = false;
    }

    protected override void Update()
    {
        if(enemy_spawned)
        {
            if(!reset_enemy_collider)
            {
                colliders[0].enabled = true;
                reset_enemy_collider = true;
            }
            base.Update();
            move_direction = (player_pos - this.transform.position).normalized;
            if (in_cooldown)
            {
                cooldown_timer -= Time.deltaTime;
                if (cooldown_timer <= 0)
                {
                    in_cooldown = false;
                }
            }
            if (!can_move)
            {
                knockback_timer -= Time.deltaTime;
                if (knockback_timer <= 0)
                {
                    can_move = true;
                    colliders[0].enabled = true;
                }
            }
        }
    }

    protected virtual void FixedUpdate()
    {
        if(enemy_spawned)
        {
            if (!in_cooldown && can_move)
            {
                rb.MovePosition(rb.position + move_direction * movement_speed * Time.fixedDeltaTime);
            }
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(enemy_spawned)
        {
            base.OnTriggerEnter2D(collision);

            if (collision.GetComponentInParent<PlayerController>() != null)
            {
                if(in_cooldown && GetComponent<Chaser2>() != null)
                {
                    return;
                }
                in_cooldown = true;
                cooldown_timer = cooldown_length;
                colliders[0].enabled = false;

                if (apply_knockback)
                {
                    float new_knockback_force = knockback_force;
                    if (collision.transform.name.Contains("Shotgun"))
                    {
                        new_knockback_force = knockback_force * 1.25f;
                    }
                    if (collision.transform.name.Contains("Grenade"))
                    {
                        new_knockback_force = knockback_force * 2f;
                    }

                    Vector2 direction = (this.transform.position - collision.transform.position).normalized;

                    Vector2 knockback = new_knockback_force * direction;

                    rb.AddForce(knockback, ForceMode2D.Impulse);
                    can_move = false;
                    knockback_timer = 0.2f;
                }
            }
            else
            {
                if (apply_knockback)
                {
                    float new_knockback_force = knockback_force;
                    if (collision.transform.name.Contains("Shotgun"))
                    {
                        new_knockback_force = knockback_force * 1.25f;
                    }
                    if (collision.transform.name.Contains("Grenade"))
                    {
                        new_knockback_force = knockback_force * 2f;
                    }

                    Vector2 direction = (this.transform.position - collision.transform.position).normalized;

                    Vector2 knockback = new_knockback_force * direction;

                    rb.AddForce(knockback, ForceMode2D.Impulse);
                    can_move = false;
                    knockback_timer = 0.2f;
                }
            }
        }
    }
}
