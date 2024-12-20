using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class Death : Base_Enemy
{
    private Animator animator;
    private SpriteRenderer sprite_renderer;
    private Rigidbody2D rb;
    private float movement_speed = 2f;

    [SerializeField] TrackerSpawner tracker_spawner;
    [SerializeField] RotatingSpawner rotating_spawner;
    [SerializeField] RandomSpawner random_spawner;
    [SerializeField] base_projectile boss_projectile;
    [SerializeField] GameObject vertical_line_shot_pattern_1;
    [SerializeField] GameObject horizontal_line_shot_pattern_1;
    [SerializeField] GameObject vertical_line_shot_pattern_2;
    [SerializeField] GameObject horizontal_line_shot_pattern_2;
    [SerializeField] GameObject boss_death_sqaure;
    [SerializeField] banana banana;
    [SerializeField] GameObject bed;

    private bool bananabanana = false;

    private GameObject current_vertical_pattern_1;
    private GameObject current_horizontal_pattern_1;
    private GameObject current_vertical_pattern_2;
    private GameObject current_horizontal_pattern_2;

    [SerializeField] GameObject boss_red_square;
    private GameObject boss_red_square_object;

    private TrackerSpawner tracker_spawner_object;
    private RotatingSpawner rotating_spawner_object;
    private RotatingSpawner rotating_spawner_object_2;
    private RandomSpawner random_spawner_object;

    [SerializeField] RotatingSpawner boss_tracker_spawner;
    private RotatingSpawner boss_tracker_spawner_object;
    private bool rotate_phase_active = false;
    private bool rotate_phase_part_two_transition_timer_started;
    private float rotate_phase_part_two_transition_timer;
    private float rotate_phase_part_two_transition_timer_length = 12f;
    private bool rotate_phase_part_two_started = false;

    private float spawner_phase_timer;
    private float spawner_phase_timer_length = 15f;
    private bool spawner_phase_timer_started = false;

    private float transitional_timer;
    private float transitional_timer_length = 2f;
    private bool transitional_timer_started = false;

    private Vector2 starting_position;

    private int current_attack = 1; // 1 = spawner attack 2 = chasing

    CircleCollider2D slash_collider;

    private Vector3 static_player_position;
    private Vector2 static_move_direction;
    private bool slashing;
    private bool to_left;
    private int num_of_slashes = 0;
    private bool num_of_slashes_incremented = false;

    private Vector2 add_spawn_position_top_left;
    private Vector2 add_spawn_position_top_right;
    private Vector2 add_spawn_position_bottom_left;
    private Vector2 add_spawn_position_bottom_right;
    [SerializeField] Base_Enemy add_spawn;

    private CircleCollider2D[] circle_collider_2Ds;
    private CircleCollider2D body_circular_hitbox;

    private bool line_shots_phase_variation = false;

    private float line_shots_active_timer;
    private float line_shots_active_timer_length_1 = 4f;
    private float line_shots_active_timer_length_2 = 7f;
    private bool line_shots_active_timer_started = false;
    private bool first_set_line_shots_completed = false;
    private bool second_set_shot = false;
    private bool second_set_second_line_shot = false;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
        sprite_renderer = GetComponent<SpriteRenderer>();
        circle_collider_2Ds = GetComponents<CircleCollider2D>();
        slash_collider = circle_collider_2Ds[0];
        body_circular_hitbox = circle_collider_2Ds[1];
        rb = GetComponent<Rigidbody2D>();
        slash_collider.enabled = false;
        body_circular_hitbox.enabled = false;
        starting_position = new Vector2(this.transform.position.x, this.transform.position.y);
        add_spawn_position_top_left = new Vector2(this.transform.position.x - 1.1f, this.transform.position.y + 0.65f);
        add_spawn_position_top_right = new Vector2(this.transform.position.x + 1.1f, this.transform.position.y + 0.65f);
        add_spawn_position_bottom_left = new Vector2(this.transform.position.x - 1.1f, this.transform.position.y - 0.65f);
        add_spawn_position_bottom_right = new Vector2(this.transform.position.x + 1.1f, this.transform.position.y - 0.65f);
    }

    protected override void Update()
    {
        if(!PauseMenu.game_paused)
        {
            if (!slashing)
            {
                base.Update();
            }

            if (rotate_phase_active)
            {
                if (!rotate_phase_part_two_transition_timer_started && !rotate_phase_part_two_started)
                {
                    boss_tracker_spawner_object.rotation_speed += 0.1f;
                }

                if (boss_tracker_spawner_object.rotation_speed > 50 && !rotate_phase_part_two_transition_timer_started)
                {

                    rotate_phase_part_two_transition_timer = rotate_phase_part_two_transition_timer_length;
                    rotate_phase_part_two_transition_timer_started = true;
                }

                if (rotate_phase_part_two_transition_timer_started)
                {
                    rotate_phase_part_two_transition_timer -= Time.deltaTime;
                    if (rotate_phase_part_two_transition_timer < 0)
                    {
                        Destroy(boss_tracker_spawner_object.gameObject);
                        boss_tracker_spawner_object = Instantiate(boss_tracker_spawner, transform);
                        boss_tracker_spawner_object.GetComponent<BulletSpawner>().enemy_bullet = boss_projectile;
                        random_spawner_object = Instantiate(random_spawner, transform);
                        random_spawner_object.shot_cooldown = 0.05f;
                        random_spawner_object.GetComponent<BulletSpawner>().enemy_bullet = boss_projectile;
                        rotate_phase_part_two_transition_timer_started = false;
                        rotate_phase_part_two_started = true;
                    }

                }

            }

            if (transitional_timer_started)
            {
                transitional_timer -= Time.deltaTime;
                if (transitional_timer <= 0)
                {
                    if (current_attack == 1)
                    {
                        animator.SetBool("SpawnerPhase", true);

                    }
                    else if (current_attack == 2)
                    {
                        animator.SetBool("LeaveSlashing", false);
                        animator.SetBool("SlashStartup", false);
                        animator.SetBool("Slashing", false);
                        animator.SetBool("SlashStartup", true);

                    }
                    else if (current_attack == 3 || current_attack == 4 || current_attack == 5)
                    {
                        animator.SetBool("SpawnAdds", true);
                    }
                    else if (current_attack == 6)
                    {
                        animator.SetBool("RotatePhase", true);
                    }
                    else if (current_attack == 7)
                    {
                        LineShotsPhase();
                    }

                    transitional_timer_started = false;
                }
            }

            if (spawner_phase_timer_started)
            {
                spawner_phase_timer -= Time.deltaTime;
                if (spawner_phase_timer <= 0)
                {
                    EndSpawnerPhase();
                }
            }

            if (line_shots_active_timer_started)
            {
                line_shots_active_timer -= Time.deltaTime;

                if (second_set_shot)
                {
                    if (line_shots_active_timer < line_shots_active_timer_length_2 - 2 && !second_set_second_line_shot)
                    {
                        if (line_shots_phase_variation)
                        {
                            SpawnLineShots2();
                        }
                        else
                        {
                            SpawnLineShots1();
                        }
                        second_set_second_line_shot = true;
                    }
                }

                if (line_shots_active_timer < 0)
                {

                    if (current_horizontal_pattern_1 != null)
                    {
                        Destroy(current_horizontal_pattern_1.gameObject);
                    }
                    if (current_horizontal_pattern_2 != null)
                    {
                        Destroy(current_horizontal_pattern_2.gameObject);
                    }
                    if (current_vertical_pattern_1 != null)
                    {
                        Destroy(current_vertical_pattern_1.gameObject);
                    }
                    if (current_vertical_pattern_2 != null)
                    {
                        Destroy(current_vertical_pattern_2.gameObject);
                    }

                    if (second_set_second_line_shot)
                    {
                        line_shots_active_timer_started = false;
                        first_set_line_shots_completed = false;
                        second_set_shot = false;
                        second_set_second_line_shot = false;
                        transitional_timer = transitional_timer_length * 2.5f;
                        transitional_timer_started = true;
                        current_attack = 1;

                        if (bananabanana)
                        {
                            Instantiate(banana, new Vector2(starting_position.x + 0.65f, starting_position.y), Quaternion.identity);
                            bananabanana = !bananabanana;
                        }
                        else
                        {
                            Instantiate(banana, new Vector2(starting_position.x - 0.65f, starting_position.y), Quaternion.identity);
                            bananabanana = !bananabanana;
                        }

                        return;
                    }

                    if (!first_set_line_shots_completed)
                    {
                        if (line_shots_phase_variation)
                        {
                            SpawnLineShots2();
                        }
                        else
                        {
                            SpawnLineShots1();
                        }
                        line_shots_active_timer = line_shots_active_timer_length_1;
                        line_shots_active_timer_started = true;
                        first_set_line_shots_completed = true;
                    }
                    else
                    {
                        if (line_shots_phase_variation)
                        {
                            SpawnLineShots1();
                        }
                        else
                        {
                            SpawnLineShots2();
                        }
                        line_shots_active_timer = line_shots_active_timer_length_2;
                        line_shots_active_timer_started = true;
                        second_set_shot = true;
                    }
                }
            }
        }
    }

    private void FixedUpdate()
    {
        if (slashing)
        {
            rb.MovePosition(rb.position + static_move_direction * movement_speed * Time.fixedDeltaTime);

            if (to_left)
            {
                if (this.transform.position.x < static_player_position.x)
                {
                    slashing = false;
                    slash_collider.enabled = false;
                    if (num_of_slashes >= 4)
                    {
                        EndSlashingPhase();
                    }
                    else
                    {
                        animator.SetBool("SlashStartup", true);
                        animator.SetBool("Slashing", false);
                        if (!num_of_slashes_incremented)
                        {
                            num_of_slashes++;
                            num_of_slashes_incremented = true;
                        }
                    }
                }
            }
            else
            {
                if (this.transform.position.x > static_player_position.x)
                {
                    slashing = false;
                    slash_collider.enabled = false;
                    if(num_of_slashes >= 4)
                    {
                        EndSlashingPhase();
                    }
                    else
                    {
                        animator.SetBool("SlashStartup", true);
                        animator.SetBool("Slashing", false);
                        if (!num_of_slashes_incremented)
                        {
                            num_of_slashes++;
                            num_of_slashes_incremented = true;
                        }
                    }
                }
            }
        }
    }

    private void SpawnLineShots1()
    {
        current_horizontal_pattern_1 = Instantiate(horizontal_line_shot_pattern_1, transform);
        current_vertical_pattern_1 = Instantiate(vertical_line_shot_pattern_1, transform);
    }
    private void SpawnLineShots2()
    {
        current_horizontal_pattern_2 = Instantiate(horizontal_line_shot_pattern_2, transform);
        current_vertical_pattern_2 = Instantiate(vertical_line_shot_pattern_2, transform);
    }

    private void LineShotsPhase()
    {
        line_shots_phase_variation = !line_shots_phase_variation;

        if(line_shots_phase_variation)
        {
            SpawnLineShots1();
            line_shots_active_timer = line_shots_active_timer_length_1;
            line_shots_active_timer_started = true;
        }
        else
        {
            SpawnLineShots2();
            line_shots_active_timer = line_shots_active_timer_length_1;
            line_shots_active_timer_started = true;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (health <= 0)
        {
            Instantiate(boss_death_sqaure, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);
            Instantiate(bed, starting_position, Quaternion.identity);
            Health.DisableUI();
            GameManager.boss_dead = true;
            Destroy(this.gameObject);
        }
    }

    private void RotatePhaseStart()
    {
        rotate_phase_active = true;
        boss_tracker_spawner_object = Instantiate(boss_tracker_spawner, transform);
        boss_tracker_spawner_object.GetComponent<BulletSpawner>().enemy_bullet = boss_projectile;
        body_hitbox.enabled = false;
        body_hitbox_2.enabled = false;
        body_circular_hitbox.enabled = true;
    }

    private void RotatePhaseEnd()
    {
        Destroy(boss_tracker_spawner_object.gameObject);
        Destroy(random_spawner_object.gameObject);
        rotate_phase_active = false;
        rotate_phase_part_two_started = false;
        rotate_phase_part_two_transition_timer_started = false;
        transitional_timer = transitional_timer_length;
        transitional_timer_started = true;
        animator.SetBool("RotatePhase", false);
        current_attack += 1;
        body_hitbox.enabled = true;
        body_hitbox_2.enabled = true;
        body_circular_hitbox.enabled = false;
    }

    private void SpawnAdds()
    {
        animator.SetBool("SpawnAdds", false);
        Instantiate(add_spawn, add_spawn_position_top_left, Quaternion.identity);
        Instantiate(add_spawn, add_spawn_position_top_right, Quaternion.identity);
        Instantiate(add_spawn, add_spawn_position_bottom_left, Quaternion.identity);
        Instantiate(add_spawn, add_spawn_position_bottom_right, Quaternion.identity);

        if(current_attack == 1)
        {
            transitional_timer = transitional_timer_length * 3;
        }
        else if(current_attack == 2) 
        {
            transitional_timer = transitional_timer_length * 2;
        }
        else if(current_attack == 3)
        {
            transitional_timer = transitional_timer_length * 1.5f;
        }
        else if(current_attack == 4 || current_attack == 5)
        {
            transitional_timer = transitional_timer_length;
        }
    
        transitional_timer_started = true;
        current_attack += 1;
    }

    private void StartSpawnerPhase()
    {
        tracker_spawner_object = Instantiate(tracker_spawner, transform);
        tracker_spawner_object.cooldown_time = 1f;
        tracker_spawner_object.GetComponent<BulletSpawner>().enemy_bullet = boss_projectile;

        rotating_spawner_object = Instantiate(rotating_spawner, transform);
        rotating_spawner_object.cooldown_time = 0.4f;
        rotating_spawner_object.rotation_speed *= -1;
        rotating_spawner_object.GetComponent<BulletSpawner>().enemy_bullet = boss_projectile;

        rotating_spawner_object_2 = Instantiate(rotating_spawner, transform);
        rotating_spawner_object_2.cooldown_time = 0.4f;
        rotating_spawner_object_2.GetComponent<BulletSpawner>().enemy_bullet = boss_projectile;

        random_spawner_object = Instantiate(random_spawner, transform);
        random_spawner_object.shot_cooldown = 0.2f;
        random_spawner_object.GetComponent<BulletSpawner>().enemy_bullet = boss_projectile;

        spawner_phase_timer = spawner_phase_timer_length;
        spawner_phase_timer_started = true;
    }

    private void EndSpawnerPhase()
    {
        Destroy(tracker_spawner_object.gameObject);
        Destroy(rotating_spawner_object.gameObject);
        Destroy(rotating_spawner_object_2.gameObject);
        Destroy(random_spawner_object.gameObject);
        spawner_phase_timer_started = false;

        transitional_timer = transitional_timer_length;
        transitional_timer_started = true;
        animator.SetBool("SpawnerPhase", false);
        current_attack += 1;
    }

    private void EndSlashingPhase()
    {
        animator.SetBool("LeaveSlashing", true);
        num_of_slashes = 0;
        transitional_timer = transitional_timer_length;
        transitional_timer_started = true;
        current_attack += 1;
        sprite_renderer.color = new Color(255 / 255f, 255 / 255f, 255 / 255f);
    }

    public void SlashStartup()
    {
        sprite_renderer.color = new Color(255 / 255f, 0 / 255f, 0 / 255f);
    }

    public void RemoveSlashStartup()
    {
        if(num_of_slashes == 4)
        {
            static_player_position = starting_position;
            static_move_direction = (static_player_position - this.transform.position).normalized;
        }
        else
        {
            static_player_position = PlayerController.player_position;
            static_move_direction = (static_player_position - this.transform.position).normalized;
        }
        
        slashing = true;

        if (this.transform.position.x > static_player_position.x)
        {
            to_left = true;
            sprite_renderer.flipX = true;
            slash_collider.offset = new Vector2(-0.19f, slash_collider.offset.y);
        }
        else
        {
            to_left = false;
            sprite_renderer.flipX = false;
            slash_collider.offset = new Vector2(0.19f, slash_collider.offset.y);
        }

        animator.SetBool("SlashStartup", false);
        num_of_slashes_incremented = false;
        animator.SetBool("Slashing", true);
        
        slash_collider.enabled = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (enemy_spawned)
        {
            if(!num_of_slashes_incremented)
            {
                num_of_slashes++;
                num_of_slashes_incremented = true;
            }
            slashing = false;
            slash_collider.enabled = false;
            animator.SetBool("SlashStartup", true);
            animator.SetBool("Slashing", false);
        }
    }

    public void SlashFailsafe()
    {
        if (enemy_spawned)
        {
            if (!num_of_slashes_incremented)
            {
                num_of_slashes++;
                num_of_slashes_incremented = true;
            }
            slashing = false;
            slash_collider.enabled = false;
            animator.SetBool("SlashStartup", true);
            animator.SetBool("Slashing", false);
        }
    }

    public void OnSpawn()
    {
        MusicManager.StopMusic();
        boss_red_square_object = Instantiate(boss_red_square, new Vector2(this.transform.position.x, this.transform.position.y), Quaternion.identity);
    }

    public void OnFinishSpawn()
    {
        Destroy(boss_red_square_object.gameObject);
        transitional_timer = transitional_timer_length;
        transitional_timer_started = true;
        body_circular_hitbox.enabled = true;
        GameManager.boss_spawned = true;
    }
}
