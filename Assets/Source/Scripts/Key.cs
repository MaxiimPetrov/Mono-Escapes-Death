using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : MonoBehaviour
{
    [SerializeField] private int key_type; // 1 = red 2 = green 3 = yellow 4 = purple
    private BoxCollider2D body_hitbox;
    private SoundEffectPlayer sound_effect_player;

    private void Awake()
    {
        sound_effect_player = GameObject.Find("SoundEffectPlayer").GetComponent<SoundEffectPlayer>();
        BoxCollider2D[] colliders = GetComponents<BoxCollider2D>();
        body_hitbox = colliders[0];
        body_hitbox.enabled = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(key_type == 1)
        {
            GameManager.red_key_collected = true;
            KeyUI.SetRedKeyUI();
        }
        if (key_type == 2)
        {
            GameManager.green_key_collected= true;
            KeyUI.SetGreenKeyUI();
        }
        if(key_type == 3)
        {
            GameManager.yellow_key_collected = true;
            KeyUI.SetYellowKeyUI();
        }
        if(key_type == 4)
        {
            GameManager.purple_key_collected = true;
            KeyUI.SetPurpleKeyUI();
        }

        if (GameManager.waves_completed != 14 && GameManager.waves_completed != 28)
        {
            GameManager.wave_active = false;
        }

        PlayerController player_script = collision.GetComponent<PlayerController>();
        LevelUp(player_script);
        KeyUI.ShowLevelUpText();
        sound_effect_player.PlayKey();
        Destroy(this.gameObject);
    }

    private void EnableHitbox()
    {
        body_hitbox.enabled = true;
    }

    private void LevelUp(PlayerController player_script)
    {
        GameManager.player_level += 1;
        player_script.move_speed += 0.05f;
    }
}
