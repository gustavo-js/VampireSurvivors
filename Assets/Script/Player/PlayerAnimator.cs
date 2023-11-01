using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    Animator animator;
    PlayerMovement playerMovement;
    SpriteRenderer spriteRenderer;

    void Start()
    {
        animator = GetComponent<Animator>();
        playerMovement = GetComponent<PlayerMovement>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        CheckXMovement();
        CheckYMovement();
    }

    void CheckXMovement()
    {
        if (playerMovement.moveDirection.x != 0)
        {
            animator.SetBool("HorizontalWalking", true);
            SpriteDirectionCheck();
        }
        else
        {
            animator.SetBool("HorizontalWalking", false);
        }
    }

    void CheckYMovement()
    {
        if (playerMovement.moveDirection.y != 0)
        {
            if (playerMovement.moveDirection.y < 0)
            {
                animator.SetBool("WalkingDown", true);
            }
            else
            {
                animator.SetBool("WalkingUp", true);
            }
        }
        else 
        {
                animator.SetBool("WalkingDown", false);
                animator.SetBool("WalkingUp", false);
        }
    }

    void SpriteDirectionCheck()
    {
        if (playerMovement.moveDirection.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}
