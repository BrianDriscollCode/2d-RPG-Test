using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface PlayerState
{
    void EnterState(Player player);
    void Update(Player player, float deltaTime);
    void FixedUpdate(Player player, float deltaTime);
    void ExitState(Player player);
}
