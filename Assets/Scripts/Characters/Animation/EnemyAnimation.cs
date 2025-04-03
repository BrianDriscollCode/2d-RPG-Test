using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimation : MonoBehaviour
{
    public Animator animator;
    DirectionState currentDirection;
    [SerializeField] Enemy enemy;

    bool playingAttackAnimation = false;

    // Start is called before the first frame update
    void Start()
    {
        currentDirection = enemy.GetDirectionState();
        animator = GetComponent<Animator>();
        animator.Play("IdleLeft");
    }

    // Update is called once per frame
    void Update()
    {
        currentDirection = enemy.GetDirectionState();

        if (!playingAttackAnimation)
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

    public void EnableAttackAnimation()
    {
        playingAttackAnimation = true;
    }

    public void DisableAttackAnimation()
    {
        playingAttackAnimation = false;
    }

    public void PlayAttackAnimation(string attackAnimationName)
    {
        animator.Play(attackAnimationName);
    }

    public void OnAttackAnimationComplete()
    {
        BattleEventManager.OnPlayerMoveCompleted();
        Debug.Log("Attack animation completed!");
    }

}
