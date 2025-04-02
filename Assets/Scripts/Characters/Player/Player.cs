using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;


public class Player : Character
{
    // The current player state
    public PlayerState playerState;
    public bool isMoving;

    // PlayerStates
    public PlayerState explorationState = new ExplorationState();
    public PlayerState battleState = new BattleState();

    // Attributes
    public float speed = 27f;
    public float verticalInput = 0f;
    public float horizontalInput = 0f;
    public DirectionState currentDirection;

    [SerializeField] AttackAbility[] allMoves;

    // Components
    private Rigidbody rb;

    // Managers
    private ReferenceManager referenceManager;

    // Events
    public event Action OnMoveSelected;

    public void SelectMoveFromUI()
    {
        // This method is triggered when the player selects a move
        OnMoveSelected?.Invoke(); // Notify listeners that a move was selected
    }

    void Start()
    {
        InitializeCharacter("Player", 100, 100, E_CharacterType.PLAYER);

        rb = GetComponent<Rigidbody>();
        playerState = explorationState;
        playerState.EnterState(this); // Enter the ExplorationState initially
        referenceManager = StandardFunctions.FindReferenceManager();

        EventManager.StartBattle += HandleStartBattle;
        EventManager.EndBattle += HandleEndBattle;
    }


    void Update()
    {
        playerState.Update(this, Time.deltaTime);
        //Debug.Log(referenceManager.pixelPerfectCamera);

        /*if (isMoving)
        {
            if (referenceManager.pixelPerfectCamera.pixelSnapping)
            {
                referenceManager.pixelPerfectCamera.pixelSnapping = false;
            }
        }
        else
        {
            if (!referenceManager.pixelPerfectCamera.pixelSnapping)
            {
                referenceManager.pixelPerfectCamera.pixelSnapping = true;
            }
        }*/
    }

    void FixedUpdate()
    {
        playerState.FixedUpdate(this, Time.deltaTime);
    }

    public void ChangeState(PlayerState newState)
    {
        playerState.ExitState(this);
        playerState = newState;
        playerState.EnterState(this);
    }

    public DirectionState GetDirectionState()
    {
        return currentDirection;
    }   

    public void HandleStartBattle()
    {
        Debug.Log("Battle Started!");
        ChangeState(battleState);
    }

    public void HandleEndBattle()
    {
        Debug.Log("Battle Ended!");
    }

    public void SetReferenceManager(ReferenceManager referenceManagerObject)
    {
        referenceManager = referenceManagerObject;
    }

    public PlayerState GetPlayerState()
    {
        return playerState;
    }

    public void SetMove()
    {
        Debug.Log("Player move set");
        SetCurrentMove(allMoves[0]);
    }

    public void RunMove()
    {
        Debug.Log("Running move - " + allMoves[0]);
    }
}

