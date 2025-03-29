using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class ExplorationState : PlayerState
{
    public void EnterState(Player player)
    { 
        
    }

    public void Update(Player player, float deltaTime)
    {
        bool isMovingUp = Input.GetKey(KeyCode.W);
        bool isMovingDown = Input.GetKey(KeyCode.S);
        bool isMovingLeft = Input.GetKey(KeyCode.A);
        bool isMovingRight = Input.GetKey(KeyCode.D);

        player.currentDirection = GetCurrentDirection(player, isMovingUp, isMovingDown, isMovingLeft, isMovingRight);
        HandleInput(player, isMovingUp, isMovingDown, isMovingLeft, isMovingRight);
    }
    public void FixedUpdate(Player player, float deltaTime)
    {
        HandleRayCast(player);
        HandleMovement(player);
    }

    public void ExitState(Player player)
    {
    }

    void HandleMovement(Player player)
    {
        if (player.horizontalInput != 0.0f && player.verticalInput != 0.0f)
        {
            player.horizontalInput /= 1.25f;
            player.verticalInput /= 1.25f;
        }

        player.transform.position += new Vector3(player.horizontalInput * player.speed * Time.deltaTime, player.verticalInput * player.speed * Time.deltaTime, 0);
    }

    void HandleRayCast(Player player)
    {
        Vector2 rayOrigin = player.transform.position;
        float rayLength = 0.5f;
        int layerMask = LayerMask.GetMask("Enemy");

        Vector2 rayDirection = Vector2.zero;

        switch (player.currentDirection)
        {
            case DirectionState.UP:
                rayDirection = Vector2.up;
                break;
            case DirectionState.DOWN:
                rayDirection = Vector2.down;
                break;
            case DirectionState.LEFT:
                rayDirection = Vector2.left;
                break;
            case DirectionState.RIGHT:
                rayDirection = Vector2.right;
                break;
        }

        // Draw the ray in the Scene view for debugging
        Debug.DrawRay(rayOrigin, rayDirection * rayLength, Color.red, 0.1f);

        // Perform the raycast
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, rayDirection, rayLength, layerMask);

        if (hit.collider != null)
        {
            if (hit.collider.CompareTag("Enemy"))
            {
                // Transition to BattleState when an enemy is detected
                player.ChangeState(player.battleState);
            }
        }
    }

    void HandleInput(Player player, bool isMovingUp, bool isMovingDown, bool isMovingLeft, bool isMovingRight)
    {
        if (isMovingUp)
        {
            player.verticalInput = 0.1f;
        }
        else if (isMovingDown)
        {
            player.verticalInput = -0.1f;
        }
        else
        {
            player.verticalInput = 0.0f;
        }

        if (isMovingLeft)
        {
            player.horizontalInput = -0.1f;

        }
        else if (isMovingRight)
        {
            player.horizontalInput = 0.1f;
        }
        else
        {
            player.horizontalInput = 0.0f;
        }

        if (isMovingUp || isMovingDown || isMovingRight || isMovingLeft)
        {
            player.isMoving = true;
        }
        else
        {
            player.isMoving = false;

        }
    }

    DirectionState GetCurrentDirection(Player player, bool isMovingUp, bool isMovingDown, bool isMovingLeft, bool isMovingRight)
    {
        DirectionState playerDirection = player.currentDirection;

        if (isMovingLeft || isMovingRight) // Prioritize horizontal movement
        {
            if (isMovingLeft)
            {
                playerDirection = DirectionState.LEFT;
            }
            else
            {
                playerDirection = DirectionState.RIGHT;
            }
        }
        else if (isMovingUp || isMovingDown) // Otherwise, move vertically
        {
            if (isMovingUp)
            {
                playerDirection = DirectionState.UP;
            }
            else
            {
                playerDirection = DirectionState.DOWN;
            }
        }

        return playerDirection;
    }
}
