using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    string GetName();

    void Interact(Player player);
}
