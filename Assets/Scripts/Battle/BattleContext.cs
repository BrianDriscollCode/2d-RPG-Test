using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class BattleContext : MonoBehaviour
{
    [SerializeField] GameObject playerPosition1;
    [SerializeField] GameObject playerPosition2;
    [SerializeField] GameObject playerPosition3;
    [SerializeField] GameObject playerPosition4;

    [SerializeField] Enemy1 enemy1;
    [SerializeField] Enemy1 enemy2;
    [SerializeField] Enemy1 enemy3;
    [SerializeField] Enemy1 enemy4;

    [SerializeField] GameObject enemyPosition1;
    [SerializeField] GameObject enemyPosition2;
    [SerializeField] GameObject enemyPosition3;
    [SerializeField] GameObject enemyPosition4;

    int playerAmount = 1;
    int enemyAmount = 0;

    Player player;

    ReferenceManager referenceManager;

    private void Start()
    {
        referenceManager = StandardFunctions.FindReferenceManager();
        player = referenceManager.Player;

        Enemy1[] enemies = { enemy1, enemy2, enemy3, enemy4 };

        enemyAmount = enemies.Count(enemy => enemy != null);
        Debug.Log("BattleContext::Start-EnemyAmount: " + enemyAmount);
    }
    public void PlaceFighters()
    {
        if (enemyAmount == 1)
        {
            enemy1.transform.position = enemyPosition2.transform.position;
        }
        else if (enemyAmount == 2)
        {
            enemy1.transform.position = enemyPosition2.transform.position;
            enemy2.transform.position = enemyPosition3.transform.position;
        }
        else if (enemyAmount == 3)
        {
            enemy1.transform.position = enemyPosition1.transform.position;
            enemy2.transform.position = enemyPosition2.transform.position;
            enemy3.transform.position = enemyPosition3.transform.position;
        }
        else
        {
            enemy1.transform.position = enemyPosition1.transform.position;
            enemy2.transform.position = enemyPosition2.transform.position;
            enemy3.transform.position = enemyPosition3.transform.position;
            enemy4.transform.position = enemyPosition4.transform.position;
        }

        if (playerAmount == 1)
        {
            player.transform.position = playerPosition2.transform.position;
        }
        
    }

    


}
