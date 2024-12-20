using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ShotgunShellSpawner : MonoBehaviour
{
    public ObjectPool<base_projectile> shotgun_shell_pool;
    private Base_Gun weapon;
    [SerializeField] base_projectile shotgun_child_bullet;
    private EffectSpawner effect_spawner;
    public int bullet_spawned = 0;

    private void Start()
    {
        weapon = GetComponent<Base_Gun>();
        effect_spawner = GetComponent<EffectSpawner>();
        shotgun_shell_pool = new ObjectPool<base_projectile>(CreateShotgunShell, OnTakeBulletFromPool, OnReturnBulletToPool, OnDestroyBullet, true, 40, 200);
    }

    private base_projectile CreateShotgunShell()
    {
        base_projectile bullet = Instantiate(shotgun_child_bullet, weapon.spawn_location.transform.position, weapon.spawn_location.transform.rotation);
        bullet.SetPool(shotgun_shell_pool, effect_spawner);
        return bullet;
    }

    private void OnTakeBulletFromPool(base_projectile bullet)
    {
        if(bullet_spawned == 0)
        {
            bullet.transform.position = weapon.spawn_location.transform.position;
            bullet.transform.rotation = weapon.spawn_location.transform.rotation;
            bullet.released = false;
            bullet.gameObject.SetActive(true);
        }
        else if(bullet_spawned == 1)
        {
            bullet.transform.position = new Vector3(weapon.spawn_location.transform.position.x, weapon.spawn_location.transform.position.y, weapon.spawn_location.transform.position.z);
            bullet.transform.rotation = weapon.spawn_location.transform.rotation * Quaternion.Euler(0, 0, 15);
            bullet.released = false;
            bullet.gameObject.SetActive(true);
        }
        else
        {
            bullet.transform.position = new Vector3(weapon.spawn_location.transform.position.x, weapon.spawn_location.transform.position.y, weapon.spawn_location.transform.position.z);
            bullet.transform.rotation = weapon.spawn_location.transform.rotation * Quaternion.Euler(0, 0, -15);
            bullet.released = false;
            bullet.gameObject.SetActive(true);
        }
    }

    private void OnReturnBulletToPool(base_projectile bullet)
    {
        bullet.gameObject.SetActive(false);
    }

    private void OnDestroyBullet(base_projectile bullet)
    {
        Destroy(bullet.gameObject);
    }
}
