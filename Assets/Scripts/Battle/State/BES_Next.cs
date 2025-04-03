using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BES_Next : BattleEngineState
{
    public void EnterState(BattleEngine battleEngine)
    {
        battleEngine.NextTurn();
    }
    public void Update(BattleEngine battleEngine, float deltaTime) { }
    public void FixedUpdate(BattleEngine battleEngine, float deltaTime) { }
    public void ExitState(BattleEngine battleEngine) { }
}
