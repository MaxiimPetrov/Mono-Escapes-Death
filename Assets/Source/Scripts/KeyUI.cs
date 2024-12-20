using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class KeyUI : MonoBehaviour
{
    private static Image purple_key_ui;
    private static Image red_key_ui;
    private static Image green_key_ui;
    private static Image yellow_key_ui;
    private static TextMeshProUGUI need_more_keys_text;
    private static TextMeshProUGUI level_up_text;
    private static float level_up_text_timer = 2f;
    private static float level_up_text_timer_length = 2f;
    private static bool level_up_timer_started;

    void Start()
    {
        purple_key_ui = GameObject.Find("PurpleKeyCollected").GetComponent<Image>();
        red_key_ui = GameObject.Find("RedKeyCollected").GetComponent<Image>();
        green_key_ui = GameObject.Find("GreenKeyCollected").GetComponent<Image>();
        yellow_key_ui = GameObject.Find("YellowKeyCollected").GetComponent<Image>();
        need_more_keys_text = GameObject.Find("NeedMoreKeysText").GetComponent<TextMeshProUGUI>();
        level_up_text = GameObject.Find("LevelUpText").GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        if(level_up_timer_started)
        {
            level_up_text_timer -= Time.deltaTime;
            if(level_up_text_timer < 0)
            {
                level_up_text.enabled = false;
                level_up_timer_started = false;
            }
        }
    }

    public static void SetPurpleKeyUI()
    {
        purple_key_ui.enabled = true;
    }

    public static void SetRedKeyUI()
    {
        red_key_ui.enabled = true;
    }
    public static void SetGreenKeyUI()
    {
        green_key_ui.enabled = true;
    }
    public static void SetYellowKeyUI()
    {
        yellow_key_ui.enabled = true;
    }

    public static void ShowKeysText()
    {
        need_more_keys_text.enabled = true;
    }

    public static void HideKeysText()
    {
        need_more_keys_text.enabled = false;
    }

    public static void ShowLevelUpText()
    {
        level_up_text.enabled = true;
        level_up_text_timer = level_up_text_timer_length;
        level_up_timer_started = true;
    }
}
