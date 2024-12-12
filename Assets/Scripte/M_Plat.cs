using UnityEngine;

public class RoadMovement : MonoBehaviour
{
    public float rotationSpeed = 10f; // Vitesse de base de la route

    // Fonction pour faire tourner la route en fonction de la vitesse
    public void RotateRoad(float speed)
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime * (speed / 100f));  // Ajuste la rotation en fonction de la vitesse
    }
}
