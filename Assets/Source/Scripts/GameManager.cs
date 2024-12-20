using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static PlayerController player;
    public static Vector2 player_grid_position = Vector2.zero;
    private static GameObject virtual_camera;
    public static bool wave_active = false;
    public static int num_enemies_active = 0;
    public static bool enemies_dead = true;
    public static int waves_completed = 0;
    public static int player_level = 1; // up to 5
    public static bool enemies_spawned = false;
    public static int chosen_weapon = 0; //1 = pistol 2 = shotgun 3 = plasma 4 = grenade
    public static bool red_key_collected = false;
    public static bool green_key_collected = false;
    public static bool yellow_key_collected = false;
    public static bool purple_key_collected = false;
    public static bool boss_spawned = false;
    public static bool boss_dead = false;
    public static int[] random_key_order;
    public static int key_num_spawned = 0;
    private static GameObject minimap_camera;
    private static GameObject minimap_mono;
    private Health health_script;

    void Start()
    {
        virtual_camera = GameObject.Find("Virtual Camera");
        random_key_order = GenerateRandomKeyOrder(4, 0, 3);
        minimap_camera = GameObject.Find("Minimap Camera");
        minimap_mono = GameObject.Find("MinimapMono");
    }

    public static void ResetGameValues()
    {
        wave_active = false;
        num_enemies_active = 0;
        enemies_dead = true;
        waves_completed = 0;
        player_level = 1;
        enemies_spawned = false;
        chosen_weapon = 0;
        red_key_collected = false;
        purple_key_collected = false;
        yellow_key_collected = false;
        green_key_collected = false;
        boss_spawned = false;
        boss_dead = false;
        key_num_spawned = 0;
        PauseMenu.game_paused = false;
        Time.timeScale = 1.0f;
        player_grid_position = Vector2.zero;
    }

    public static void UpdateCameraPosition()
    {
        virtual_camera.transform.position = new Vector3(player_grid_position.x * 3.83996f, player_grid_position.y * 2.239966f, virtual_camera.transform.position.z);
        minimap_camera.transform.position = new Vector3(player_grid_position.x * 3.83996f, player_grid_position.y * 2.239966f, virtual_camera.transform.position.z);
        minimap_mono.transform.position = new Vector3(player_grid_position.x * 3.83996f, player_grid_position.y * 2.239966f, 1);
    }

    int[] GenerateRandomKeyOrder(int size, int minValue, int maxValue) //Fisher-Yates algorithm
    {
        int[] array = new int[size];
        for (int i = 0; i < size; i++)
        {
            array[i] = minValue + i; 
        }

        for (int i = array.Length - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            int temp = array[i];
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
        return array;
    }
}
