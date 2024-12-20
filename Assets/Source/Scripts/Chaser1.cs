using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chaser1 : Base_ChaserEnemy
{
    private Animator animator;

    protected override void Start()
    {
        base.Start();
        animator = GetComponent<Animator>();
    }

    protected override void Update()
    {
        if (enemy_spawned)
        {
            base.Update();
            if (!in_cooldown)
            {
                animator.SetBool("IsRunning", true);
            }
            else
            {
                animator.SetBool("IsRunning", false);
            }
        }
    }
}
