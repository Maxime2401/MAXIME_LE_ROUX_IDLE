using UnityEngine;
using System.Collections;
public class RotateY : MonoBehaviour
{
    // Vitesse de rotation en degrés par frame
    public float rotationSpeed = 1f;

    // Axe de rotation (X, Y ou Z)
    public Vector3 rotationAxis = Vector3.up;

    void Start()
    {
        // Générer une vitesse aléatoire
        rotationSpeed = Random.Range(1f, 10f);

        // Démarrer la coroutine pour la rotation
        StartCoroutine(RotateCoroutine());
    }

    IEnumerator RotateCoroutine()
    {
        while (true)
        {
            // Faire tourner l'objet autour de l'axe choisi
            transform.Rotate(rotationAxis * rotationSpeed);
            yield return null; // Attendre le prochain frame
        }
    }
}
