using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleButtonsPanel : MonoBehaviour
{
    [SerializeField] Button attackButton;

    public BattleEngine battleEngine;

    public void SetupBattleButtons(BattleEngine battleEngine)
    {
        this.battleEngine = battleEngine;
        attackButton.onClick.AddListener(OnAttackButtonClick);
    }

    private void OnAttackButtonClick()
    {
        battleEngine.test();
    }
}
