using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;


[CreateAssetMenu(fileName = "NewMove", menuName = "Attack System/Attacks")]
public class AttackAbility : ScriptableObject
{
    public string moveName;
    public int damage;       
    public int manaCost;     
    public MoveType moveType;
    public enum MoveType
    {
        MEELE,
        MAGIC,
        RANGED
    }
    public AnimationClip[] clip;

    public void Execute(Character user, Character target)
    {
        Debug.Log($"{user.characterName} used {moveName} on {target.characterName}");
        target.TakeDamage(damage);
    }
}
