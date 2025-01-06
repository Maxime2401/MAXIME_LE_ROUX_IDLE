using UnityEngine;
using System;

public class ObjectDestroyer : MonoBehaviour
{
    public static event Action<int> OnObjectDestroyed;
    public int objectID; // Identifiant unique pour chaque objet

    void OnDestroy()
    {
        if (OnObjectDestroyed != null)
        {
            OnObjectDestroyed.Invoke(objectID);
        }
    }
}
