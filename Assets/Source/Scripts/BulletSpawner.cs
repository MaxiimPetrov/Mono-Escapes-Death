using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BulletSpawner : MonoBehaviour
{
    public ObjectPool<base_projectile> pool;
    private Base_Gun weapon;
    private TrackerSpawner tracker_spawner;
    private RandomSpawner random_spawner;
    [SerializeField] public base_projectile enemy_bullet;
    public Vector3 rotate_spawner_position;
    public Vector3 rotate_spawner_rotation;
    private RotatingSpawner rotating_spawner;
    private EffectSpawner effect_spawner;

    private void Start()
    {
        weapon = GetComponent<Base_Gun>();
        tracker_spawner = GetComponent<TrackerSpawner>();
        random_spawner = GetComponent <RandomSpawner>();
        rotating_spawner = GetComponent<RotatingSpawner>();
        effect_spawner = GetComponent<EffectSpawner>();
        pool = new ObjectPool<base_projectile>(CreateBullet, OnTakeBulletFromPool, OnReturnBulletToPool, OnDestroyBullet, true, 40, 200);
    }


    private base_projectile CreateBullet()
    {

        if(tracker_spawner != null)
        {
            Vector2 direction = new Vector2(PlayerController.player_position.x - this.transform.position.x, PlayerController.player_position.y - this.transform.position.y);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            base_projectile bullet = Instantiate(enemy_bullet, this.transform.position, Quaternion.Euler(0, 0, angle));
            bullet.SetPool(pool, effect_spawner);
            return bullet;
        }
        else if(random_spawner != null)
        {
            int random_angle = Random.Range(-180, 180);
            base_projectile bullet = Instantiate(enemy_bullet, this.transform.position, Quaternion.Euler(0, 0, random_angle));
            bullet.SetPool(pool, effect_spawner);
            return bullet;
        }
        else if(rotating_spawner != null)
        {
            base_projectile bullet = Instantiate(enemy_bullet, rotate_spawner_position, Quaternion.Euler(rotate_spawner_rotation));
            bullet.SetPool(pool, effect_spawner);
            return bullet;
        }
        else if(weapon.name.Contains("Shotgun"))
        {
            base_projectile bullet = Instantiate(weapon.projectile, weapon.spawn_location.transform.position, weapon.spawn_location.transform.rotation);

            ShotgunShellSpawner shotgun_shell_spawner = GetComponent<ShotgunShellSpawner>();
            if(shotgun_shell_spawner != null)
            {
                shotgun_shell_spawner.shotgun_shell_pool.Get();
                shotgun_shell_spawner.bullet_spawned++;
                shotgun_shell_spawner.shotgun_shell_pool.Get();
                shotgun_shell_spawner.bullet_spawned++;
                shotgun_shell_spawner.shotgun_shell_pool.Get();
                shotgun_shell_spawner.bullet_spawned = 0;
            }

            bullet.SetPool(pool, effect_spawner);
            return bullet;
        }
        else if(weapon.name.Contains("Grenade"))
        {
            base_projectile bullet = Instantiate(weapon.projectile, weapon.spawn_location.transform.position, weapon.spawn_location.transform.rotation);
            bullet.SetPool(pool, effect_spawner);
            GrenadeExplosionSpawner grenade_explosion_spawner = GetComponent<GrenadeExplosionSpawner>();
            bullet.grenade_explosion_spawner = grenade_explosion_spawner;
            bullet.explosion_pool = grenade_explosion_spawner.pool;
            GrenadeExplosionSpawnerFast grenade_explosion_spawner_fast = GetComponent<GrenadeExplosionSpawnerFast>();
            bullet.grenade_explosion_spawner_fast = grenade_explosion_spawner_fast;
            bullet.explosion_pool_fast = grenade_explosion_spawner_fast.pool;
            return bullet;
        }
        else
        {
            base_projectile bullet = Instantiate(weapon.projectile, weapon.spawn_location.transform.position, weapon.spawn_location.transform.rotation);
            bullet.SetPool(pool, effect_spawner);
            return bullet;
        }
        
    }
    private void OnTakeBulletFromPool(base_projectile bullet)
    {
        if (tracker_spawner != null)
        {
            Vector2 direction = new Vector2(PlayerController.player_position.x - this.transform.position.x, PlayerController.player_position.y - this.transform.position.y);
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            bullet.transform.position = this.transform.position;
            bullet.transform.rotation = Quaternion.Euler(0, 0, angle);

            bullet.released = false;
            bullet.gameObject.SetActive(true);
        }
        else if (random_spawner != null)
        {
            int random_angle = Random.Range(-180, 180);
            bullet.transform.position = this.transform.position;
            bullet.transform.rotation = Quaternion.Euler(0, 0, random_angle);
            bullet.released = false;
            bullet.gameObject.SetActive(true);
        }
        else if (rotating_spawner != null)
        {
            bullet.transform.position = rotate_spawner_position;
            bullet.transform.rotation = Quaternion.Euler(rotate_spawner_rotation);
            bullet.released = false;
            bullet.gameObject.SetActive(true);
        }
        else if (weapon.name.Contains("Shotgun"))
        {
            bullet.transform.position = weapon.spawn_location.transform.position;
            bullet.transform.rotation = weapon.spawn_location.transform.rotation;
            bullet.released = false;
            bullet.gameObject.SetActive(true);
            ShotgunShellSpawner shotgun_shell_spawner = GetComponent<ShotgunShellSpawner>();
            if (shotgun_shell_spawner != null)
            {
                shotgun_shell_spawner.shotgun_shell_pool.Get();
                shotgun_shell_spawner.bullet_spawned++;
                shotgun_shell_spawner.shotgun_shell_pool.Get();
                shotgun_shell_spawner.bullet_spawned++;
                shotgun_shell_spawner.shotgun_shell_pool.Get();
                shotgun_shell_spawner.bullet_spawned = 0;
            }
        }
        else
        {
            bullet.transform.position = weapon.spawn_location.transform.position;
            bullet.transform.rotation = weapon.spawn_location.transform.rotation;
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
