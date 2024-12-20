using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class Room : MonoBehaviour
{
    public bool door_top, door_bottom, door_left, door_right;
    public bool boss_door_top, boss_door_bottom, boss_door_left, boss_door_right;
    public float relative_door_position_x = 1.85f;
    public float relative_door_position_y = 1.04f;
    [SerializeField] GameObject door;
    [SerializeField] GameObject boss_door;
    [SerializeField] GameObject banana_spawn;
    [SerializeField] GameObject MMI_Left;
    [SerializeField] GameObject MMI_Right;
    [SerializeField] GameObject MMI_Top;
    [SerializeField] GameObject MMI_Bottom;
    [SerializeField] GameObject MMI_BottomLeft;
    [SerializeField] GameObject MMI_BottomLeftRight;
    [SerializeField] GameObject MMI_BottomRight;
    [SerializeField] GameObject MMI_TopBottomRight;
    [SerializeField] GameObject MMI_LeftRight;
    [SerializeField] GameObject MMI_TopBottom;
    [SerializeField] GameObject MMI_TopBottomLeft;
    [SerializeField] GameObject MMI_TopBottomLeftRight;
    [SerializeField] GameObject MMI_TopLeft;
    [SerializeField] GameObject MMI_TopLeftRight;
    [SerializeField] GameObject MMI_TopRight;
    private GameObject minimap_image;
    public GameObject wave_type_attatched;
    private bool room_completed = false;
    public int room_type; // 0 = start room | 1 = normal room | 2 = boss room
    public Vector2 grid_position;
    private bool wave_started_here = false;
    private GameObject wave_object;
    public GameObject[] keys;

    private void Start()
    {
        if (room_type != 2)
        {
            if (door_top)
            {
                if (boss_door_top)
                {
                    GameObject new_door = Instantiate(boss_door, new Vector2(this.transform.position.x, this.transform.position.y + relative_door_position_y), Quaternion.identity);
                    Door door_script = new_door.GetComponent<Door>();
                    door_script.is_door_top = true;
                    door_script.is_boss_door = true;
                }
                else
                {
                    GameObject new_door = Instantiate(door, new Vector2(this.transform.position.x, this.transform.position.y + relative_door_position_y), Quaternion.identity);
                    Door door_script = new_door.GetComponent<Door>();
                    door_script.is_door_top = true;
                }
            }
            if (door_bottom)
            {
                if (boss_door_bottom)
                {
                    GameObject new_door = Instantiate(boss_door, new Vector2(this.transform.position.x, this.transform.position.y - relative_door_position_y), Quaternion.Euler(0f, 0f, 180f));
                    Door door_script = new_door.GetComponent<Door>();
                    door_script.is_door_bottom = true;
                    door_script.is_boss_door = true;
                }
                else
                {
                    GameObject new_door = Instantiate(door, new Vector2(this.transform.position.x, this.transform.position.y - relative_door_position_y), Quaternion.Euler(0f, 0f, 180f));
                    Door door_script = new_door.GetComponent<Door>();
                    door_script.is_door_bottom = true;
                }
            }
            if (door_left)
            {
                if (boss_door_left)
                {
                    GameObject new_door = Instantiate(boss_door, new Vector2(this.transform.position.x - relative_door_position_x, this.transform.position.y), Quaternion.Euler(0f, 0f, 90f));
                    Door door_script = new_door.GetComponent<Door>();
                    door_script.is_door_left = true;
                    door_script.is_boss_door = true;
                }
                else
                {
                    GameObject new_door = Instantiate(door, new Vector2(this.transform.position.x - relative_door_position_x, this.transform.position.y), Quaternion.Euler(0f, 0f, 90f));
                    Door door_script = new_door.GetComponent<Door>();
                    door_script.is_door_left = true;
                }
            }
            if (door_right)
            {
                if (boss_door_right)
                {
                    GameObject new_door = Instantiate(boss_door, new Vector2(this.transform.position.x + relative_door_position_x, this.transform.position.y), Quaternion.Euler(0f, 0f, 270f));
                    Door door_script = new_door.GetComponent<Door>();
                    door_script.is_door_right = true;
                    door_script.is_boss_door = true;
                }
                else
                {
                    GameObject new_door = Instantiate(door, new Vector2(this.transform.position.x + relative_door_position_x, this.transform.position.y), Quaternion.Euler(0f, 0f, 270f));
                    Door door_script = new_door.GetComponent<Door>();
                    door_script.is_door_right = true;
                }
            }
        }

        SpawnMinimapForRoom();

        if(room_type == 0)
        {
            SpriteRenderer minimap_sprite = minimap_image.GetComponentInChildren<SpriteRenderer>();
            minimap_sprite.color = Color.green;
        }

        if (room_type == 2)
        {
            SpriteRenderer minimap_sprite = minimap_image.GetComponentInChildren<SpriteRenderer>();
            minimap_sprite.color = Color.red;
        }
    }

    public Room(Vector2 _grid_position, int _room_type, GameObject _wave_type_attatched)
    {
        grid_position = _grid_position;
        room_type = _room_type;
        wave_type_attatched = _wave_type_attatched;
    }

    private void FixedUpdate()
    {
        if(GameManager.enemies_dead && wave_started_here && !room_completed)
        {
            GameManager.waves_completed++;
            room_completed = true;
            Destroy(wave_object);
            GameManager.enemies_spawned = false;

            SpriteRenderer minimap_sprite = minimap_image.GetComponentInChildren<SpriteRenderer>();
            minimap_sprite.color = Color.green;

            base_projectile[] all_enemy_projectiles = FindObjectsOfType<base_projectile>();

            foreach(base_projectile existent_projectiles in all_enemy_projectiles)
            {
                if ((existent_projectiles.name.ToLower().Contains("enemy") && existent_projectiles.name.ToLower().Contains("projectile")) || (existent_projectiles.name.ToLower().Contains("boss") && existent_projectiles.name.ToLower().Contains("projectile")))
                {
                    base_projectile projectile_script = existent_projectiles.GetComponent<base_projectile>();
                    if (projectile_script != null)
                    {
                        projectile_script.RemoveProjectileAtEndOfWave();
                    }
                }
            }
            
            if(GameManager.waves_completed % 7 == 0)
            {
                if(GameManager.waves_completed == 14 || GameManager.waves_completed == 28)
                {
                    Instantiate(keys[GameManager.random_key_order[GameManager.key_num_spawned]], new Vector3(this.transform.position.x - 0.3f, this.transform.position.y, this.transform.position.z), Quaternion.identity);
                    Instantiate(banana_spawn, new Vector3(this.transform.position.x + 0.3f, this.transform.position.y, this.transform.position.z), Quaternion.identity);
                    GameManager.key_num_spawned++;
                }
                else
                {
                    Instantiate(keys[GameManager.random_key_order[GameManager.key_num_spawned]], this.transform.position, Quaternion.identity);
                    GameManager.key_num_spawned++;
                }
            }
            else if(GameManager.waves_completed % 2 == 0)
            {
                Instantiate(banana_spawn, this.transform.position, Quaternion.identity);
            }
            else
            {
                GameManager.wave_active = false;
            }
        }
    }

    public void SetDoors(bool door_top_, bool door_bottom_, bool door_right_, bool door_left_, bool boss_door_top_, bool boss_door_bottom_, bool boss_door_right_, bool boss_door_left_)
    {
        door_top = door_top_;
        door_bottom = door_bottom_;
        door_left = door_left_; 
        door_right = door_right_;
        boss_door_top = boss_door_top_;
        boss_door_bottom = boss_door_bottom_;
        boss_door_left = boss_door_left_;
        boss_door_right = boss_door_right_;
    }

    public void SetRoomInfo(Vector2 _grid_position, int _room_type, GameObject _wave_type_attatched)
    {
        grid_position = _grid_position;
        room_type = _room_type;
        wave_type_attatched = _wave_type_attatched;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!room_completed && wave_type_attatched != null && !GameManager.wave_active)
        {
            wave_object = Instantiate(wave_type_attatched, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);
            GameManager.enemies_dead = false;
            GameManager.wave_active = true;
            wave_started_here = true;
        }
    }

    private void SpawnMinimapForRoom()
    {
        if (door_top && !door_bottom && !door_right && !door_left)
        {
            // Room with only a top door
            minimap_image = Instantiate(MMI_Top, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);
        }
        else if (!door_top && door_bottom && !door_right && !door_left)
        {
            // Room with only a bottom door
            minimap_image = Instantiate(MMI_Bottom, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);
        }
        else if (!door_top && !door_bottom && door_right && !door_left)
        {
            // Room with only a right door
            minimap_image = Instantiate(MMI_Right, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);
        }
        else if (!door_top && !door_bottom && !door_right && door_left)
        {
            // Room with only a left door
            minimap_image = Instantiate(MMI_Left, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);
        }
        else if (door_top && door_bottom && !door_right && !door_left)
        {
            // Room with top and bottom doors
            minimap_image = Instantiate(MMI_TopBottom, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);
        }
        else if (door_top && !door_bottom && !door_right && door_left)
        {
            // Room with top and left doors
            minimap_image = Instantiate(MMI_TopLeft, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);
        }
        else if (door_top && !door_bottom && door_right && !door_left)
        {
            // Room with top and right doors
            minimap_image = Instantiate(MMI_TopRight, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);
        }
        else if (!door_top && door_bottom && !door_right && door_left)
        {
            // Room with bottom and left doors
            minimap_image = Instantiate(MMI_BottomLeft, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);
        }
        else if (!door_top && door_bottom && door_right && !door_left)
        {
            // Room with bottom and right doors
            minimap_image = Instantiate(MMI_BottomRight, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);
        }
        else if (!door_top && !door_bottom && door_right && door_left)
        {
            // Room with right and left doors
            minimap_image = Instantiate(MMI_LeftRight, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);
        }
        else if (door_top && door_bottom && door_right && !door_left)
        {
            // Room with top, bottom, and right doors
            minimap_image = Instantiate(MMI_TopBottomRight, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);
        }
        else if (door_top && door_bottom && door_right && door_left)
        {
            // Room with all doors
            minimap_image = Instantiate(MMI_TopBottomLeftRight, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);
        }
        else if (door_top && door_bottom && door_left && !door_right)
        {
            // Room with top, bottom, and left doors
            minimap_image = Instantiate(MMI_TopBottomLeft, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);
        }
        else if (door_top && door_left && door_right && !door_bottom)
        {
            // Room with top, left, and right doors
            minimap_image = Instantiate(MMI_TopLeftRight, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);
        }
        else if (!door_top && door_bottom && door_left && door_right)
        {
            // Room with bottom, left, and right doors
            minimap_image = Instantiate(MMI_BottomLeftRight, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);
        }
    }
}