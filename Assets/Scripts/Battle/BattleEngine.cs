using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BattleEngine : MonoBehaviour
{
    private List<MonoBehaviour> participants = new List<MonoBehaviour>(); // Store both players and enemies
    private int turnPointer = 0;
    GameObject debugBattleUI;

    // Resources 
    [SerializeField] public GameObject pointedHand;

    // States
    BattleEngineState BES_Setup = new BES_Setup();
    BattleEngineState BES_SelectMove = new BES_SelectMove();
    BattleEngineState BES_SelectTarget = new BES_SelectTarget();
    BattleEngineState BES_Move = new BES_Move();
    BattleEngineState BES_Next = new BES_Next();

    BattleEngineState currentBattleState;

    // Initiated during setup
    DebugBattleUI debugBattleUIComponent;
    MonoBehaviour currentParticipant;
    GameObject battleDebug;
    BattleUI battleUI;
    public BattleButtonsPanel battleButtonPanel;
    bool connected;

    // References
    [SerializeField] GameObject battleUIRef;
    [SerializeField] GameObject selectionArrow;
    private GameObject currentArrow;


    private void Start()
    {
        Debug.Log(currentBattleState + "current battle state");
    }

    private void Update()
    {
       /* if (Input.GetKeyDown(KeyCode.Q) && currentBattleState == BES_SelectMove)
        {
            NextTurn();
        }*/

        currentBattleState.Update(this, Time.deltaTime);
        currentBattleState.FixedUpdate(this, Time.deltaTime);
    }

    public void SetupBattleEngine(Player[] players, Enemy[] enemies, GameObject debugBattleUIRef)
    {
        connected = true;
        debugBattleUI = debugBattleUIRef;

        CreateParticipantList(players, enemies);
        SetupDebugBattleUI();
        SetupBattleUI();

        currentBattleState = BES_SelectMove;
        UpdateDebugBattleUI();
    }

    //*************FunctionName: CreateParticipantList
    //
    // Explanation: Get all enemeis and players in current battle context
    //              and add them to an array and sort according to a 
    //              randomly assigned RNG number
    private void CreateParticipantList(Player[] players, Enemy[] enemies)
    {
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
    }

    private void SetupDebugBattleUI()
    {
        currentBattleState = BES_Setup;
        battleDebug = Instantiate(debugBattleUI);
        currentParticipant = GetCurrentTurnParticipant();
        debugBattleUIComponent = battleDebug.GetComponent<DebugBattleUI>();
        debugBattleUIComponent.SetupDebugBattleUI(currentParticipant, connected, currentBattleState);
        Debug.Log(currentBattleState + "CURRENT BATTLE STATE");

        // Need better solution that arbitray delay, but good enough for now
        StartCoroutine(DelayedPointSelectionArrow(currentParticipant));
    }

    private void SetupBattleUI()
    {
        battleUI = Instantiate(battleUIRef).GetComponent<BattleUI>();
        battleButtonPanel = battleUI.GetBattleButtonsPanel;
        battleButtonPanel.SetupBattleButtons(this);
    }

    private IEnumerator DelayedPointSelectionArrow(MonoBehaviour target)
    {
        yield return new WaitForSeconds(0.5f);
        PointSelectionArrow(target);
    }

    // Move to the next turn
    public void NextTurn()
    {
        turnPointer = (turnPointer + 1) % participants.Count;
        UpdateDebugBattleUI();
    }

    private void UpdateDebugBattleUI()
    {
        currentParticipant = GetCurrentTurnParticipant();
        debugBattleUIComponent.SetConnectedValueText(connected);
        debugBattleUIComponent.SetCurrentTurnValueText(currentParticipant);
        debugBattleUIComponent.SetStateValueText(currentBattleState);
        Debug.Log(currentBattleState + " -CURRENT BATTLE STATE");
        //PointSelectionArrow();
    }

    // Function to get the current participant's turn
    public MonoBehaviour GetCurrentTurnParticipant()
    {
        if (participants.Count == 0) return null;
        return participants[turnPointer];
    }
    
    public void PointSelectionArrow(MonoBehaviour target)
    {
        Vector3 offset = new Vector3(0, 0.5f, 0);

        if (currentArrow != null)
        {
            Destroy(currentArrow);
        }
        
        currentArrow = Instantiate(selectionArrow);
        currentArrow.transform.position = target.transform.position + offset;
    }

    public void test()
    {
        Debug.Log("TEST");
    }
}
