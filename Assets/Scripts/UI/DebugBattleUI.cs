using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugBattleUI : MonoBehaviour
{
    ReferenceManager referenceManager;

    public MonoBehaviour currentTurn;
    bool connected;
    BattleEngineState state;

    [SerializeField] TextMeshProUGUI currentTurnValue;
    [SerializeField] TextMeshProUGUI connectedValue;
    [SerializeField] TextMeshProUGUI stateValue;

    private void Start()
    {
        referenceManager = StandardFunctions.FindReferenceManager();
    }

    public void SetupDebugBattleUI(MonoBehaviour currentTurnObject, bool connectedStatus, BattleEngineState stateStatus)
    {
        currentTurn = currentTurnObject;
        connected = connectedStatus;
        state = stateStatus;
    }

    private void FixedUpdate()
    {
        currentTurnValue.text = currentTurn.name;
        connectedValue.text = connected.ToString();
        stateValue.text = state.ToString();
    }

    public void SetCurrentTurnValueText(MonoBehaviour currentTurnMono)
    {
        currentTurn = currentTurnMono;
        Debug.Log(currentTurnMono.name + " - CurrentTurnMono");
    }

    public void SetConnectedValueText(bool connectedValueMono)
    {
        connected = connectedValueMono;
    }

    public void SetStateValueText(BattleEngineState stateStatus)
    {
        state = stateStatus;
    }
}