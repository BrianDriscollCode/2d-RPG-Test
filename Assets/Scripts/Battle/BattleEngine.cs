using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BattleEngine : MonoBehaviour
{
    private List<MonoBehaviour> participants = new List<MonoBehaviour>(); // Store both players and enemies
    private int turnPointer = 0;
    GameObject debugBattleUI;

    // Initiated during setup
    DebugBattleUI debugBattleUIComponent;
    MonoBehaviour currentParticipant;
    GameObject battleDebug;
    bool connected;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            NextTurn();
        }
    }

    public void SetupBattleEngine(Player[] players, Enemy[] enemies, GameObject debugBattleUIRef)
    {
        debugBattleUI = debugBattleUIRef;

        participants.Clear();

        // Create a list to store participants with their initiative values
        List<(int initiative, MonoBehaviour participant)> initiativeList = new List<(int, MonoBehaviour)>();

        // Add players with a random initiative
        foreach (var player in players)
        {
            int initiative = Random.Range(1, 100);
            initiativeList.Add((initiative, player));
        }

        // Add enemies with a random initiative
        foreach (var enemy in enemies)
        {
            int initiative = Random.Range(1, 100);
            initiativeList.Add((initiative, enemy));
        }

        // Sort participants by initiative (higher goes first)
        initiativeList.Sort((a, b) => b.initiative.CompareTo(a.initiative));

        // Extract sorted participants into the list
        foreach (var entry in initiativeList)
        {
            participants.Add(entry.participant);
            Debug.Log($"Turn Order: {entry.participant.name}, Initiative: {entry.initiative}");
        }

        //Debug.Log("Battle engine initialized!");

        // setup Debug Interface
        battleDebug = Instantiate(debugBattleUI);
        currentParticipant = GetCurrentTurnParticipant();
        debugBattleUIComponent = battleDebug.GetComponent<DebugBattleUI>();
        connected = true;
        debugBattleUIComponent.SetupDebugBattleUI(currentParticipant, connected);
    }

    // Move to the next turn
    public void NextTurn()
    {
        turnPointer = (turnPointer + 1) % participants.Count;
        //Debug.Log($"Next turn: {participants[turnPointer].name}");
        currentParticipant = GetCurrentTurnParticipant();
        debugBattleUIComponent.SetConnectedValueText(connected);
        debugBattleUIComponent.SetCurrentTurnValueText(currentParticipant);
        Debug.Log("DEBUG!!! " + currentParticipant);
    }

    // Function to get the current participant's turn
    public MonoBehaviour GetCurrentTurnParticipant()
    {
        if (participants.Count == 0) return null;
        return participants[turnPointer];
    }
}
