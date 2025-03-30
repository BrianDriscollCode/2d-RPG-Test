using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBattle : MonoBehaviour
{
    private BoxCollider2D trigger;
    public Player player;
    private ReferenceManager referenceManager;
    [SerializeField] BattleContext battleContext;

    private void Start()
    {
        trigger = GetComponent<BoxCollider2D>();
        referenceManager = StandardFunctions.FindReferenceManager();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<Player>();
            InitializeBattle();
        }
    }

    private void InitializeBattle()
    {
        GameStateManager gameStateManager = referenceManager.GameStateManager;
        gameStateManager.SetGameState(EGameState.BATTLE);
        battleContext.PlaceFighters();
        battleContext.PlaceCamera();
        battleContext.InitiateBattleEngine();

        // function here
        Debug.Log("TriggerBattle::InitializeBattle - Triggered");
    }
}
