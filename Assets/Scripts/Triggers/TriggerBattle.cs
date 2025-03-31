using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerBattle : MonoBehaviour
{
    [SerializeField] BattleContext battleContext;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            InitializeBattle();
        }
    }

    private void InitializeBattle()
    {
        battleContext.InitiateBattleEngine();
    }
}
