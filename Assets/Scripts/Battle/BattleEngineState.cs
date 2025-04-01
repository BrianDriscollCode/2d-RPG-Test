using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface BattleEngineState
{
    void EnterState(BattleEngine battleEngine);
    void Update(BattleEngine battleEngine, float deltaTime);
    void FixedUpdate(BattleEngine battleEngine, float deltaTime);
    void ExitState(BattleEngine battleEngine);
}
