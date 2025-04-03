using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class BES_SelectMove : BattleEngineState
{
    enum ButtonChosen
    {
        ATTACK,
        DEFEND,
        MAGIC,
        INVENTORY
    }

    private GameObject pointingHand;

    private E_CharacterType characterType;

    private bool UI_Initial_Generation = false;

    public void EnterState(BattleEngine battleEngine)
    {
        Character currentTarget = battleEngine.GetCurrentTurnParticipant();
        characterType = GetCharacterType(currentTarget);

        battleEngine.StartCoroutine(SelectMove(battleEngine, characterType));

    }
    public void Update(BattleEngine battleEngine, float deltaTime) 
    {
        HandleUINavigation(battleEngine);

    }
    public void FixedUpdate(BattleEngine battleEngine, float deltaTime)
    {     
        
    }
    public void ExitState(BattleEngine battleEngine) { }

    private void HandleUINavigation(BattleEngine battleEngine)
    {
        BattleButtonsPanel battleButtonsPanel = battleEngine.GetBattleButtonsPanel();

        // Reset initial generation if we are back to the player and the UI hasn't been initialized yet
        if (characterType == E_CharacterType.PLAYER && !UI_Initial_Generation)
        {
            UI_Initial_Generation = true;
            battleButtonsPanel.hand1.GetComponent<Image>().color = battleButtonsPanel.transparentWhite;
            battleButtonsPanel.hand2.GetComponent<Image>().color = battleButtonsPanel.transparentWhite;
            battleButtonsPanel.hand3.GetComponent<Image>().color = battleButtonsPanel.transparentWhite;
            battleButtonsPanel.hand4.GetComponent<Image>().color = battleButtonsPanel.transparentWhite;
        }

        // Skip updating UI if the character is not a player
        if (characterType != E_CharacterType.PLAYER)
        {
            battleButtonsPanel.hand1.GetComponent<Image>().color = battleButtonsPanel.transparentWhite;
            battleButtonsPanel.hand2.GetComponent<Image>().color = battleButtonsPanel.transparentWhite;
            battleButtonsPanel.hand3.GetComponent<Image>().color = battleButtonsPanel.transparentWhite;
            battleButtonsPanel.hand4.GetComponent<Image>().color = battleButtonsPanel.transparentWhite;
            return;
        }

        // Handle button navigation logic
        if (Input.GetKeyDown(KeyCode.S)) // Move down (increase button pointer)
        {
            if (battleButtonsPanel.buttonPointer < 3) // Ensure buttonPointer doesn't go beyond the last button
            {
                battleButtonsPanel.buttonPointer += 1;
                UpdateButtonHighlight(battleButtonsPanel); // Update the button highlight
            }
        }
        else if (Input.GetKeyDown(KeyCode.W)) // Move up (decrease button pointer)
        {
            if (battleButtonsPanel.buttonPointer > 0) // Ensure buttonPointer doesn't go below 0
            {
                battleButtonsPanel.buttonPointer -= 1;
                UpdateButtonHighlight(battleButtonsPanel); // Update the button highlight
            }
        }

        // Handle UI updates based on the selected button
        UpdateButtonHighlight(battleButtonsPanel);
    }

    private void MakePointingHandTransparent(BattleEngine battleEngine)
    {
        BattleButtonsPanel battleButtonsPanel = battleEngine.GetBattleButtonsPanel();

        battleButtonsPanel.hand1.GetComponent<Image>().color = battleButtonsPanel.transparentWhite;
        battleButtonsPanel.hand2.GetComponent<Image>().color = battleButtonsPanel.transparentWhite;
        battleButtonsPanel.hand3.GetComponent<Image>().color = battleButtonsPanel.transparentWhite;
        battleButtonsPanel.hand4.GetComponent<Image>().color = battleButtonsPanel.transparentWhite;
    }

    private void UpdateButtonHighlight(BattleButtonsPanel battleButtonsPanel)
    {
        // Reset all hands' colors to transparent
        battleButtonsPanel.hand1.GetComponent<Image>().color = battleButtonsPanel.transparentWhite;
        battleButtonsPanel.hand2.GetComponent<Image>().color = battleButtonsPanel.transparentWhite;
        battleButtonsPanel.hand3.GetComponent<Image>().color = battleButtonsPanel.transparentWhite;
        battleButtonsPanel.hand4.GetComponent<Image>().color = battleButtonsPanel.transparentWhite;

        // Update hands' colors based on buttonPointer
        if (battleButtonsPanel.buttonPointer == 0)
        {
            battleButtonsPanel.selectedButton = battleButtonsPanel.attackButton;
            battleButtonsPanel.hand1.GetComponent<Image>().color = battleButtonsPanel.filledWhite;
        }
        else if (battleButtonsPanel.buttonPointer == 1)
        {
            battleButtonsPanel.selectedButton = battleButtonsPanel.defendButton;
            battleButtonsPanel.hand2.GetComponent<Image>().color = battleButtonsPanel.filledWhite;
        }
        else if (battleButtonsPanel.buttonPointer == 2)
        {
            battleButtonsPanel.selectedButton = battleButtonsPanel.magicButton;
            battleButtonsPanel.hand3.GetComponent<Image>().color = battleButtonsPanel.filledWhite;
        }
        else if (battleButtonsPanel.buttonPointer == 3)
        {
            battleButtonsPanel.selectedButton = battleButtonsPanel.inventoryButton;
            battleButtonsPanel.hand4.GetComponent<Image>().color = battleButtonsPanel.filledWhite;
        }
    }

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

    private IEnumerator SelectMove(BattleEngine battleEngine, E_CharacterType type)
    {
        if (type == E_CharacterType.PLAYER)
        {
            Debug.Log("BES_SelectMove::SelectMove- FOUND PLAYER");
            // do something
            Player player = battleEngine.GetCurrentParticipant().GetComponent<Player>();

            bool moveSelected = false;
            void OnMoveSelected() => moveSelected = true;

            BattleEventManager.PlayerMoveSelected += OnMoveSelected;

            yield return new WaitUntil(() => moveSelected);

            BattleEventManager.PlayerMoveSelected -= OnMoveSelected;

            player.SetMove();
            MakePointingHandTransparent(battleEngine);
            battleEngine.changeState(battleEngine.BES_SelectTarget);
        }
        else if (type == E_CharacterType.ENEMY)
        {
            Debug.Log("BES_SelectMove::SelectMove- FOUND ENEMY");
            Enemy enemy = battleEngine.GetCurrentParticipant().GetComponent<Enemy>();
            enemy.SetMove();
            battleEngine.changeState(battleEngine.BES_SelectTarget);
            
        }
    }
}
