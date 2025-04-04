using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.XR;


public class BattleEngine : MonoBehaviour
{
    private List<MonoBehaviour> participants = new List<MonoBehaviour>(); // Store both players and enemies
    private int turnPointer = 0;
    GameObject debugBattleUI;

    // Resources 
    [SerializeField] public GameObject pointedHand;

    // States
    public BattleEngineState BES_Setup = new BES_Setup();
    public BattleEngineState BES_SelectMove = new BES_SelectMove();
    public BattleEngineState BES_SelectTarget = new BES_SelectTarget();
    public BattleEngineState BES_Move = new BES_Move();
    public BattleEngineState BES_Next = new BES_Next();

    BattleEngineState currentBattleState;

    // Initiated during setup
    DebugBattleUI debugBattleUIComponent;
    Player[] players;
    Enemy[] enemies;
    public MonoBehaviour currentParticipant;
    public Character currentTarget;
    GameObject battleDebug;
    BattleUI battleUI;
    public BattleButtonsPanel battleButtonPanel;
    bool connected;

    // References
    [SerializeField] GameObject battleUIRef;
    [SerializeField] GameObject selectionArrow;
    private GameObject currentArrow;

    public GameObject enemyPosition1;
    public GameObject enemyPosition2;
    public GameObject enemyPosition3;
    public GameObject enemyPosition4;
    public GameObject playerPosition1;
    public GameObject playerPosition2;
    public GameObject playerPosition3;
    public GameObject playerPosition4;


    private void Start()
    {
        Debug.Log(currentBattleState + "current battle state");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && currentBattleState == BES_SelectMove)
        {
            NextTurn();
        }

        currentBattleState.Update(this, Time.deltaTime);
        currentBattleState.FixedUpdate(this, Time.deltaTime);
    }

    public void SetupBattleEngine(Player[] players, Enemy[] enemies, GameObject debugBattleUIRef,
        GameObject playerPosition1, GameObject playerPosition2, GameObject playerPosition3, GameObject playerPosition4,
        GameObject enemyPosition1, GameObject enemyPosition2, GameObject enemyPosition3, GameObject enemyPosition4
    )
    {
        connected = true;
        debugBattleUI = debugBattleUIRef;

        this.players = players;
        this.enemies = enemies;
        CreateParticipantList(players, enemies);
        SetupDebugBattleUI();
        SetupBattleUI();

        changeState(BES_SelectMove);
        UpdateDebugBattleUI();

        this.playerPosition1 = playerPosition1;
        this.playerPosition2 = playerPosition2;
        this.playerPosition3 = playerPosition3;
        this.playerPosition4 = playerPosition4;

        this.enemyPosition1 = enemyPosition1;
        this.enemyPosition2 = enemyPosition2;
        this.enemyPosition3 = enemyPosition3;
        this.enemyPosition4 = enemyPosition4;
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
            int initiative = Random.Range(99, 100);
            initiativeList.Add((initiative, player));
        }

        // Add enemies with a random initiative
        foreach (var enemy in enemies)
        {
            int initiative = Random.Range(1, 80);
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
        currentBattleState.EnterState(this);
        battleDebug = Instantiate(debugBattleUI);
        currentParticipant = GetCurrentTurnParticipant();
        debugBattleUIComponent = battleDebug.GetComponent<DebugBattleUI>();
        debugBattleUIComponent.SetupDebugBattleUI(currentParticipant, connected, currentBattleState);
        Debug.Log(currentBattleState + "CURRENT BATTLE STATE");

        // Need better solution that arbitray delay, but good enough for now
        //StartCoroutine(DelayedPointSelectionArrow(currentParticipant));
    }

    private void SetupBattleUI()
    {
        Debug.Log("SETUPBATTLEUI");
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
        if (currentBattleState != BES_Next)
        {
            changeState(BES_Next);
        }
        turnPointer = (turnPointer + 1) % participants.Count;
        changeState(BES_SelectMove);
    }

    private void UpdateDebugBattleUI()
    {
        currentParticipant = GetCurrentTurnParticipant();
        debugBattleUIComponent.SetConnectedValueText(connected);
        debugBattleUIComponent.SetCurrentTurnValueText(currentParticipant);
        debugBattleUIComponent.SetStateValueText(currentBattleState);
        //Debug.Log(currentBattleState + " -CURRENT BATTLE STATE");
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

    public void test(string phrase)
    {
        Debug.Log("TEST - " + phrase);
    }

    public void changeState(BattleEngineState state)
    {
        currentBattleState.ExitState(this);
        UpdateDebugBattleUI();
        currentBattleState = state;
        currentBattleState.EnterState(this);
        UpdateDebugBattleUI();
    }


    // Getters and Setters

    public BattleEngineState GetBattleEngineState() { return currentBattleState; }
    public MonoBehaviour GetCurrentParticipant() { return currentParticipant; }
    // Redundant but keeping for now just incase
    public void SetCurrentParticipant() { currentParticipant = GetCurrentTurnParticipant(); }

    public Player[] GetPlayers() { return players; }

    public void SetPlayers(Player[] players) { this.players = players; }

    public Enemy[] GetEnemies() { return enemies; }
    public void SetEnemies(Enemy[] enemies) { this.enemies = enemies; }

    public GameObject GetSelectionArrow() { return selectionArrow; }
    public void SetSelectionArrow(GameObject image) { this.selectionArrow = image; }


    public Character GetCurrentTarget() { return  currentTarget; }
    public void SetCurrentTarget(Character currentTarget) { this.currentTarget = currentTarget; Debug.Log("BattleEngine::SetCurrentTarget - " + currentTarget); }
}
