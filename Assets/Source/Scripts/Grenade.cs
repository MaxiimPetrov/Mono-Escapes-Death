using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.U2D;

public class Grenade : base_projectile
{
    private float living_timer;
    public GameObject explosion;
    public GameObject explosion_fast;

    protected override void Start()
    {
        base.Start();
    }

    protected override void OnEnable()
    {
        destroy_bullet_after_timer = StartCoroutine(DeactivateGrenadeAfterTimer());
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<Base_Enemy>() != null && GameManager.enemies_spawned == false)
        {
            return;
        }
        else
        {
            explode_fast();
        }
    }

    private void explode_long()
    {
        Transform sprite_rotation = this.transform.GetChild(0);

        grenade_explosion_spawner.explosion_spawn_location = this.transform.position;
        grenade_explosion_spawner.explosion_rotation = this.transform.rotation.eulerAngles;
        explosion_pool.Get();

        if (!released)
        {
            pool.Release(this);
            released = true;
        }
    }

    private void explode_fast()
    {
        Transform sprite_rotation = this.transform.GetChild(0);
        grenade_explosion_spawner_fast.explosion_spawn_location = this.transform.position;
        grenade_explosion_spawner_fast.explosion_rotation = this.transform.rotation.eulerAngles;
        explosion_pool_fast.Get();

        if (!released)
        {
            pool.Release(this);
            released = true;
        }
    }

    protected IEnumerator DeactivateGrenadeAfterTimer()
    {
        float timer = 0f;
        while (timer < bullet_life_span)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        explode_long();
    }
}
