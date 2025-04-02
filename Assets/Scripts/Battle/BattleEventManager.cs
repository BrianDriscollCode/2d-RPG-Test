using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BattleEventManager : MonoBehaviour
{
    public static event UnityAction PlayerMoveSelected;
    public static void OnPlayerMoveSelected() => PlayerMoveSelected?.Invoke();

    public static event UnityAction PlayerMoveCompleted;
    public static void OnPlayerMoveCompleted() => PlayerMoveCompleted?.Invoke();
}
