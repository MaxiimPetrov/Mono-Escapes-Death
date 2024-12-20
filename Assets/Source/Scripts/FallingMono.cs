using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingMono : MonoBehaviour
{
    private float timer = 5f;
    private bool disable_ui;
   
    void Start()
    {
        PauseMenu.game_paused = true;
    }
    
    void Update()
    {
        if (!disable_ui)
        {
            Health.DisableUI();
            disable_ui = true;
        }
        timer -= Time.deltaTime;
        
        if (timer < 0)
        {
            PauseMenu.game_paused = false;
            Health.EnableUI();
            Destroy(this.gameObject);
        }
    }
}
