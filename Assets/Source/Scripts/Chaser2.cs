using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser2 : Base_ChaserEnemy
{
    private bool made_contact = false;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public base_projectile projectile;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Update()
    {
        if(enemy_spawned)
        {
            base.Update();
            if (cooldown_timer <= 0 && made_contact)
            {
                Instantiate(projectile, this.transform.position, this.transform.rotation);
                Destroy(this.gameObject);
            }
        }

        if(GameManager.boss_dead)
        {
            Destroy(this.gameObject);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if(enemy_spawned)
        {
            base.OnTriggerEnter2D(collision);
            if (collision.gameObject.name.Contains("Player"))
            {
                made_contact = true;
                animator.SetBool("MadeContact", true);
                spriteRenderer.color = Color.red;
            }
        }
    }

}


