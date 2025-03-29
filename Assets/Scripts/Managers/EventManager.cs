using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventManager : MonoBehaviour
{
    public static event UnityAction StartBattle;
    public static void OnStartBattle() => StartBattle?.Invoke();

    public static event UnityAction EndBattle;
    public static void OnEndBattle() => EndBattle?.Invoke();
}
