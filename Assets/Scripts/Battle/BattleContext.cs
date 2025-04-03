using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class BattleContext : MonoBehaviour
{
    // Enemy related
    Enemy[] enemies;

    [SerializeField] Enemy enemy1;
    [SerializeField] Enemy enemy2;
    [SerializeField] Enemy enemy3;
    [SerializeField] Enemy enemy4;

    [SerializeField] GameObject enemyPosition1;
    [SerializeField] GameObject enemyPosition2;
    [SerializeField] GameObject enemyPosition3;
    [SerializeField] GameObject enemyPosition4;

    private SpriteRenderer enemy1PositionRenderer;
    private SpriteRenderer enemy2PositionRenderer;
    private SpriteRenderer enemy3PositionRenderer;
    private SpriteRenderer enemy4PositionRenderer;


    // Player related
    Player[] players;

    [SerializeField] GameObject playerPosition1;
    [SerializeField] GameObject playerPosition2;
    [SerializeField] GameObject playerPosition3;
    [SerializeField] GameObject playerPosition4;

    private SpriteRenderer player1PositionRenderer;
    private SpriteRenderer player2PositionRenderer;
    private SpriteRenderer player3PositionRenderer;
    private SpriteRenderer player4PositionRenderer;

    // Amounts to decide sprite placement
    int playerAmount = 1;
    int enemyAmount = 0;

    // References
    Player player;
    ReferenceManager referenceManager;
    [SerializeField] GameObject BattleEnginePrefab;
    [SerializeField] GameObject debugBattleUI;

    // Tween
    private Vector3 targetPosition;
    private float duration = 0.25f;

    // Camera Helper
    [SerializeField] public GameObject middle;

    private void Start()
    {
        referenceManager = StandardFunctions.FindReferenceManager();

        //*****NEED TO MAKE MULTIPLE
        player = referenceManager.Player;

        // IF not 4 enemies, handled in Initiate Battle Engine
        Enemy[] enemies = { enemy1, enemy2, enemy3, enemy4 };

        enemy1PositionRenderer = enemyPosition1.GetComponent<SpriteRenderer>();
        enemy2PositionRenderer = enemyPosition2.GetComponent<SpriteRenderer>();
        enemy3PositionRenderer = enemyPosition3.GetComponent<SpriteRenderer>();
        enemy4PositionRenderer = enemyPosition4.GetComponent<SpriteRenderer>();

        enemy1PositionRenderer.enabled = false;
        enemy2PositionRenderer.enabled = false;
        enemy3PositionRenderer.enabled = false;
        enemy4PositionRenderer.enabled = false;

        player1PositionRenderer = playerPosition1.GetComponent<SpriteRenderer>();
        player2PositionRenderer = playerPosition2.GetComponent<SpriteRenderer>();
        player3PositionRenderer = playerPosition3.GetComponent<SpriteRenderer>();
        player4PositionRenderer = playerPosition4.GetComponent<SpriteRenderer>();

        player1PositionRenderer.enabled = false;
        player2PositionRenderer.enabled = false;
        player3PositionRenderer.enabled = false;
        player4PositionRenderer.enabled = false;

        middle.GetComponent<SpriteRenderer>().enabled = false;

        enemyAmount = enemies.Count(enemy => enemy != null);
        Debug.Log("BattleContext::Start-EnemyAmount: " + enemyAmount);
    }
    public void PlaceFighters()
    {
        if (enemyAmount == 1)
        {
            //enemy1.transform.position = enemyPosition2.transform.position;
            enemy1.transform.DOMove(enemyPosition2.transform.position, duration).SetEase(Ease.OutQuad);
            enemy1.battlePosition = enemyPosition2.transform.position;
        }
        else if (enemyAmount == 2)
        {
            enemy1.transform.DOMove(enemyPosition2.transform.position, duration).SetEase(Ease.OutQuad);
            enemy1.battlePosition = enemyPosition2.transform.position;
            enemy2.transform.DOMove(enemyPosition3.transform.position, duration).SetEase(Ease.OutQuad);
            enemy2.battlePosition = enemyPosition3.transform.position;
        }
        else if (enemyAmount == 3)
        {
            enemy1.transform.DOMove(enemyPosition1.transform.position, duration).SetEase(Ease.OutQuad);
            enemy1.battlePosition = enemyPosition1.transform.position;
            enemy2.transform.DOMove(enemyPosition2.transform.position, duration).SetEase(Ease.OutQuad);
            enemy2.battlePosition = enemyPosition2.transform.position;
            enemy3.transform.DOMove(enemyPosition3.transform.position, duration).SetEase(Ease.OutQuad);
            enemy3.battlePosition = enemyPosition3.transform.position;
        }
        else
        {
            enemy1.transform.DOMove(enemyPosition1.transform.position, duration).SetEase(Ease.OutQuad);
            enemy1.battlePosition = enemyPosition1.transform.position;
            enemy2.transform.DOMove(enemyPosition2.transform.position, duration).SetEase(Ease.OutQuad);
            enemy2.battlePosition = enemyPosition2.transform.position;
            enemy3.transform.DOMove(enemyPosition3.transform.position, duration).SetEase(Ease.OutQuad);
            enemy3.battlePosition = enemyPosition3.transform.position;
            enemy4.transform.DOMove(enemyPosition4.transform.position, duration).SetEase(Ease.OutQuad);
            enemy4.battlePosition = enemyPosition4.transform.position;
        }

        if (playerAmount == 1)
        {
            //player.transform.position = playerPosition2.transform.position;
            player.transform.DOMove(playerPosition2.transform.position, duration).SetEase(Ease.OutQuad);
            player.battlePosition = playerPosition2.transform.position;
        }
    }

    public void PlaceCamera()
    {
        referenceManager.cinemachineVirtualCamera.Follow = middle.transform;
        referenceManager.cinemachineVirtualCamera.m_Lens.OrthographicSize = 4f;
    }

    public void InitiateBattleEngine()
    {
        GameStateManager gameStateManager = referenceManager.GameStateManager;
        gameStateManager.SetGameState(EGameState.BATTLE);
        PlaceFighters();
        PlaceCamera();

        Player[] players = { player };
        Enemy[] enemies = { enemy1, enemy2, enemy3, enemy4 };

        // Create a list to store non-null enemies
        List<Enemy> validEnemies = new List<Enemy>();

        // Loop through enemies and add only non-null ones
        foreach (Enemy enemy in enemies)
        {
            if (enemy != null)
            {
                validEnemies.Add(enemy);
            }
        }

        // Convert the validEnemies list back to an array
        Enemy[] nonNullEnemies = validEnemies.ToArray();

        // Create and Initiate the Battle Engine Object
        GameObject battleEngine = Instantiate(BattleEnginePrefab);
        battleEngine.GetComponent<BattleEngine>().SetupBattleEngine(
            players, nonNullEnemies, debugBattleUI,
            playerPosition1, playerPosition2, playerPosition3, playerPosition4,
            enemyPosition1, enemyPosition2, enemyPosition3, enemyPosition4
        );
    }
}
