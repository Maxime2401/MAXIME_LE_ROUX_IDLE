using UnityEngine;

public class SmoothColorChangeByDistance : MonoBehaviour
{
    public float midDistanceThreshold = 5f; // Distance pour la transition intermédiaire
    public float finalDistanceThreshold = 10f; // Distance pour la transition finale

    public Color initialColor = Color.white; // Couleur initiale
    public Color midDistanceColor = Color.yellow; // Couleur intermédiaire
    public Color finalDistanceColor = Color.red; // Couleur finale

    public Renderer objectRenderer; // Renderer de l'objet

    private DistanceManager distanceManager; // Référence au DistanceManager

    void Start()
    {
        // Trouver le DistanceManager dans la scène
        distanceManager = FindObjectOfType<DistanceManager>();

        if (distanceManager == null)
        {
            Debug.LogError("DistanceManager introuvable dans la scène !");
        }

        // Vérifiez que l'objet a un Renderer
        if (objectRenderer == null)
        {
            Debug.LogError("Aucun Renderer assigné à SmoothColorChangeByDistance !");
        }
        else
        {
            // Initialiser la couleur de départ
            objectRenderer.material.color = initialColor;
        }
    }

    void Update()
    {
        UpdateObjectColorSmoothly();
    }

    void UpdateObjectColorSmoothly()
    {
        if (distanceManager == null || objectRenderer == null) return;

        float distanceInKm = distanceManager.totalDistance / 1000f; // Conversion en km

        if (distanceInKm >= finalDistanceThreshold)
        {
            // Si au-delà du seuil final, applique la couleur finale
            objectRenderer.material.color = finalDistanceColor;
        }
        else if (distanceInKm >= midDistanceThreshold)
        {
            // Interpoler entre la couleur intermédiaire et la couleur finale
            float t = (distanceInKm - midDistanceThreshold) / (finalDistanceThreshold - midDistanceThreshold);
            objectRenderer.material.color = Color.Lerp(midDistanceColor, finalDistanceColor, t);
        }
        else
        {
            // Interpoler entre la couleur initiale et la couleur intermédiaire
            float t = distanceInKm / midDistanceThreshold;
            objectRenderer.material.color = Color.Lerp(initialColor, midDistanceColor, t);
        }
    }
}
