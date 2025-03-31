using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleUI : MonoBehaviour
{

    [SerializeField] private BattleButtonsPanel battleButtonsPanel; // Private field

    public BattleButtonsPanel GetBattleButtonsPanel { get => battleButtonsPanel; private set => battleButtonsPanel = value; }
}
