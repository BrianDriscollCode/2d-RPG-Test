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
    E_CharacterType currentType;

    BattleEngine battleEngineLocal;

    Vector3 offset;

    bool runAnim = false;

    public void EnterState(BattleEngine battleEngine)
    {
        battleEngineLocal = battleEngine;
        currentParticipant = battleEngine.GetCurrentParticipant();
        currentTarget = battleEngine.GetCurrentTarget();

        if (currentParticipant.TryGetComponent<Player>(out Player player))
        {
            currentType = player.characterType;
        }
        else if (currentParticipant.TryGetComponent<Enemy>(out Enemy enemy))
        {
            currentType = enemy.characterType;
        }
        else
        {
            Debug.LogError("currentParticipant is neither Player nor Enemy!");
        }


        Debug.Log("BES_Move::EnterState - CurrentType: " + currentType);
        if (currentType == E_CharacterType.ENEMY)
        {
            offset = new Vector3(0.5f, 0, 0);

            Debug.Log("BES_Move::EnterState - " + currentParticipant.GetComponent<Character>());
        
            attackAbility = currentParticipant.GetComponent<Character>().GetCurrentMove();

            Vector3 moveToPosition = currentTarget.transform.position + offset;
            currentParticipant.transform
                .DOMove(moveToPosition, 0.5f)
                .SetEase(Ease.InOutCubic)
                .OnComplete(() =>
                {
                    Debug.Log("Movement complete!");
                    // Turns off bool flags in CharacterAnimSimple to disable current
                    // anims and enable attack anims
                    currentParticipant.GetComponent<EnemyAnimation>().EnableAttackAnimation();

                    // Enable anim in Update to run
                    runAnim = true;
                });
            
        }
        else if (currentType == E_CharacterType.PLAYER)
        {
            offset = new Vector3(-0.5f, 0, 0);

            Debug.Log("BES_Move::EnterState - " + currentParticipant.GetComponent<Character>());

            attackAbility = currentParticipant.GetComponent<Character>().GetCurrentMove();

            Vector3 moveToPosition = currentTarget.transform.position + offset;
            currentParticipant.transform
                .DOMove(moveToPosition, 0.5f)
                .SetEase(Ease.InOutCubic)
                .OnComplete(() =>
                {
                    Debug.Log("Movement complete!");
                    // Turns off bool flags in CharacterAnimSimple to disable current
                    // anims and enable attack anims
                    currentParticipant.GetComponent<CharacterAnimSimple>().EnableAttackAnimation();

                    // Enable anim in Update to run
                    runAnim = true;
                });
        }

        BattleEventManager.PlayerMoveCompleted += ToggleRunAnimFalse;
    }
    public void Update(BattleEngine battleEngine, float deltaTime)
    {
        if (runAnim && currentType == E_CharacterType.PLAYER)
        {
            // Contains animation event to signal completion
            currentParticipant.GetComponent<CharacterAnimSimple>().PlayAttackAnimation(attackAbility.clip[1].name);
        }
        else if (runAnim && currentType == E_CharacterType.ENEMY)
        {
            currentParticipant.GetComponent<EnemyAnimation>().PlayAttackAnimation(attackAbility.clip[0].name);
        }
        else
        {
            return;
        }
    }
    public void FixedUpdate(BattleEngine battleEngine, float deltaTime) { }
    public void ExitState(BattleEngine battleEngine)
    {
        BattleEventManager.PlayerMoveCompleted -= ToggleRunAnimFalse;
    }

    // Runs when animation event on attackAnim finishes
    public void ToggleRunAnimFalse()
    {
        Debug.Log("BES_Move::ToggleRunAnimFalse - Run");
        runAnim = false;

        if (currentType == E_CharacterType.PLAYER)
        {
            currentParticipant.GetComponent<CharacterAnimSimple>().DisableAttackAnimation();
        }
        else if (currentType == E_CharacterType.ENEMY)
        {
            currentParticipant.GetComponent<EnemyAnimation>().DisableAttackAnimation();
        }
        else
        {
            Debug.Log("BES_Move::ToggleRunAnimFalse - incorrect character type: " + currentType);
        }

        currentParticipant.transform
            .DOMove(currentParticipant.transform.position, 0.01f)
            .SetEase(Ease.InOutCubic)
            .SetDelay(0.1f)
            .OnComplete(() =>
             {
                 currentParticipant.transform
                    .DOMove(currentParticipant.GetComponent<Character>().battlePosition, 0.5f)
                    .SetEase(Ease.InOutCubic)
                    .SetDelay(0.1f)
                    .OnComplete(() =>
                    {
                        battleEngineLocal.changeState(battleEngineLocal.BES_Next);
                    });
             });
    }
}

