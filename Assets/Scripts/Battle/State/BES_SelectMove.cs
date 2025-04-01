using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    private BattleButtonsPanel battleButtonsPanel;
    private GameObject pointingHand;

    private bool UI_Toggle = false;

    public void EnterState(BattleEngine battleEngine)
    {
        MonoBehaviour currentTarget = battleEngine.GetCurrentTurnParticipant();
        battleEngine.PointSelectionArrow(currentTarget);
        battleButtonsPanel = battleEngine.battleButtonPanel;

        pointingHand = GameObject.Instantiate(battleEngine.pointedHand, battleButtonsPanel.transform.position, Quaternion.identity);
        
    }
    public void Update(BattleEngine battleEngine, float deltaTime) 
    {
        handleUINavigation(battleEngine);
    }
    public void FixedUpdate(BattleEngine battleEngine, float deltaTime)
    {     
        
    }
    public void ExitState(BattleEngine battleEngine) { }

    private void handleUINavigation(BattleEngine battleEngine)
    {
        battleButtonsPanel = battleEngine.battleButtonPanel;

        if (Input.GetKeyDown(KeyCode.S) && battleButtonsPanel.buttonPointer < 3)
        {
            battleButtonsPanel.buttonPointer += 1;
            UI_Toggle = true;
        }
        else if (Input.GetKeyDown(KeyCode.W) && battleButtonsPanel.buttonPointer > 0)
        {
            battleButtonsPanel.buttonPointer -= 1;
            UI_Toggle = true;
        }

        if (battleButtonsPanel.buttonPointer == 0 && UI_Toggle)
        {
            battleButtonsPanel.selectedButton = battleButtonsPanel.attackButton;
            battleButtonsPanel.hand1.GetComponent<Image>().color = battleButtonsPanel.filledWhite;
            battleButtonsPanel.hand2.GetComponent<Image>().color = battleButtonsPanel.transparentWhite;
            battleButtonsPanel.hand3.GetComponent<Image>().color = battleButtonsPanel.transparentWhite;
            battleButtonsPanel.hand4.GetComponent<Image>().color = battleButtonsPanel.transparentWhite;
            UI_Toggle = false;
        }
        else if (battleButtonsPanel.buttonPointer == 1 && UI_Toggle)
        {
            battleButtonsPanel.selectedButton = battleButtonsPanel.defendButton;
            battleButtonsPanel.hand1.GetComponent<Image>().color = battleButtonsPanel.transparentWhite;
            battleButtonsPanel.hand2.GetComponent<Image>().color = battleButtonsPanel.filledWhite;
            battleButtonsPanel.hand3.GetComponent<Image>().color = battleButtonsPanel.transparentWhite;
            battleButtonsPanel.hand4.GetComponent<Image>().color = battleButtonsPanel.transparentWhite;
            UI_Toggle = false;
        }
        else if (battleButtonsPanel.buttonPointer == 2 && UI_Toggle)
        {
            battleButtonsPanel.selectedButton = battleButtonsPanel.magicButton;
            battleButtonsPanel.hand1.GetComponent<Image>().color = battleButtonsPanel.transparentWhite;
            battleButtonsPanel.hand2.GetComponent<Image>().color = battleButtonsPanel.transparentWhite;
            battleButtonsPanel.hand3.GetComponent<Image>().color = battleButtonsPanel.filledWhite;
            battleButtonsPanel.hand4.GetComponent<Image>().color = battleButtonsPanel.transparentWhite;
            UI_Toggle = false;
        }
        else if (battleButtonsPanel.buttonPointer == 3 && UI_Toggle)
        {
            battleButtonsPanel.selectedButton = battleButtonsPanel.inventoryButton;
            battleButtonsPanel.hand1.GetComponent<Image>().color = battleButtonsPanel.transparentWhite;
            battleButtonsPanel.hand2.GetComponent<Image>().color = battleButtonsPanel.transparentWhite;
            battleButtonsPanel.hand3.GetComponent<Image>().color = battleButtonsPanel.transparentWhite;
            battleButtonsPanel.hand4.GetComponent<Image>().color = battleButtonsPanel.filledWhite;
            UI_Toggle = false;
        }
        else
        {
            return;
        }
    }
}
