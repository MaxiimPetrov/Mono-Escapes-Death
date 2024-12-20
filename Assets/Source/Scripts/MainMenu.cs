using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    private Image weapon_select_box;
    private TextMeshProUGUI weapon_select_text;
    private Button pistol_button;
    private Image pistol_button_image;
    private Image pistol_button_sprite;
    private Button shotgun_button;
    private Image shotgun_button_image;
    private Image shotgun_button_sprite;
    private Button plasma_button;
    private Image plasma_button_image;
    private Image plasma_button_sprite;
    private Button grenade_button;
    private Image grenade_button_image;
    private Image grenade_button_sprite;
    private TextMeshProUGUI pistol_text;
    private TextMeshProUGUI shotgun_text;
    private TextMeshProUGUI plasma_text;
    private TextMeshProUGUI grenade_text;
    private Button confirm_button;
    private Image confirm_button_image;
    private TextMeshProUGUI confirm_button_text;
    private Button go_back_button;
    private Image go_back_button_image;
    private TextMeshProUGUI go_back_button_text;
    private TextMeshProUGUI must_choose_text;
    private int temp_int;
    private SoundEffectPlayer player;

    private void Start()
    {
        weapon_select_box = GameObject.Find("WeaponSelectBox").GetComponent<Image>();
        weapon_select_text = GameObject.Find("WeaponSelectText").GetComponent<TextMeshProUGUI>();
        pistol_button = GameObject.Find("PistolButton").GetComponent<Button>();
        pistol_button_image = GameObject.Find("PistolButton").GetComponent<Image>();
        pistol_button_sprite = GameObject.Find("PistolImage").GetComponent<Image>();
        shotgun_button = GameObject.Find("ShotgunButton").GetComponent<Button>();
        shotgun_button_image = GameObject.Find("ShotgunButton").GetComponent<Image>();
        shotgun_button_sprite = GameObject.Find("ShotgunImage").GetComponent<Image>();
        plasma_button = GameObject.Find("PlasmaButton").GetComponent<Button>();
        plasma_button_image = GameObject.Find("PlasmaButton").GetComponent<Image>();
        plasma_button_sprite = GameObject.Find("PlasmaImage").GetComponent<Image>();
        grenade_button = GameObject.Find("GrenadeButton").GetComponent<Button>();
        grenade_button_image = GameObject.Find("GrenadeButton").GetComponent<Image>();
        grenade_button_sprite = GameObject.Find("GrenadeImage").GetComponent<Image>();
        pistol_text = GameObject.Find("PistolText").GetComponent<TextMeshProUGUI>();
        shotgun_text = GameObject.Find("ShotgunText").GetComponent<TextMeshProUGUI>();
        plasma_text = GameObject.Find("PlasmaText").GetComponent<TextMeshProUGUI>();
        grenade_text = GameObject.Find("GrenadeText").GetComponent<TextMeshProUGUI>();
        confirm_button = GameObject.Find("ConfirmButton").GetComponent<Button>();
        confirm_button_image = GameObject.Find("ConfirmButton").GetComponent<Image>();
        confirm_button_text = GameObject.Find("ConfirmButton").GetComponentInChildren<TextMeshProUGUI>();
        go_back_button = GameObject.Find("GoBackButton").GetComponent<Button>();
        go_back_button_image = GameObject.Find("GoBackButton").GetComponent<Image>();
        go_back_button_text = GameObject.Find("GoBackButton").GetComponentInChildren<TextMeshProUGUI>();
        must_choose_text = GameObject.Find("MustChooseText").GetComponent<TextMeshProUGUI>();
        player = GameObject.Find("SoundEffectPlayer").GetComponent<SoundEffectPlayer>();
    }

    public void SelectWeaponPopUp()
    {
        player.PlayButton();
        weapon_select_box.enabled = true;
        weapon_select_text.enabled = true;
        pistol_button.enabled = true;
        pistol_button_image.enabled = true;
        shotgun_button.enabled = true;
        shotgun_button_image.enabled = true;
        plasma_button.enabled = true;
        plasma_button_image.enabled = true;
        grenade_button.enabled = true;
        grenade_button_image.enabled = true;
        pistol_text.enabled = true;
        shotgun_text.enabled = true;
        plasma_text.enabled = true;
        grenade_text.enabled = true;
        confirm_button.enabled = true;
        confirm_button_image.enabled = true;
        confirm_button_text.enabled = true;
        go_back_button.enabled = true;
        go_back_button_image.enabled = true;
        go_back_button_text.enabled = true;
        pistol_button_sprite.enabled = true;
        shotgun_button_sprite.enabled= true;
        plasma_button_sprite.enabled = true;
        grenade_button_sprite.enabled = true;
    }
    public void SelectWeaponPopDown()
    {
        player.PlayButton();
        weapon_select_box.enabled = false;
        weapon_select_text.enabled = false;
        pistol_button.enabled = false;
        pistol_button_image.enabled = false;
        shotgun_button.enabled = false;
        shotgun_button_image.enabled = false;
        plasma_button.enabled = false;
        plasma_button_image.enabled = false;
        grenade_button.enabled = false;
        grenade_button_image.enabled = false;
        pistol_text.enabled = false;
        shotgun_text.enabled = false;
        plasma_text.enabled = false;
        grenade_text.enabled = false;
        confirm_button.enabled = false;
        confirm_button_image.enabled = false;
        confirm_button_text.enabled = false;
        go_back_button.enabled = false;
        go_back_button_image.enabled = false;
        go_back_button_text.enabled = false;
        pistol_button_sprite.enabled = false;
        shotgun_button_sprite.enabled = false;
        plasma_button_sprite.enabled = false;
        grenade_button_sprite.enabled = false;
        must_choose_text.enabled = false;
        pistol_button.interactable = true;
        shotgun_button.interactable = true;
        plasma_button.interactable = true;
        grenade_button.interactable = true;
        GameManager.chosen_weapon = 0;
    }

    public void ExitGame()
    {
        player.PlayButton();
        Application.Quit();
    }

    public void ChoosePistol()
    {
        player.PlayButton();
        GameManager.chosen_weapon = 1;
        pistol_button.interactable = false;
        shotgun_button.interactable = true;
        plasma_button.interactable = true;
        grenade_button.interactable = true;
        must_choose_text.enabled = false;
    }

    public void ChooseShotgun()
    {
        player.PlayButton();
        GameManager.chosen_weapon = 2;
        pistol_button.interactable = true;
        shotgun_button.interactable = false;
        plasma_button.interactable = true;
        grenade_button.interactable = true;
        must_choose_text.enabled = false;
    }

    public void ChoosePlasma()
    {
        player.PlayButton();
        GameManager.chosen_weapon = 3;
        pistol_button.interactable = true;
        shotgun_button.interactable = true;
        plasma_button.interactable = false;
        grenade_button.interactable = true;
        must_choose_text.enabled = false;
    }

    public void ChooseGrenade()
    {
        player.PlayButton();
        GameManager.chosen_weapon = 4;
        pistol_button.interactable = true;
        shotgun_button.interactable = true;
        plasma_button.interactable = true;
        grenade_button.interactable = false;
        must_choose_text.enabled = false;
    }

    public void ContinueToGame()
    {
        player.PlayButton();
        confirm_button.interactable = false;
        confirm_button.interactable = true;

        if (GameManager.chosen_weapon == 0)
        {
            must_choose_text.enabled = true;
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
