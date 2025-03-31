using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public string characterName;
    public int maxHealth;
    public int currentHealth;


    public void InitializeCharacter(string characterName, int maxHealth, int currentHealth)
    {
        this.characterName = characterName;
        this.maxHealth = maxHealth;
        this.currentHealth = currentHealth;
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
}

