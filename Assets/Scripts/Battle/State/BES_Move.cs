using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class BES_Move : BattleEngineState
{
    MonoBehaviour currentParticipant;
    Character currentTarget;
    AttackAbility attackAbility;
    AnimationClip[] attackAnimation;

    bool runAnim = false;

    public void EnterState(BattleEngine battleEngine)
    {
        currentParticipant = battleEngine.GetCurrentParticipant();
        currentTarget = battleEngine.GetCurrentTarget();
        Debug.Log("BES_Move::EnterState - " + currentParticipant.GetComponent<Character>());

        attackAbility = currentParticipant.GetComponent<Character>().GetCurrentMove();

        Vector3 moveToPosition = currentTarget.transform.position + new Vector3(-0.5f, 0, 0);
        currentParticipant.transform
            .DOMove(moveToPosition, 0.5f)
            .SetEase(Ease.InOutCubic)
            .OnComplete(() =>
            {
                Debug.Log("Movement complete!");
                currentParticipant.GetComponent<CharacterAnimSimple>().EnableAttackAnimation();
                runAnim = true;
            });
        Debug.Log(attackAbility.clip[1]);


        BattleEventManager.PlayerMoveCompleted += () => ToggleRunAnimFalse(battleEngine);

    }
    public void Update(BattleEngine battleEngine, float deltaTime)
    {
        if (runAnim)
        {
            currentParticipant.GetComponent<CharacterAnimSimple>().PlayAttackAnimation(attackAbility.clip[1].name);
        }
    }
    public void FixedUpdate(BattleEngine battleEngine, float deltaTime) { }
    public void ExitState(BattleEngine battleEngine)
    {
        BattleEventManager.PlayerMoveCompleted -= () => ToggleRunAnimFalse(battleEngine);

    }

    public void ToggleRunAnimFalse(BattleEngine battleEngine)
    {
        Debug.Log("BES_Move::ToggleRunAnimFalse - Run");
        runAnim = false;
        currentParticipant.GetComponent<CharacterAnimSimple>().DisableAttackAnimation();

        currentParticipant.transform
            .DOMove(currentParticipant.transform.position, 0.01f)
            .SetEase(Ease.InOutCubic)
            .SetDelay(0.1f)
            .OnComplete(() =>
             {
                 currentParticipant.transform.DOMove(battleEngine.playerPosition2.transform.position, 0.5f)
                .SetEase(Ease.InOutCubic);
             });
    }
}

