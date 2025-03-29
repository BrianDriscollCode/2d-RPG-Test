using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugUserInterface : MonoBehaviour
{
    ReferenceManager referenceManager;
    Player player;
    GameStateManager gameStateManager;

    [SerializeField] TextMeshProUGUI GameState;
    [SerializeField] TextMeshProUGUI PlayerState;

    private void Start()
    {
        referenceManager = StandardFunctions.FindReferenceManager();
        player = referenceManager.Player;
        gameStateManager = referenceManager.GameStateManager;
    }

    private void FixedUpdate()
    {
        GameState.text = gameStateManager.GetGameState().ToString();
        PlayerState.text = player.playerState.ToString();
    }
}
