using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;


[CreateAssetMenu(fileName = "NewMove", menuName = "Move System/Move")]
public class PlayerMoveSet : ScriptableObject
{
    public string moveName;  // Name of the move (e.g., "Fireball")
    public int damage;       // How much damage the move does
    public int manaCost;     // How much mana the move costs
    
    public enum MoveType
    {
        MEELE,
        MAGIC,
        RANGED
    }

    public MoveType moveType;
    public AnimationClip clip;

    public void Execute(Character user, Character target)
    {
        Debug.Log($"{user.characterName} used {moveName} on {target.characterName}");
        target.TakeDamage(damage);
    }
}
