using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class GrenadeExplosionSpawner : MonoBehaviour
{
    public ObjectPool<GrenadeExplosion> pool;
    public Vector3 explosion_spawn_location;
    public Vector3 explosion_rotation;
    public GrenadeExplosion slow_explosion;

    private void Start()
    {
        pool = new ObjectPool<GrenadeExplosion>(CreateExplosion, OnTakeExplosionFromPool, OnReturnExplosionToPool, OnDestroyExplosion, true, 40, 200);
    }

    private GrenadeExplosion CreateExplosion()
    {
        GrenadeExplosion explosion = Instantiate(slow_explosion, explosion_spawn_location, Quaternion.Euler(explosion_rotation));
        explosion.SetPool(pool);
        return explosion;
    }

    private void OnTakeExplosionFromPool(GrenadeExplosion explosion)
    {
        explosion.transform.position = explosion_spawn_location;
        explosion.transform.rotation = Quaternion.Euler(explosion_rotation);
        explosion.gameObject.SetActive(true);
    }

    private void OnReturnExplosionToPool(GrenadeExplosion explosion)
    {
        explosion.gameObject.SetActive(false);
    }

    private void OnDestroyExplosion(GrenadeExplosion explosion)
    {
        Destroy(explosion.gameObject);
    }
}
