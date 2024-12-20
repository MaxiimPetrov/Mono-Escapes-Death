using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip[] songs;
    public static AudioSource audio_source;
    public AudioClip boss_song;
    private bool boss_song_set;
    private bool song_played;
    private float timer = 5f;

    void Start()
    {
        audio_source = GetComponent<AudioSource>();
        if (songs.Length > 0)
        {
            int random = Random.Range(0, songs.Length);
            audio_source.clip = songs[random];
        }
    }

    private void Update()
    {
        if(timer > 0)
        {
            timer -= Time.deltaTime;
            if(timer < 0)
            {
                audio_source.Play();
            }
        }

        if(GameManager.boss_spawned) {
            if(!boss_song_set)
            {
                audio_source.Stop(); 
                audio_source.clip = boss_song; 
                audio_source.Play(); 
                boss_song_set = true;
            }
        }

        if(GameManager.boss_dead)
        {
            audio_source.Stop();
        }

        if (PauseMenu.game_paused)
        {
            audio_source.Pause();
            song_played = false;
        }
        else
        {
            if (!song_played)
            {
                audio_source.Play();
                song_played = true;
            }
        }
    }

    public static void StopMusic()
    {
        audio_source.Stop();
    }
}
