using UnityEngine;

public class ObjectDeactivateHandler : MonoBehaviour
{
    private SpeedManager speedManager;  // Référence au script SpeedManager
    public float increaseAmount = 20f;  // Valeur à ajouter à chainePerformance, modifiable dans l'inspecteur
    public float performanceMax = 20f;
    public float chanceToIncreaseDeceleration = 0.1f; // Pourcentage de chance (0.1 = 10%)
    public float minDeceleration = 0f;
    public float deceleration=0f;
    void Start()
    {
        // Trouver l'instance de SpeedManager dans la scène
        speedManager = FindObjectOfType<SpeedManager>();
    }

    void OnDestroy()
    {
        // Vérifier que le SpeedManager a bien été trouvé
        if (speedManager != null)
        {
            // Ajouter increaseAmount à chainePerformance quand cet objet est détruit
            speedManager.decelerationRate+=deceleration;
            speedManager.minDecelerationRate+=minDeceleration;
            speedManager.chainePerformance += increaseAmount;
            speedManager.whellsPerformanceMax += performanceMax;
            speedManager.chanceToIncreaseDeceleration += chanceToIncreaseDeceleration;
            Debug.Log("Chaine Performance augmentée de " + increaseAmount + ". Nouvelle valeur : " + speedManager.chainePerformance);
            Debug.Log("Performance Max augmentée de " + performanceMax + ". Nouvelle valeur : " + speedManager.whellsPerformanceMax);
            Debug.Log("Chance d'augmentation de la décélération augmentée de " + chanceToIncreaseDeceleration + ". Nouvelle valeur : " + speedManager.chanceToIncreaseDeceleration);
        }
        else
        {
            Debug.LogWarning("SpeedManager non trouvé !");
        }
    }
}
