using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    private static Image[] heart_images = new Image[5];
    private int hearts_count = 0;
    private static List<GameObject> UI_elements = new List<GameObject>();

    private void Start()
    {
        FindAllHeartObjects();
        UI_elements.Clear();
        foreach (Transform child in this.transform)
        {
            UI_elements.Add(child.gameObject);
        }
    }

    private void FindAllHeartObjects()
    {
        GameObject[] all_objects = FindObjectsOfType<GameObject>();
        for (int i = 0; i < all_objects.Length; i++)
        {
            if (all_objects[i].name.Contains("Heart"))
            {
                if(all_objects[i].name.Contains("1"))
                {
                    heart_images[4] = all_objects[i].GetComponent<Image>();
                }
                else if(all_objects[i].name.Contains("2"))
                {
                    heart_images[3] = all_objects[i].GetComponent<Image>();
                }
                else if (all_objects[i].name.Contains("3"))
                {
                    heart_images[2] = all_objects[i].GetComponent<Image>();
                }
                else if (all_objects[i].name.Contains("4"))
                {
                    heart_images[1] = all_objects[i].GetComponent<Image>();
                }
                else if (all_objects[i].name.Contains("5"))
                {
                    heart_images[0] = all_objects[i].GetComponent<Image>();
                }
            }
        }
    }

    public static void SetHealthUI(int current_player_health)
    {
        if (current_player_health == 0)
        {
            for (int i = 0; i < heart_images.Length; i++)
            {
                heart_images[i].enabled = false;
            }
        }
        else if (current_player_health == 1)
        {
            heart_images[4].color = new Color(105 / 255f, 50 / 255f, 50 / 255f);
        }
        else if (current_player_health == 2)
        {
            heart_images[4].color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
            heart_images[3].enabled = false;
        }
        else if (current_player_health == 3)
        {
            heart_images[3].color = new Color(105 / 255f, 50 / 255f, 50 / 255f);
        }
        else if (current_player_health == 4)
        {
            heart_images[3].color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
            heart_images[2].enabled = false;
        }
        else if (current_player_health == 5)
        {
            heart_images[2].color = new Color(105 / 255f, 50 / 255f, 50 / 255f);
        }
        else if (current_player_health == 6)
        {
            heart_images[2].color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
            heart_images[1].enabled = false;
        }
        else if (current_player_health == 7)
        {
            heart_images[1].color = new Color(105 / 255f, 50 / 255f, 50 / 255f);
        }
        else if (current_player_health == 8)
        {
            heart_images[1].color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
            heart_images[0].enabled = false;
        }
        else if (current_player_health == 9)
        {
            heart_images[0].color = new Color(105 / 255f, 50 / 255f, 50 / 255f);

        }
        else if (current_player_health == 10)
        {
            for (int i = 0; i < heart_images.Length; i++)
            {
                heart_images[i].color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
                heart_images[i].enabled = true;
            }
        }
    }

    public static void DisableUI()
    {
        for(int i = 0; i < UI_elements.Count; i++)
        {
            UI_elements[i].SetActive(false);
        }

    }

    public static void EnableUI()
    {
        for (int i = 0; i < UI_elements.Count; i++)
        {
            UI_elements[i].SetActive(true);
        }
    }
}
