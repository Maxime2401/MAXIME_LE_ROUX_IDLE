using UnityEngine;

public class WheelRotation : MonoBehaviour
{
    public SpeedManager speedManager;  // Référence au gestionnaire de vitesse

    // Cette fonction sera appelée dans chaque mise à jour pour chaque roue.
    void Update()
    {
        // Si la vitesse est supérieure à 0, on fait tourner les roues
        if (speedManager != null && speedManager.currentSpeed > 0)
        {
            // Calcul de la vitesse de rotation de la roue
            float rotationAmount = 100f * Time.deltaTime * (speedManager.currentSpeed / speedManager.maxSpeed);

            // Appliquer la rotation de manière égale à toutes les roues
            transform.Rotate(Vector3.right, rotationAmount);  // Rotation autour de l'axe X pour les roues
        }
    }
}
