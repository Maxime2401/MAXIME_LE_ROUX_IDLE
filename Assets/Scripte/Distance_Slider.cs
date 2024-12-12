using UnityEngine;
using UnityEngine.UI;

public class DistanceSliderManager : MonoBehaviour
{
    public Slider distanceSlider;     // Slider pour afficher la distance
    public DistanceManager distanceManager; // Référence au DistanceManager
    private float distanceGoal = 50f; // Objectif de distance initial

    void Start()
    {
        // Configurer le slider
        if (distanceSlider != null)
        {
            distanceSlider.minValue = 0;
            distanceSlider.maxValue = distanceGoal;
        }
    }

    void Update()
    {
        UpdateDistanceSlider();
        CheckDistanceGoal();
    }

    // Mettre à jour le slider de distance
    void UpdateDistanceSlider()
    {
        if (distanceSlider != null && distanceManager != null)
        {
            float distanceInKm = distanceManager.totalDistance / 1000f;
            distanceSlider.value = Mathf.Clamp(distanceInKm % distanceGoal, distanceSlider.minValue, distanceSlider.maxValue);
        }
    }

    // Vérifier si l'objectif de distance est atteint
    void CheckDistanceGoal()
    {
        if (distanceManager != null)
        {
            float distanceInKm = distanceManager.totalDistance / 1000f;

            if (distanceInKm >= distanceGoal)
            {
                // Objectif atteint, définir un nouvel objectif
                distanceGoal += 100f; // Augmenter l'objectif de 100 km
                distanceSlider.maxValue = distanceGoal; // Mettre à jour le slider
                Debug.Log("Nouvel objectif de distance: " + distanceGoal + " km");
            }
        }
    }
}
