using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceManager : MonoBehaviour
{
    public static ReferenceManager Instance { get; private set; }

    public Player Player { get; private set; }
    public GameStateManager GameStateManager { get; private set; }
    public BattleManager BattleManager { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        FindPlayer();
        FindGameStateManager();
        FindBattleManager();
    }

    private void FindPlayer()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject == null)
        {
            Debug.LogWarning("ReferenceManager::FindPlayer::Error - 'Player object not found in scene'");
        }
        else
        {
            Player = playerObject.GetComponent<Player>();
            if (Player == null)
            {
                Debug.LogError("ReferenceManager::FindPlayer::Error - 'Player component not found on GameObject'");
            }
            else
            {
                Debug.Log("ReferenceManager::FindPlayer::Success - Found player: " + Player.name);
            }
        }
    }

    private void FindGameStateManager()
    {
        GameObject gameStateManagerObject = GameObject.FindWithTag("GameStateManager");
        if (gameStateManagerObject == null)
        {
            Debug.LogWarning("ReferenceManager::FindGameStateManager::Error - 'GameStateManager object not found in scene'");
        }
        else
        {
            GameStateManager = gameStateManagerObject.GetComponent<GameStateManager>();
            if (GameStateManager == null)
            {
                Debug.LogError("ReferenceManager::FindGameStateManager::Error - 'GameStateManager component not found on GameObject'");
            }
            else
            {
                Debug.Log("ReferenceManager::FindGameStateManager::Success - Found GameStateManager: " + GameStateManager.name);
            }
        }
    }

    private void FindBattleManager()
    {
        GameObject battleManagerObject = GameObject.FindWithTag("BattleManager");
        if (battleManagerObject == null)
        {
            Debug.LogWarning("ReferenceManager::FindBattleManager::Error - 'BattleManager object not found in scene'");
        }
        else
        {
            BattleManager = battleManagerObject.GetComponent<BattleManager>();
            if (BattleManager == null)
            {
                Debug.LogError("ReferenceManager::FindBattleManager::Error - 'BattleManager component not found on GameObject'");
            }
            else
            {
                Debug.Log("ReferenceManager::FindBattleManager::Success - Found BattleManager: " + BattleManager.name);
            }
        }
    }
}


