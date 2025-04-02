using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BES_SelectTarget : BattleEngineState
{

    Enemy[] enemies;
    Player[] players;
    GameObject selectionArrow;
    GameObject currentArrow;
    MonoBehaviour currentParticipant;
    E_CharacterType characterType;

    int targetPointer = 0;

    bool renderArrow = true;

    Character currentTarget;
    

    public void EnterState(BattleEngine battleEngine) 
    {
        this.enemies = battleEngine.GetEnemies();
        this.players = battleEngine.GetPlayers();
        this.selectionArrow = battleEngine.GetSelectionArrow();
        this.currentParticipant = battleEngine.GetCurrentParticipant();
        this.characterType = GetCharacterType(currentParticipant);

        ChangeTarget(battleEngine, targetPointer);

    }
    public void Update(BattleEngine battleEngine, float deltaTime) 
    {
        HandleUIFunctionality(battleEngine);
        HandleSubmit(battleEngine);

    }
    public void FixedUpdate(BattleEngine battleEngine, float deltaTime) { }
    public void ExitState(BattleEngine battleEngine) 
    {
        GameObject.Destroy(currentArrow);
    }

    // Functionality
    private E_CharacterType GetCharacterType(MonoBehaviour localCharacter)
    {
        if (localCharacter.TryGetComponent<Player>(out Player player))
        {
            return player.characterType;
        }
        else if (localCharacter.TryGetComponent<Enemy>(out Enemy enemy))
        {
            return enemy.characterType;
        }
        else
        {
            Debug.LogError("BES_SelectMove - Incorrect character type");
            return E_CharacterType.NONE;
        }
    }

    private void HandleUIFunctionality(BattleEngine battleEngine)
    {
        if (Input.GetKeyDown(KeyCode.S) && targetPointer < enemies.Length - 1)
        {
            targetPointer += 1;
            renderArrow = true;
            ChangeTarget(battleEngine, targetPointer);
        }
        else if (Input.GetKeyDown(KeyCode.W) && targetPointer > 0)
        {
            targetPointer -= 1;
            renderArrow = true;
            ChangeTarget(battleEngine, targetPointer);
        }

        if (characterType == E_CharacterType.PLAYER && renderArrow)
        {
            GameObject.Destroy(currentArrow);
            Vector3 arrowPosition = enemies[targetPointer].transform.position + new Vector3(0, 0.5f, 0);

            currentArrow = GameObject.Instantiate(selectionArrow, arrowPosition, Quaternion.identity);
            renderArrow = false;
        }
    }

    private void ChangeTarget(BattleEngine battleEngine, int targetPointer)
    {
        if (characterType == E_CharacterType.PLAYER)
        {
            currentTarget = enemies[targetPointer];
            battleEngine.SetCurrentTarget(currentTarget);
        }
    }

    private void HandleSubmit(BattleEngine battleEngine)
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            battleEngine.changeState(battleEngine.BES_Move);
        }
    }
}
