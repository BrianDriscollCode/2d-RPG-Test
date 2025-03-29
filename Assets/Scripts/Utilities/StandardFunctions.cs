using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StandardFunctions
{
    public static ReferenceManager FindReferenceManager()
    {
        ReferenceManager referenceManager = GameObject.FindGameObjectWithTag("ReferenceManager").GetComponent<ReferenceManager>();

        if (referenceManager == null)
        {
            Debug.LogWarning("StandardFunctions::FindReferenceManager - ReferenceManager object not found in scene.");
        }
        else
        {
            Debug.Log("StandardFunctions::FindReferenceManager - Found ReferenceManager object: " + referenceManager.name);
        }

        return referenceManager;
    }
}
