using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter3 : Base_Enemy
{
    private Rigidbody2D rb;
    public float speed;
    private Vector3 last_velocity;
    private Vector3 direction;
    private bool bouncing = false;
    private bool bounced_once = false;
    private float timer = 0.2f;
    private bool start_timer = false;
    float small_value = 0.0001f;
    private float gravity_value = 1;
    private bool initial_push = false;
    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
    }

    protected override void Update()
    {
        if(enemy_spawned)
        {
            if(!initial_push)
            {
                int random_speed = Random.Range(1, 5);

                if (random_speed == 1)
                {
                    rb.AddForce(new Vector2(speed, speed));
                }
                else if (random_speed == 2)
                {
                    rb.AddForce(new Vector2(-speed, speed));
                }
                else if (random_speed == 3)
                {
                    rb.AddForce(new Vector2(-speed, -speed));
                }
                else
                {
                    rb.AddForce(new Vector2(speed, -speed));
                }
                initial_push = true;
            }

            if (rb.velocity.x < 0)
            {
                sprite.flipX = true;
            }
            else
            {
                sprite.flipX = false;
            }

            if (start_timer)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    start_timer = false;
                    bouncing = true;
                }
            }
        }
    }

    void FixedUpdate()
    {
        if(enemy_spawned)
        {
            last_velocity = rb.velocity;

            if (bouncing && Mathf.Abs(rb.velocity.x) < small_value && Mathf.Abs(rb.velocity.y) < small_value)
            {
                rb.velocity = Vector2.zero;
                rb.AddForce(new Vector2(speed, speed * gravity_value));
            }

            if (bouncing && Mathf.Abs(rb.velocity.x) < small_value && Mathf.Abs(rb.velocity.y) > small_value)
            {
                rb.velocity = Vector2.zero;
                rb.AddForce(new Vector2(-speed, speed * gravity_value));
            }

            if (((Mathf.Abs(rb.velocity.x) > small_value && Mathf.Abs(rb.velocity.y) < small_value) ||
                (Mathf.Abs(rb.velocity.x) < small_value && Mathf.Abs(rb.velocity.y) > small_value)) && !bouncing)
            {
                rb.gravityScale = 0.5f * gravity_value;
                timer = 0.2f;
                start_timer = true;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(enemy_spawned)
        {
            if (bouncing && bounced_once)
            {
                rb.gravityScale = 0;
                rb.velocity = Vector2.zero;
                int random_speed = Random.Range(1, 3);

                if (random_speed == 1)
                {
                    rb.AddForce(new Vector2(speed, speed * gravity_value));
                }
                else
                {
                    rb.AddForce(new Vector2(-speed, speed * gravity_value));
                }
                bouncing = false;
                bounced_once = false;
                gravity_value *= -1;
                return;
            }

            if (bouncing)
            {
                bounced_once = true;
            }

            var local_speed = last_velocity.magnitude;
            direction = Vector3.Reflect(last_velocity.normalized, collision.contacts[0].normal);
            rb.velocity = direction * local_speed;
        }
    }
}
