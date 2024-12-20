using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Pool;

public class base_projectile : MonoBehaviour
{
    public float speed = 100;
    public int damage;
    private Rigidbody2D rb;
    public float bullet_life_span;
    public bool use_rigidbody = true;
    public GameObject delete_effect;
    public ObjectPool<base_projectile> pool;
    public EffectSpawner effect_spawner_script;
    public ObjectPool<GrenadeExplosion> explosion_pool;
    public ObjectPool<GrenadeExplosion> explosion_pool_fast;
    public GrenadeExplosionSpawner grenade_explosion_spawner;
    public GrenadeExplosionSpawnerFast grenade_explosion_spawner_fast;
    public bool released = false;
    protected Coroutine destroy_bullet_after_timer;

    protected virtual void Start()
    {
        if (use_rigidbody)
        {
            rb = GetComponent<Rigidbody2D>();
        }
    }

    protected virtual void OnEnable()
    {
        if (!this.name.Contains("Grenade"))
        {
            destroy_bullet_after_timer = StartCoroutine(DeactivateBulletAfterTimer());
        }
    }

    protected virtual void Update()
    {
        
    }

    void FixedUpdate()
    {
        if (use_rigidbody)
        {
            rb.velocity = transform.right * speed * Time.fixedDeltaTime;
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponentInParent<Base_Enemy>() != null && (GameManager.enemies_spawned == false || !collision.GetComponentInParent<Base_Enemy>().enemy_spawned))
        {
            return;
        }
        else
        {
            if (transform.parent != null && (transform.parent.name.Contains("Skull") || transform.parent.name.Contains("Boss")))
            {
                Instantiate(delete_effect, this.transform.position, this.transform.rotation);
                Destroy(this.gameObject);
            }
            else
            {
                if (!released)
                {
                    pool.Release(this);
                    released = true;
                    effect_spawner_script.position_to_spawn_effect = this.transform.position;
                    effect_spawner_script.pool.Get();
                }
            }
        }
    }

    public void RemoveProjectileAtEndOfWave()
    {
        if (this.transform.parent != null && transform.parent.name.Contains("Skull"))
        {
            return;
        }
        else
        {
            if (!released)
            {
                pool.Release(this);
                released = true;
            }
            effect_spawner_script.position_to_spawn_effect = this.transform.position;
            effect_spawner_script.pool.Get();
        }
    }

    public void SetPool(ObjectPool<base_projectile> _pool, EffectSpawner _effect_spawner_script)
    {
        pool = _pool;
        effect_spawner_script = _effect_spawner_script;
    }

    protected IEnumerator DeactivateBulletAfterTimer()
    {
        float timer = 0f;
        while(timer < bullet_life_span)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        if (!released)
        {
            pool.Release(this);
            released = true;
        }
        
    }
}
