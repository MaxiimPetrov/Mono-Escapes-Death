using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class Chaser3 : Base_ChaserEnemy
{
    private bool dashing = false;
    private bool dash_paramters_set = false;
    private float wait_timer = 0.5f;
    private Vector3 static_player_position;
    private Vector2 static_move_direction;
    private bool to_left;
    private Animator animator;
    private SpriteRenderer chaser_3_sprite;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        chaser_3_sprite = GetComponent<SpriteRenderer>();
    }

    protected override void Update()
    {
        if(enemy_spawned)
        {
            player_pos = PlayerController.player_position;
            colliders[0].enabled = true;
            if (wait_timer <= 0)
            {
                if (!dash_paramters_set)
                {
                    dashing = true;
                    static_player_position = player_pos;
                    static_move_direction = (static_player_position - this.transform.position).normalized;
                    dash_paramters_set = true;

                    if (this.transform.position.x > static_player_position.x)
                    {
                        to_left = true;
                        chaser_3_sprite.flipX = true;
                    }
                    else
                    {
                        to_left = false;
                        chaser_3_sprite.flipX = false;
                    }
                    animator.SetBool("Chasing", true);
                }
            }
            else
            {
                wait_timer -= Time.deltaTime;
            }
        }
    }

    protected override void FixedUpdate()
    {
        if(enemy_spawned)
        {
            if (dashing)
            {
                rb.MovePosition(rb.position + static_move_direction * movement_speed * Time.fixedDeltaTime);

                if (to_left)
                {
                    if (this.transform.position.x < static_player_position.x)
                    {
                        dashing = false;
                        dash_paramters_set = false;
                        wait_timer = 0.5f;
                        animator.SetBool("Chasing", false);
                    }
                }
                else
                {
                    if (this.transform.position.x > static_player_position.x)
                    {
                        dashing = false;
                        dash_paramters_set = false;
                        wait_timer = 0.5f;
                        animator.SetBool("Chasing", false);
                    }
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (enemy_spawned)
        {
            dashing = false;
            dash_paramters_set = false;
            wait_timer = 0.5f;
            animator.SetBool("Chasing", false);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (enemy_spawned)
        {
            if (collision.contactCount > 2)
            {
                dashing = false;
                dash_paramters_set = false;
                wait_timer = 0.01f;
                animator.SetBool("Chasing", false);
            }
        }
    }
}

