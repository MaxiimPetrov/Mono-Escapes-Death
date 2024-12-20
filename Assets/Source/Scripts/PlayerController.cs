using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static Vector2 move_direction;
    private Rigidbody2D rb;
    public float move_speed;
    public static Animator animator;
    public int health;
    [SerializeField] private Texture2D cursor_texture;
    private Vector2 cursor_hotspot;
    private bool can_move = true;
    public float knockback_force;
    private float knockback_timer;
    [SerializeField] private float knockback_timer_length = 0.3f;
    private bool knockback_applied = false;
    public bool eating = false;
    private float additional_i_timer;
    private float additional_i_timer_length = 0.2f;
    private bool start_additional_i_timer = false;
    public static Vector3 player_position;
    private BoxCollider2D[] colliders;
    [SerializeField] private GameObject death_square;
    [SerializeField] private GameObject death_effect;
    private SoundEffectPlayer sound_effect_player;

    void Start()
    {
        knockback_timer = knockback_timer_length;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        cursor_hotspot = new Vector2(cursor_texture.width / 2, cursor_texture.height / 2);
        Cursor.SetCursor(cursor_texture, cursor_hotspot, CursorMode.Auto);
        colliders = this.gameObject.GetComponentsInChildren<BoxCollider2D>();
        sound_effect_player = GameObject.Find("SoundEffectPlayer").GetComponent<SoundEffectPlayer>();
    }

    private void Update()
    {
        if (!PauseMenu.game_paused)
        {
            player_position = this.transform.position;
            if (!can_move)
            {
                knockback_timer -= Time.deltaTime;
                if (knockback_timer <= 0 && !eating)
                {
                    can_move = true;
                    animator.SetBool("IsHurt", false);
                    knockback_applied = false;
                    additional_i_timer = additional_i_timer_length;
                    start_additional_i_timer = true;
                }
            }

            if(start_additional_i_timer)
            {
                additional_i_timer -= Time.deltaTime;
                if(additional_i_timer <= 0)
                {
                    colliders[1].enabled = true;
                    start_additional_i_timer = false;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (can_move)
        {
            rb.MovePosition(rb.position + move_direction * move_speed * Time.fixedDeltaTime);
        }
    }
    void OnMove(InputValue movementValue)
    {
        if(!PauseMenu.game_paused)
        {
            move_direction = movementValue.Get<Vector2>();
            if (move_direction.x != 0 || move_direction.y != 0)
            {
                animator.SetFloat("X", move_direction.x);
                animator.SetFloat("Y", move_direction.y);
                animator.SetBool("IsWalking", true);
            }
            else
            {
                animator.SetBool("IsWalking", false);
            }
        }
        else
        {
            move_direction = new Vector2(0, 0);
            animator.SetBool("IsWalking", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Contains("Banana"))
        {
            animator.SetBool("Eating", true);
            eating = true;
            can_move = false;
        }
        else if (collision.gameObject.name.Contains("Key") || collision.gameObject.name.Contains("Chaser_2") || collision.gameObject.name.Contains("Room") || collision.gameObject.name.Contains("BossChaser") || eating) //|| (GameManager.num_enemies_active == 0 && !collision.transform.parent.name.Contains("Skull"))
        {
            return;
        }
        else
        {
            base_projectile bullet = collision.GetComponent<base_projectile>();
            Base_Enemy enemy = collision.GetComponent<Base_Enemy>();

            if (bullet != null)
            {
                health -= bullet.damage;
                Health.SetHealthUI(health);
            }
            if (enemy != null)
            {
                health -= enemy.collision_damage;
                Health.SetHealthUI(health);
            }
            if (health <= 0)
            {
                MusicManager.StopMusic();
                Health.DisableUI();
                Instantiate(death_square, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
                Instantiate(death_effect, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
                sound_effect_player.PlayPlayerDeath();
                Destroy(this.gameObject);
            }
            else if(!knockback_applied)
            {
                sound_effect_player.PlayPlayerHurt();
                knockback_timer = knockback_timer_length;
                animator.SetBool("IsHurt", true);
                Vector2 direction = (this.transform.position - collision.transform.position).normalized;
                Vector2 knockback = knockback_force * direction;
                rb.AddForce(knockback, ForceMode2D.Impulse);
                can_move = false;
                knockback_applied = true;
                colliders[1].enabled = false;
            }
        }
    }

    private void FinishEating()
    {
        animator.SetBool("Eating", false);
        eating = false;
        can_move = true;
    }
}
