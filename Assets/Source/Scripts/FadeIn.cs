using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeInScript : MonoBehaviour
{
    private SpriteRenderer sr;
    private float timer_duration = 3f;
    private Animator animator;
    private BoxCollider2D box_collider;
    private float start_timer = 6f;
    
    private void Awake()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        box_collider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(!PauseMenu.game_paused)
        {
            if (start_timer > 0)
            {
                start_timer -= Time.deltaTime;
                if (start_timer < 0)
                {
                    StartCoroutine(FadeIn());
                }
            }
        }
        
    }

    private IEnumerator FadeIn()
    {
        float timer = 0.0f;

        while (timer < timer_duration)
        {
            float alpha = Mathf.Lerp(0.0f, 1.0f, timer / timer_duration);
            Color new_color = sr.color;
            new_color.a = alpha;
            sr.color = new_color;
            timer += Time.deltaTime;
            yield return null;
        }

        box_collider.enabled = true;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        animator.SetBool("MonoGoesToBed", true);
        Destroy(collision.transform.gameObject);
    }

    public void DestroyBed() 
    {
        StartCoroutine(FadeOut());
    }

    private IEnumerator FadeOut()
    {
        float timer = 0.0f;

        while (timer < timer_duration)
        {
            float alpha = Mathf.Lerp(1.0f, 0.0f, timer / timer_duration);
            Color new_color = sr.color;
            new_color.a = alpha;
            sr.color = new_color;
            timer += Time.deltaTime;
            yield return null;
        }
        Destroy(this.gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
    }
}
