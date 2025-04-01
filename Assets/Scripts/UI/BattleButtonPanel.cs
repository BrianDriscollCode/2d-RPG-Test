using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BattleButtonsPanel : MonoBehaviour
{
    [SerializeField] public Button attackButton;
    [SerializeField] public Button defendButton;
    [SerializeField] public Button magicButton;
    [SerializeField] public Button inventoryButton;

    [SerializeField] public GameObject hand1;
    [SerializeField] public GameObject hand2;
    [SerializeField] public GameObject hand3;
    [SerializeField] public GameObject hand4;

    public Button selectedButton; 

    public Color filledWhite;
    public Color halfWhite;
    public Color transparentWhite;

    public BattleEngine battleEngine;

    public int buttonPointer = 0;

    private void Start()
    { 
        filledWhite = new Color(1f, 1f, 1f, 1f);
        halfWhite = new Color(1f, 1f, 1f, 0.5f);
        transparentWhite = new Color(1f, 1f, 1f, 0f);

        hand1.GetComponent<Image>().color = filledWhite;
        hand2.GetComponent<Image>().color = transparentWhite;
        hand3.GetComponent<Image>().color = transparentWhite;
        hand4.GetComponent<Image>().color = transparentWhite;

        selectedButton = attackButton;
    }

    private void Update()
    {
        
    }

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
