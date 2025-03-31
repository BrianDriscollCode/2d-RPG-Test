using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string characterName;
    public int maxHealth;
    public int currentHealth;
    protected AttackAbility currentMove;
    protected GameObject CurrentTarget; 

    public void InitializeCharacter(string characterName, int maxHealth, int currentHealth)
    {
        this.characterName = characterName;
        this.maxHealth = maxHealth;
        this.currentHealth = currentHealth;
        this.currentMove = null;
    }

    public virtual void TakeDamage(int amount)
    {
        currentHealth -= amount;
        Debug.Log($"{characterName} took {amount} damage. HP left: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        Debug.Log($"{characterName} has been defeated.");
        Destroy(gameObject); // Remove character from scene
    }

    public void SetCurrentMove(AttackAbility move)
    {
        this.currentMove = move;
    }

    public void SetCurrentTarget(GameObject target)
    {
        this.CurrentTarget = target;
    }
}

