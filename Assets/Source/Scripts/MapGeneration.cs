using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapGeneration : MonoBehaviour
{
    Vector2 worldSize = new Vector2(6, 6);
    public List<Vector2> takenPositions = new List<Vector2>();
    public Dictionary<Vector2, Room> gridDictionary = new Dictionary<Vector2, Room>();
    int gridSizeX, gridSizeY, numberOfRooms = 30;
    Vector2 boss_room_position;

    [SerializeField] private PlayerController player;

    [Header("Single Door Configurations")]
    [SerializeField] private GameObject room_top;
    [SerializeField] private GameObject room_bottom;
    [SerializeField] private GameObject room_left;
    [SerializeField] private GameObject room_right;

    [Header("Two Door Configurations")]
    [SerializeField] private GameObject room_top_bottom;
    [SerializeField] private GameObject room_top_left;
    [SerializeField] private GameObject room_top_right;
    [SerializeField] private GameObject room_bottom_left;
    [SerializeField] private GameObject room_bottom_right;
    [SerializeField] private GameObject room_left_right;

    [Header("Three Door Configurations")]
    [SerializeField] private GameObject room_top_bottom_left;
    [SerializeField] private GameObject room_top_bottom_right;
    [SerializeField] private GameObject room_top_left_right;
    [SerializeField] private GameObject room_bottom_left_right;

    [Header("Four Door Configuration")]
    [SerializeField] private GameObject room_all_doors;

    [Header("Boss Room")]
    [SerializeField] private GameObject boss_room;

    [Header("Wave Types")]
    [SerializeField] private GameObject[] wave_types;
    [SerializeField] private GameObject boss_wave;
    private int[] random_wave_order;

    [SerializeField] private Base_Gun pistol;
    [SerializeField] private Base_Gun shotgun;
    [SerializeField] private Base_Gun plasma_gun;
    [SerializeField] private Base_Gun grenade_launcher;
    [SerializeField] private GameObject controls;

    void Start()
    {
        if (numberOfRooms >= (worldSize.x * 2) * (worldSize.y * 2))
        {
            numberOfRooms = Mathf.RoundToInt((worldSize.x * 2) * (worldSize.y * 2));
        }
        random_wave_order = GenerateRandomKeyOrder(28, 0, 27);
        gridSizeX = Mathf.RoundToInt(worldSize.x); 
        gridSizeY = Mathf.RoundToInt(worldSize.y);
        CreateRooms(); 
        SetRoomDoors(); 
        SpawnMap();
        Instantiate(controls);
        SpawnPlayer();
    }

    void CreateRooms()
    {
        takenPositions.Insert(0, Vector2.zero);
        gridDictionary.Add(Vector2.zero, new Room(Vector2.zero, 0, null)); //adds start room
        Vector2 checkPos = Vector2.zero;

        float randomCompare = 0.2f, randomCompareStart = 0.2f, randomCompareEnd = 0.01f;

        for (int i = 0; i < numberOfRooms - 1; i++)                                         
        {
            float randomPerc = ((float)i) / (((float)numberOfRooms - 1));
            randomCompare = Mathf.Lerp(randomCompareStart, randomCompareEnd, randomPerc);
            //grab new position
            checkPos = NewPosition();
            //test new position
            if ((NumberOfNeighbors(checkPos, takenPositions) > 1 && Random.value > randomCompare) || i == 28)
            {
                do
                {
                    checkPos = SelectiveNewPosition();
                } while (NumberOfNeighbors(checkPos, takenPositions) > 1);
            }
            takenPositions.Insert(0, checkPos);
            if(i == 28)
            {
                gridDictionary.Add(checkPos, new Room(checkPos, 2, boss_wave)); //boss room
                boss_room_position = checkPos;
            }
            else
            {
                gridDictionary.Add(checkPos, new Room(checkPos, 1, wave_types[random_wave_order[i]]));
            }
        }
    }

    private void SpawnMap()
    {
        foreach (KeyValuePair<Vector2, Room> room in gridDictionary)
        {
            Vector3 position = new Vector3(room.Key.x * 3.83996f, room.Key.y * 2.239966f, 0f);

            if (room.Value.room_type == 2)
            {
                GameObject new_room = Instantiate(boss_room, position, Quaternion.identity);
                Room room_script = new_room.GetComponent<Room>();
                room_script.SetDoors(room.Value.door_top, room.Value.door_bottom, room.Value.door_right, room.Value.door_left, room.Value.boss_door_top, room.Value.boss_door_bottom, room.Value.boss_door_right, room.Value.boss_door_left);
                room_script.SetRoomInfo(room.Value.grid_position, room.Value.room_type, room.Value.wave_type_attatched);
            }
            else if (room.Value.door_top && !room.Value.door_bottom && !room.Value.door_right && !room.Value.door_left)
            {
                // Room with only a top door
                GameObject new_room = Instantiate(room_top, position, Quaternion.identity);
                Room room_script = new_room.GetComponent<Room>();
                room_script.SetDoors(room.Value.door_top, room.Value.door_bottom, room.Value.door_right, room.Value.door_left, room.Value.boss_door_top, room.Value.boss_door_bottom, room.Value.boss_door_right, room.Value.boss_door_left);
                room_script.SetRoomInfo(room.Value.grid_position, room.Value.room_type, room.Value.wave_type_attatched);
            }
            else if (!room.Value.door_top && room.Value.door_bottom && !room.Value.door_right && !room.Value.door_left)
            {
                // Room with only a bottom door
                GameObject new_room = Instantiate(room_bottom, position, Quaternion.identity);
                Room room_script = new_room.GetComponent<Room>();
                room_script.SetDoors(room.Value.door_top, room.Value.door_bottom, room.Value.door_right, room.Value.door_left, room.Value.boss_door_top, room.Value.boss_door_bottom, room.Value.boss_door_right, room.Value.boss_door_left);
                room_script.SetRoomInfo(room.Value.grid_position, room.Value.room_type, room.Value.wave_type_attatched);
            }
            else if (!room.Value.door_top && !room.Value.door_bottom && room.Value.door_right && !room.Value.door_left)
            {
                // Room with only a right door
                GameObject new_room = Instantiate(room_right, position, Quaternion.identity);
                Room room_script = new_room.GetComponent<Room>();
                room_script.SetDoors(room.Value.door_top, room.Value.door_bottom, room.Value.door_right, room.Value.door_left, room.Value.boss_door_top, room.Value.boss_door_bottom, room.Value.boss_door_right, room.Value.boss_door_left);
                room_script.SetRoomInfo(room.Value.grid_position, room.Value.room_type, room.Value.wave_type_attatched);
            }
            else if (!room.Value.door_top && !room.Value.door_bottom && !room.Value.door_right && room.Value.door_left)
            {
                // Room with only a left door
                GameObject new_room = Instantiate(room_left, position, Quaternion.identity);
                Room room_script = new_room.GetComponent<Room>();
                room_script.SetDoors(room.Value.door_top, room.Value.door_bottom, room.Value.door_right, room.Value.door_left, room.Value.boss_door_top, room.Value.boss_door_bottom, room.Value.boss_door_right, room.Value.boss_door_left);
                room_script.SetRoomInfo(room.Value.grid_position, room.Value.room_type, room.Value.wave_type_attatched);
            }
            else if (room.Value.door_top && room.Value.door_bottom && !room.Value.door_right && !room.Value.door_left)
            {
                // Room with top and bottom doors
                GameObject new_room = Instantiate(room_top_bottom, position, Quaternion.identity);
                Room room_script = new_room.GetComponent<Room>();
                room_script.SetDoors(room.Value.door_top, room.Value.door_bottom, room.Value.door_right, room.Value.door_left, room.Value.boss_door_top, room.Value.boss_door_bottom, room.Value.boss_door_right, room.Value.boss_door_left);
                room_script.SetRoomInfo(room.Value.grid_position, room.Value.room_type, room.Value.wave_type_attatched);
            }
            else if (room.Value.door_top && !room.Value.door_bottom && !room.Value.door_right && room.Value.door_left)
            {
                // Room with top and left doors
                GameObject new_room = Instantiate(room_top_left, position, Quaternion.identity);
                Room room_script = new_room.GetComponent<Room>();
                room_script.SetDoors(room.Value.door_top, room.Value.door_bottom, room.Value.door_right, room.Value.door_left, room.Value.boss_door_top, room.Value.boss_door_bottom, room.Value.boss_door_right, room.Value.boss_door_left);
                room_script.SetRoomInfo(room.Value.grid_position, room.Value.room_type, room.Value.wave_type_attatched);
            }
            else if (room.Value.door_top && !room.Value.door_bottom && room.Value.door_right && !room.Value.door_left)
            {
                // Room with top and right doors
                GameObject new_room = Instantiate(room_top_right, position, Quaternion.identity);
                Room room_script = new_room.GetComponent<Room>();
                room_script.SetDoors(room.Value.door_top, room.Value.door_bottom, room.Value.door_right, room.Value.door_left, room.Value.boss_door_top, room.Value.boss_door_bottom, room.Value.boss_door_right, room.Value.boss_door_left);
                room_script.SetRoomInfo(room.Value.grid_position, room.Value.room_type, room.Value.wave_type_attatched);
            }
            else if (!room.Value.door_top && room.Value.door_bottom && !room.Value.door_right && room.Value.door_left)
            {
                // Room with bottom and left doors
                GameObject new_room = Instantiate(room_bottom_left, position, Quaternion.identity);
                Room room_script = new_room.GetComponent<Room>();
                room_script.SetDoors(room.Value.door_top, room.Value.door_bottom, room.Value.door_right, room.Value.door_left, room.Value.boss_door_top, room.Value.boss_door_bottom, room.Value.boss_door_right, room.Value.boss_door_left);
                room_script.SetRoomInfo(room.Value.grid_position, room.Value.room_type, room.Value.wave_type_attatched);
            }
            else if (!room.Value.door_top && room.Value.door_bottom && room.Value.door_right && !room.Value.door_left)
            {
                // Room with bottom and right doors
                GameObject new_room = Instantiate(room_bottom_right, position, Quaternion.identity);
                Room room_script = new_room.GetComponent<Room>();
                room_script.SetDoors(room.Value.door_top, room.Value.door_bottom, room.Value.door_right, room.Value.door_left, room.Value.boss_door_top, room.Value.boss_door_bottom, room.Value.boss_door_right, room.Value.boss_door_left);
                room_script.SetRoomInfo(room.Value.grid_position, room.Value.room_type, room.Value.wave_type_attatched);
            }
            else if (!room.Value.door_top && !room.Value.door_bottom && room.Value.door_right && room.Value.door_left)
            {
                // Room with right and left doors
                GameObject new_room = Instantiate(room_left_right, position, Quaternion.identity);
                Room room_script = new_room.GetComponent<Room>();
                room_script.SetDoors(room.Value.door_top, room.Value.door_bottom, room.Value.door_right, room.Value.door_left, room.Value.boss_door_top, room.Value.boss_door_bottom, room.Value.boss_door_right, room.Value.boss_door_left);
                room_script.SetRoomInfo(room.Value.grid_position, room.Value.room_type, room.Value.wave_type_attatched);
            }
            else if (room.Value.door_top && room.Value.door_bottom && room.Value.door_right && !room.Value.door_left)
            {
                // Room with top, bottom, and right doors
                GameObject new_room = Instantiate(room_top_bottom_right, position, Quaternion.identity);
                Room room_script = new_room.GetComponent<Room>();
                room_script.SetDoors(room.Value.door_top, room.Value.door_bottom, room.Value.door_right, room.Value.door_left, room.Value.boss_door_top, room.Value.boss_door_bottom, room.Value.boss_door_right, room.Value.boss_door_left);
                room_script.SetRoomInfo(room.Value.grid_position, room.Value.room_type, room.Value.wave_type_attatched);
            }
            else if (room.Value.door_top && room.Value.door_bottom && room.Value.door_right && room.Value.door_left)
            {
                // Room with all doors
                GameObject new_room = Instantiate(room_all_doors, position, Quaternion.identity);
                Room room_script = new_room.GetComponent<Room>();
                room_script.SetDoors(room.Value.door_top, room.Value.door_bottom, room.Value.door_right, room.Value.door_left, room.Value.boss_door_top, room.Value.boss_door_bottom, room.Value.boss_door_right, room.Value.boss_door_left);
                room_script.SetRoomInfo(room.Value.grid_position, room.Value.room_type, room.Value.wave_type_attatched);
            }
            else if (room.Value.door_top && room.Value.door_bottom && room.Value.door_left && !room.Value.door_right)
            {
                // Room with top, bottom, and left doors
                GameObject new_room = Instantiate(room_top_bottom_left, position, Quaternion.identity);
                Room room_script = new_room.GetComponent<Room>();
                room_script.SetDoors(room.Value.door_top, room.Value.door_bottom, room.Value.door_right, room.Value.door_left, room.Value.boss_door_top, room.Value.boss_door_bottom, room.Value.boss_door_right, room.Value.boss_door_left);
                room_script.SetRoomInfo(room.Value.grid_position, room.Value.room_type, room.Value.wave_type_attatched);
            }
            else if (room.Value.door_top && room.Value.door_left && room.Value.door_right && !room.Value.door_bottom)
            {
                // Room with top, left, and right doors
                GameObject new_room = Instantiate(room_top_left_right, position, Quaternion.identity);
                Room room_script = new_room.GetComponent<Room>();
                room_script.SetDoors(room.Value.door_top, room.Value.door_bottom, room.Value.door_right, room.Value.door_left, room.Value.boss_door_top, room.Value.boss_door_bottom, room.Value.boss_door_right, room.Value.boss_door_left);
                room_script.SetRoomInfo(room.Value.grid_position, room.Value.room_type, room.Value.wave_type_attatched);
            }
            else if (!room.Value.door_top && room.Value.door_bottom && room.Value.door_left && room.Value.door_right)
            {
                // Room with bottom, left, and right doors
                GameObject new_room = Instantiate(room_bottom_left_right, position, Quaternion.identity);
                Room room_script = new_room.GetComponent<Room>();
                room_script.SetDoors(room.Value.door_top, room.Value.door_bottom, room.Value.door_right, room.Value.door_left, room.Value.boss_door_top, room.Value.boss_door_bottom, room.Value.boss_door_right, room.Value.boss_door_left);
                room_script.SetRoomInfo(room.Value.grid_position, room.Value.room_type, room.Value.wave_type_attatched);
            }
        }
    }
    Vector2 NewPosition()
    {
        int x = 0, y = 0;
        Vector2 checkingPos = Vector2.zero;
        do
        {                                                     
            int index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1));
            x = (int)takenPositions[index].x;
            y = (int)takenPositions[index].y;
            bool UpDown = (Random.value < 0.5f);
            bool positive = (Random.value < 0.5f);
            if (UpDown)
            {
                if (positive)
                {
                    y += 1;
                }
                else
                {
                    y -= 1;
                }
            }
            else
            {
                if (positive)
                {
                    x += 1;
                }
                else
                {
                    x -= 1;
                }
            }
            checkingPos = new Vector2(x, y);
        } while (takenPositions.Contains(checkingPos) || x >= gridSizeX || x < -gridSizeX || y >= gridSizeY || y < -gridSizeY); //make sure the position is valid
        return checkingPos;
    }

    Vector2 SelectiveNewPosition()
    {
        int index = 0;
        int x = 0, y = 0;
        Vector2 checkingPos = Vector2.zero;
        do
        {
            do
            {
                index = Mathf.RoundToInt(Random.value * (takenPositions.Count - 1));
            } while (NumberOfNeighbors(takenPositions[index], takenPositions) > 1);
            x = (int)takenPositions[index].x;
            y = (int)takenPositions[index].y;
            bool UpDown = (Random.value < 0.5f);
            bool positive = (Random.value < 0.5f);
            if (UpDown)
            {
                if (positive)
                {
                    y += 1;
                }
                else
                {
                    y -= 1;
                }
            }
            else
            {
                if (positive)
                {
                    x += 1;
                }
                else
                {
                    x -= 1;
                }
            }
            checkingPos = new Vector2(x, y);
        } while (takenPositions.Contains(checkingPos) || x >= gridSizeX || x < -gridSizeX || y >= gridSizeY || y < -gridSizeY);
        
        return checkingPos;
    }

    int NumberOfNeighbors(Vector2 checkingPos, List<Vector2> usedPositions)
    {
        int ret = 0;
        if (usedPositions.Contains(checkingPos + Vector2.right))
        {
            ret++;
        }
        if (usedPositions.Contains(checkingPos + Vector2.left))
        {
            ret++;
        }
        if (usedPositions.Contains(checkingPos + Vector2.up))
        {
            ret++;
        }
        if (usedPositions.Contains(checkingPos + Vector2.down))
        {
            ret++;
        }
        return ret;
    }

    private void SetRoomDoors()
    {
        var keys = gridDictionary.Keys.ToList();
        foreach (var key in keys)
        {
            Room room = gridDictionary[key];

            // Update door statuses
            if (gridDictionary.ContainsKey(room.grid_position + Vector2.right))
            {
                room.door_right = true;
                if(gridDictionary[room.grid_position + Vector2.right].room_type == 2)
                {
                    room.boss_door_right = true;
                }
            }
            if (gridDictionary.ContainsKey(room.grid_position + Vector2.left))
            {
                room.door_left = true;
                if (gridDictionary[room.grid_position + Vector2.left].room_type == 2)
                {
                    room.boss_door_left = true;
                }
            }
            if (gridDictionary.ContainsKey(room.grid_position + Vector2.up))
            {
                room.door_top = true;
                if (gridDictionary[room.grid_position + Vector2.up].room_type == 2)
                {
                    room.boss_door_top = true;
                }
            }
            if (gridDictionary.ContainsKey(room.grid_position + Vector2.down))
            {
                room.door_bottom = true;
                if (gridDictionary[room.grid_position + Vector2.down].room_type == 2)
                {
                    room.boss_door_bottom = true;
                }
            }

            // Update the dictionary with the modified Room
            gridDictionary[key] = room;
        }

    }

    private void SpawnPlayer()
    {
        GameManager.player = Instantiate(player, new Vector2(0, 0), Quaternion.identity);
        GunController player_gun = GameManager.player.GetComponentInChildren<GunController>();
        if(GameManager.chosen_weapon == 1)
        {
            player_gun.equipped_gun = pistol;
        }
        else if(GameManager.chosen_weapon == 2)
        {
            player_gun.equipped_gun = shotgun;
        }
        else if (GameManager.chosen_weapon == 3)
        {
            player_gun.equipped_gun = plasma_gun;
        }
        else if (GameManager.chosen_weapon == 4)
        {
            player_gun.equipped_gun = grenade_launcher;
        }
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
