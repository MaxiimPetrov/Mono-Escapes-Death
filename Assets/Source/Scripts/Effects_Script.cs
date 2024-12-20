using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;

public class Effects_Script : MonoBehaviour
{
    private ObjectPool<GameObject> pool;
    [SerializeField] private bool destroy_on_commpletion = false;
    [SerializeField] private string death_scene;
    private SoundEffectPlayer sound_effect_player;

    private void Start()
    {
        sound_effect_player = GameObject.Find("SoundEffectPlayer").GetComponent<SoundEffectPlayer>();
    }

    private void RemoveEffect()
    {
        if(this.transform.name.Contains("Poof") && !destroy_on_commpletion)
        {
            pool.Release(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public void SetPool(ObjectPool<GameObject> _pool)
    {
        pool = _pool;
    }

    private void RemovePlayerDeathEffect()
    {

        Destroy(this.gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    private void PlayBossDeathSound()
    {
        sound_effect_player.PlayBossDeath();
    }
}
