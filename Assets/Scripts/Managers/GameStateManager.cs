using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    EGameState currentGameState;

    private void Start()
    {
        currentGameState = EGameState.EXPLORATION;
    }

    public void SetGameState(EGameState gameState)
    {
        if (gameState == EGameState.BATTLE)
        {
            EventManager.OnStartBattle();
        }

        currentGameState = gameState;
    }

    public EGameState GetGameState()
    {
        return currentGameState;
    }
}
