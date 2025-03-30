using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DebugBattleUI : MonoBehaviour
{
    ReferenceManager referenceManager;

    public MonoBehaviour currentTurn;
    bool connected;

    [SerializeField] TextMeshProUGUI currentTurnValue;
    [SerializeField] TextMeshProUGUI connectedValue;

    private void Start()
    {
        referenceManager = StandardFunctions.FindReferenceManager();
    }

    public void SetupDebugBattleUI(MonoBehaviour currentTurnObject, bool connectedStatus)
    {
        currentTurn = currentTurnObject;
        connected = connectedStatus;
    }

    private void FixedUpdate()
    {
        currentTurnValue.text = currentTurn.name;
        connectedValue.text = connected.ToString();

    }

    public void SetCurrentTurnValueText(MonoBehaviour currentTurnMono)
    {
        currentTurn = currentTurnMono;
        Debug.Log(currentTurnMono.name + " - CurrentTurnMono");
    }

    public void SetConnectedValueText(bool connectedValueMono)
    {
        connectedValue.text = connectedValueMono.ToString();
    }
}