using UnityEngine;
using TMPro;

public class DistanceManager : MonoBehaviour
{
    public float totalDistance = 0f;  // Distance totale parcourue
    public float moneyPerKm = 1f;     // Montant d'argent gagné par km parcouru
    public TextMeshProUGUI distanceText; // Texte pour afficher la distance

    private SpeedManager speedManager;  // Référence au SpeedManager
    private MoneyManager moneyManager;  // Référence au MoneyManager
    private float lastPaidDistance = 0f; // Distance pour laquelle de l'argent a déjà été payé

    void Start()
    {
        speedManager = FindObjectOfType<SpeedManager>();
        moneyManager = FindObjectOfType<MoneyManager>();

        if (moneyManager == null)
        {
            Debug.LogError("MoneyManager introuvable dans la scène !");
        }
    }

    void Update()
    {
        CalculateDistance();
        UpdateDistanceDisplay();
        RewardMoney();
    }

    // Calculer la distance parcourue
    void CalculateDistance()
    {
        if (speedManager != null && speedManager.currentSpeed > 0)
        {
            totalDistance += speedManager.currentSpeed * Time.deltaTime;
        }
    }

    // Mettre à jour l'affichage de la distance
    void UpdateDistanceDisplay()
    {
        if (distanceText != null)
        {
            float distanceInKm = totalDistance / 1000f;
            distanceText.text = "Distance: " + Mathf.Round(distanceInKm) + " km";
        }
    }

    // Récompenser de l'argent pour chaque km parcouru
    void RewardMoney()
    {
        float distanceInKm = totalDistance / 200f;

        if (distanceInKm >= lastPaidDistance + 1) // Ajoute de l'argent tous les 1 km
        {
            if (moneyManager != null)
            {
                moneyManager.AddMoney(moneyPerKm);
                lastPaidDistance += 1; // Met à jour la distance pour laquelle on a payé
            }
        }
    }
}
