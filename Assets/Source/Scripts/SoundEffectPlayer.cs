using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectPlayer : MonoBehaviour
{
    [SerializeField] private AudioClip[] sound_effects;
    private AudioSource audio_source;

    private void Start()
    {
        audio_source = GetComponent<AudioSource>();
    }

    public void PlayButton()
    {
        audio_source.clip = sound_effects[0];
        audio_source.Play();
    }

    public void PlayEnemyDeath()
    {
        audio_source.clip = sound_effects[2];
        audio_source.Play();
    }

    public void PlayKey()
    {
        audio_source.clip = sound_effects[1];
        audio_source.Play();
    }

    public void PlayBossDeath()
    {
        audio_source.clip = sound_effects[3];
        audio_source.Play();
    }

    public void PlayPlayerHurt()
    {
        audio_source.clip = sound_effects[4];
        audio_source.Play();
    }

    public void PlayPlayerDeath()
    {
        audio_source.clip = sound_effects[5];
        audio_source.Play();
    }
}
