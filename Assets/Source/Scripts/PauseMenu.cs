using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public static bool game_paused = false;
    private RawImage pause_menu;
    private TextMeshProUGUI paused_text;
    private Image resume_button;
    private TextMeshProUGUI resume_button_text;
    private Button resume_button_functionality;
    private Image return_button;
    private TextMeshProUGUI return_button_text;
    private Button return_button_functionality;
    [SerializeField] private Texture2D cursor_texture_paused;
    [SerializeField] private Texture2D cursor_texture_not_paused;
    private Vector2 cursor_hotspot;
    private SoundEffectPlayer sound_effect_player;

    void Start()
    {
        sound_effect_player = GameObject.Find("SoundEffectPlayer").GetComponent<SoundEffectPlayer>();
        pause_menu = GetComponent<RawImage>();
        resume_button = GameObject.Find("ResumeButton").GetComponent<Image>();
        resume_button_text = GameObject.Find("ResumeButton").GetComponentInChildren<TextMeshProUGUI>();
        resume_button_functionality = GameObject.Find("ResumeButton").GetComponent<Button>();
        return_button = GameObject.Find("ReturnButton").GetComponent<Image>();
        return_button_text = GameObject.Find("ReturnButton").GetComponentInChildren<TextMeshProUGUI>();
        return_button_functionality = GameObject.Find("ReturnButton").GetComponent<Button>();
        paused_text = GameObject.Find("PausedText").GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            game_paused = !game_paused;
            sound_effect_player.PlayButton();
            if (game_paused)
            {
                Time.timeScale = 0.0f;
                PlayerController.animator.SetBool("IsWalking", false);
            }
            else
            {
                if (PlayerController.move_direction != Vector2.zero)
                {
                    PlayerController.animator.SetBool("IsWalking", true);
                }
                else
                {
                    PlayerController.animator.SetBool("IsWalking", false);
                }
                Time.timeScale = 1.0f;

            }

            if (game_paused)
            {
                cursor_hotspot = new Vector2(cursor_texture_paused.width / 2, cursor_texture_paused.height / 2);
                Cursor.SetCursor(cursor_texture_paused, cursor_hotspot, CursorMode.Auto);
            }
            else
            {
                cursor_hotspot = new Vector2(cursor_texture_not_paused.width / 2, cursor_texture_not_paused.height / 2);
                Cursor.SetCursor(cursor_texture_not_paused, cursor_hotspot, CursorMode.Auto);
            }

            pause_menu.enabled = game_paused;
            paused_text.enabled = game_paused;
            resume_button.enabled = game_paused;
            resume_button_text.enabled = game_paused;
            resume_button_functionality.enabled = game_paused;
            return_button.enabled = game_paused;
            return_button_text.enabled = game_paused;
            return_button_functionality.enabled = game_paused;
        }
    }

    public void OnResumePressed()
    {
        game_paused = !game_paused;
        sound_effect_player.PlayButton();
        if (game_paused)
        {
            Time.timeScale = 0.0f;
            PlayerController.animator.SetBool("IsWalking", false);
        }
        else
        {
            if (PlayerController.move_direction != Vector2.zero)
            {
                PlayerController.animator.SetBool("IsWalking", true);
            }
            else
            {
                PlayerController.animator.SetBool("IsWalking", false);
            }
            Time.timeScale = 1.0f;

        }

        if (game_paused)
        {
            cursor_hotspot = new Vector2(cursor_texture_paused.width / 2, cursor_texture_paused.height / 2);
            Cursor.SetCursor(cursor_texture_paused, cursor_hotspot, CursorMode.Auto);
        }
        else
        {
            cursor_hotspot = new Vector2(cursor_texture_not_paused.width / 2, cursor_texture_not_paused.height / 2);
            Cursor.SetCursor(cursor_texture_not_paused, cursor_hotspot, CursorMode.Auto);
        }

        pause_menu.enabled = game_paused;
        paused_text.enabled = game_paused;
        resume_button.enabled = game_paused;
        resume_button_text.enabled = game_paused;
        resume_button_functionality.enabled = game_paused;
        return_button.enabled = game_paused;
        return_button_text.enabled = game_paused;
        return_button_functionality.enabled = game_paused;
        resume_button_functionality.interactable = false;
        resume_button_functionality.interactable = true;
    }

    public void OnReturnToMainMenuPressed()
    {
        sound_effect_player.PlayButton();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        GameManager.ResetGameValues();
    }

    public void OnReturnToMainMenuPressedFromDeath()
    {
        sound_effect_player.PlayButton();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
        GameManager.ResetGameValues();
    }

    public void OnReturnToMainMenuPressedFromWin()
    {
        sound_effect_player.PlayButton();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 3);
        GameManager.ResetGameValues();
    }
}
