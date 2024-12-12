using UnityEngine;
using System.Collections.Generic;

public class ObjectActivator : MonoBehaviour
{
    public int objectID; // Identifiant unique pour chaque objet
    public List<GameObject> objectsToActivate; // Liste des objets à activer
    public List<GameObject> objectsToDeactivate; // Liste des objets à désactiver

    void OnEnable()
    {
        ObjectDestroyer.OnObjectDestroyed += ActivateAndDeactivateObjects;
    }

    void OnDisable()
    {
        ObjectDestroyer.OnObjectDestroyed -= ActivateAndDeactivateObjects;
    }

    void ActivateAndDeactivateObjects(int destroyedObjectID)
    {
        if (destroyedObjectID == objectID)
        {
            // Activer les objets dans la liste objectsToActivate
            foreach (GameObject obj in objectsToActivate)
            {
                if (obj != null)
                {
                    obj.SetActive(true);
                }
            }

            // Désactiver les objets dans la liste objectsToDeactivate
            foreach (GameObject obj in objectsToDeactivate)
            {
                if (obj != null)
                {
                    obj.SetActive(false);
                }
            }
        }
    }
}
