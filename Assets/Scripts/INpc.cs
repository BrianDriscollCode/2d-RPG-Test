using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface INpc
{
    string GetName();

    void Interact(Player player);
}
