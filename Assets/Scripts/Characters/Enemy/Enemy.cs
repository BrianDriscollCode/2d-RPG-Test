using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character
{
    
    [SerializeField] AttackAbility[] allMoves;
    DirectionState currentDirection;
    // Start is called before the first frame update
    void Start()
    {
        InitializeCharacter("Enemy", 100, 100, E_CharacterType.ENEMY);
        currentDirection = DirectionState.LEFT;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void SetMove()
    {
        SetCurrentMove(allMoves[0]);
    }

    public void RunMove()
    {
        GetComponent<Animator>().Play("LeftSlash");
    }

    // Getters and Setters
    public DirectionState GetDirectionState()
    {
        return currentDirection;
    }
}
