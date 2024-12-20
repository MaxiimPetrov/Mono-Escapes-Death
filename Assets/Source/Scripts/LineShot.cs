using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class LineShot : MonoBehaviour
{
    private SpriteRenderer red_square_sprite;
    private RotatingSpawner[] rotating_spawners;
    private float indication_timer;
    private float indication_timer_length = 0.3f;
    private bool indication_timer_started = false;
    private int indications = 0;
    private float indication_active_timer;
    private float indication_timer_active_length = 0.3f;
    private bool indication_active_timer_started = false;
    private float spawners_active_timer;
    private float spawners_active_timer_length = 1f;
    private bool spawners_active_timer_started = false;

    void Start()
    {
        red_square_sprite = GetComponentInChildren<SpriteRenderer>();
        rotating_spawners = GetComponentsInChildren<RotatingSpawner>();
        indication_timer = indication_timer_length;
        indication_timer_started = true;
    }

    void Update()
    {
        if(indication_timer_started)
        {
            indication_timer -= Time.deltaTime;
            if(indication_timer < 0)
            {
                if(indications == 3)
                {
                    for(int i = 0; i < rotating_spawners.Length; i++)
                    {
                        rotating_spawners[i].active = true;
                    }

                    indication_timer_started = false;
                    spawners_active_timer = spawners_active_timer_length;
                    spawners_active_timer_started = true;
                }
                else
                {
                    red_square_sprite = GetComponentInChildren<SpriteRenderer>();
                    red_square_sprite.enabled = true;
                    indication_active_timer = indication_timer_active_length;
                    indication_active_timer_started = true;
                    indication_timer_started = false;
                }
                
            }
        }

        if(indication_active_timer_started)
        {
            indication_active_timer -= Time.deltaTime;
            if(indication_active_timer < 0)
            {
                red_square_sprite = GetComponentInChildren<SpriteRenderer>();
                red_square_sprite.enabled = false;
                indication_timer = indication_timer_length;
                indication_timer_started = true;
                indication_active_timer_started = false;
                indications++;
            }
        }

        if(spawners_active_timer_started)
        {
            spawners_active_timer -= Time .deltaTime;
            if(spawners_active_timer < 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
