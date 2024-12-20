using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class GrenadeExplosion : MonoBehaviour
{
    public int damage;
    private CircleCollider2D circle_collider;
    public ObjectPool<GrenadeExplosion> pool;

    void Start()
    {
        circle_collider = GetComponent<CircleCollider2D>();
        circle_collider.enabled = false;
    }

    private void delete_explosion()
    {
        pool.Release(this);
    }

    private void activate_hitbox()
    {
        circle_collider.enabled = true;
    }

    private void deactivate_hitbox()
    {
        circle_collider.enabled = false;
    }

    public void SetPool(ObjectPool<GrenadeExplosion> _pool)
    {
        pool = _pool;
    }
}
