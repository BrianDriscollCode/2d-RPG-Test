using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class CharacterAnim : MonoBehaviour
{
    public Animator animator;

    enum DirectionState
    {
        UP, UPLEFT, UPRIGHT,
        DOWN, DOWNLEFT, DOWNRIGHT,
        LEFT,
        RIGHT
    }

    DirectionState currentDirection;

    void Start()
    {
        currentDirection = DirectionState.DOWN;
        animator = GetComponent<Animator>();
        animator.Play("IdleDown");
    }

    void Update()
    {
        bool isMovingUp = Input.GetKey(KeyCode.W);
        bool isMovingDown = Input.GetKey(KeyCode.S);
        bool isMovingLeft = Input.GetKey(KeyCode.A);
        bool isMovingRight = Input.GetKey(KeyCode.D);

        HandleMovement(isMovingUp, isMovingDown, isMovingLeft, isMovingRight);
    }

    void HandleMovement(bool isMovingUp, bool isMovingDown, bool isMovingLeft, bool isMovingRight)
    {


        if (isMovingUp && isMovingDown)
        {
            return;
        }

        if (isMovingUp && isMovingLeft)
        {
            currentDirection = DirectionState.UPLEFT;
            animator.Play("WalkLeft");
        }
        else if (isMovingUp && isMovingRight)
        {
            currentDirection = DirectionState.UPRIGHT;
            animator.Play("WalkRight");
        }
        else if (isMovingDown && isMovingLeft)
        {
            currentDirection = DirectionState.DOWNLEFT;
            animator.Play("WalkLeft");
        }
        else if (isMovingDown && isMovingRight)
        {
            currentDirection = DirectionState.DOWNRIGHT;
            animator.Play("WalkRight");
        }
        else if (isMovingUp)
        {
            currentDirection = DirectionState.UP;
            animator.Play("WalkUp");
        }
        else if (isMovingDown)
        {
            currentDirection = DirectionState.DOWN;
            animator.Play("WalkDown");
        }
        else if (isMovingLeft)
        {
            currentDirection = DirectionState.LEFT;
            animator.Play("WalkLeft");
        }
        else if (isMovingRight)
        {
            currentDirection = DirectionState.RIGHT;
            animator.Play("WalkRight");
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
            case DirectionState.UPLEFT:
            case DirectionState.UPRIGHT:
                animator.Play("IdleUp");
                break;

            case DirectionState.DOWN:
            case DirectionState.DOWNLEFT:
            case DirectionState.DOWNRIGHT:
                animator.Play("IdleDown");
                break;

            case DirectionState.LEFT:
                animator.Play("IdleLeft");
                break;

            case DirectionState.RIGHT:
                animator.Play("IdleRight");
                break;

            default:
                animator.Play("IdleDown"); // Default idle if no direction is set
                break;
        }
    }
}