using System;
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
    Character currentParticipant;
    E_CharacterType characterType;

    int targetPointer = 0;

    bool renderArrow;

    Character currentTarget;
    

    public void EnterState(BattleEngine battleEngine) 
    {
        this.enemies = battleEngine.GetEnemies();
        this.players = battleEngine.GetPlayers();
        this.selectionArrow = battleEngine.GetSelectionArrow();
        this.currentParticipant = battleEngine.GetCurrentParticipant();
        this.characterType = GetCharacterType(currentParticipant);

        if (enemies == null) throw new NullReferenceException("Enemies are null in EnterState.");
        if (players == null) throw new NullReferenceException("Players are null in EnterState.");
        if (selectionArrow == null) throw new NullReferenceException("SelectionArrow is null in EnterState.");
        if (currentParticipant == null) throw new NullReferenceException("CurrentParticipant is null in EnterState.");
        
        renderArrow = true;

        ChangeTarget(battleEngine, targetPointer);
    }
    public void Update(BattleEngine battleEngine, float deltaTime) 
    {
        HandleTargetPointer(battleEngine);
        HandleSubmit(battleEngine);

    }
    public void FixedUpdate(BattleEngine battleEngine, float deltaTime) { }
    public void ExitState(BattleEngine battleEngine) 
    {
        GameObject.Destroy(currentArrow);
    }

    // Functionality
    private E_CharacterType GetCharacterType(Character localCharacter)
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

    // Toggle Arrow through enemies
    private void HandleTargetPointer(BattleEngine battleEngine)
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
        else if (characterType == E_CharacterType.ENEMY)
        {
            currentTarget = players[0];
            battleEngine.SetCurrentTarget(currentTarget);
            Debug.Log("BES_SelectTarget::ChangeTarget - currentTarget: " + currentTarget);
            battleEngine.changeState(battleEngine.BES_Move);
        }
        else
        {
            Debug.Log("BES_SelectTarget::ChangeTarget - Incorrect character type");
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
