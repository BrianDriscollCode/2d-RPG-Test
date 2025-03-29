using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAnimSimple : MonoBehaviour
{
    public Animator animator;
    DirectionState currentDirection;
    [SerializeField] Player player;

    void Start()
    {
        currentDirection = player.GetDirectionState();
        animator = GetComponent<Animator>();
        animator.Play("IdleDown");
    }

    void Update()
    {   
        // Exploration State
        if (player.playerState == player.explorationState)
        {
            HandleMovement();
            currentDirection = player.GetDirectionState();
        }
        // Battle State
        else
        {
            PlayIdleAnimation();
        }
    }

    void HandleMovement()
    {
        if (player.currentDirection == DirectionState.LEFT && player.isMoving)
        {
            animator.Play("WalkLeft");
        }
        else if (player.currentDirection == DirectionState.RIGHT && player.isMoving)
        {
            animator.Play("WalkRight");
        }
        else if (player.currentDirection == DirectionState.UP && player.isMoving)
        {
            animator.Play("WalkUp");
        }
        else if (player.currentDirection == DirectionState.DOWN && player.isMoving)
        {
            animator.Play("WalkDown");
        }
        else
        {
            PlayIdleAnimation();
        }
    }

    void PlayIdleAnimation()
    {
        switch (currentDirection)
        {
            case DirectionState.UP:
                animator.Play("IdleUp");
                break;

            case DirectionState.DOWN:
                animator.Play("IdleDown");
                break;

            case DirectionState.LEFT:
                animator.Play("IdleLeft");
                break;

            case DirectionState.RIGHT:
                animator.Play("IdleRight");
                break;

            default:
                animator.Play("IdleDown"); // Default idle
                break;
        }
    }
}
