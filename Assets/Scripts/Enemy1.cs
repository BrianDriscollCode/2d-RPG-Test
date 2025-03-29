using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1 : MonoBehaviour, IEnemy
{ 
    public string GetName()
    {
        return "test";
    }
    
    public void Interact(Player player)
    {
        Debug.Log("TEST INTERACT");
    }
}
