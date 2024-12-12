using UnityEngine;
using TMPro;  // Importer TextMeshPro
using UnityEngine.UI; // Importer UI pour l'image

public class SpeedManager : MonoBehaviour
{
    public float currentSpeed = 0f;         // Vitesse actuelle (en unités Unity/sec)
    public float maxSpeed = 100f;           // Vitesse maximale
    public float accelerationAmount = 5f ;  // Montant d'augmentation de la vitesse à chaque clic
    public float decelerationRate = 5f;     // Taux de décélération (diminution de la vitesse quand le joueur ne clique pas)
    public float minDecelerationRate = 10f; // Décélération minimale
    public float maxDecelerationRate = 50f; // Décélération maximale pour l'avertissement
    public float wheelsPerformance = 1f;
    public float whellsPerformanceMax = 0f;
    public float chainePerformance = 1f;
    public RoadMovement road;               // Référence à la route
    public Transform[] wheels;              // Référence aux roues
    public float wheelRotationSpeed = 100f; // Facteur de vitesse pour la rotation des roues

    public TextMeshProUGUI speedometerText; // Référence au texte du compteur de vitesse (TextMeshPro)
    public float chanceToIncreaseDeceleration = 0.1f; // Pourcentage de chance (0.1 = 10%)
    public Image warningImage; // Référence à l'image d'avertissement
    public Color minDecelerationColor = Color.green; // Couleur pour la décélération minimale
    public Color maxDecelerationColor = Color.red;   // Couleur pour la décélération maximale

    private bool hasSpeedChanged = false;
    private float timeSinceLastCheck = 0f; // Temps écoulé depuis la dernière vérification

    void Update()
    {
        // Mettre à jour le temps écoulé
        timeSinceLastCheck += Time.deltaTime;

        // Vérifier si 30 secondes se sont écoulées
        if (timeSinceLastCheck >= 30f)
        {
            // Vérifier si la décélération doit augmenter
            if (Random.value < chanceToIncreaseDeceleration)
            {
                decelerationRate += 40f;
            }
            timeSinceLastCheck = 0f; // Réinitialiser le compteur de temps
        }

        // Appliquer la décélération uniquement si la vitesse est positive
        if (currentSpeed > 0)
        {
            currentSpeed -= decelerationRate * Time.deltaTime;
            currentSpeed = Mathf.Max(currentSpeed, 0);  // Limite la vitesse à 0
            hasSpeedChanged = true;
        }

        // Activer ou désactiver l'image d'avertissement en fonction de la décélération
        if (warningImage != null)
        {
            warningImage.gameObject.SetActive(decelerationRate > minDecelerationRate);

            // Interpoler la couleur de l'image d'avertissement en fonction de la décélération
            float t = Mathf.InverseLerp(minDecelerationRate, maxDecelerationRate, decelerationRate);
            warningImage.color = Color.Lerp(minDecelerationColor, maxDecelerationColor, t);
        }

        // Ne met à jour la route et les roues que si la vitesse a changé
        if (hasSpeedChanged)
        {
            // Applique la rotation de la route en fonction de la vitesse
            if (road != null)
                road.RotateRoad(currentSpeed);

            // Applique la rotation des roues en fonction de la vitesse
            foreach (Transform wheel in wheels)
            {
                if (wheel != null)
                    wheel.Rotate(Vector3.right, wheelRotationSpeed * Time.deltaTime * (currentSpeed / maxSpeed));
            }

            // Mettre à jour le compteur de vitesse
            UpdateSpeedometer();

            // Réinitialise le flag pour ne pas recalculer chaque frame
            hasSpeedChanged = false;
        }
    }

    // Fonction pour augmenter la vitesse lorsque le joueur clique sur le pédalier
    public void OnPedalClick()
    {
        // Ajoute un montant à la vitesse, mais ne dépasse jamais la vitesse maximale
        currentSpeed = Mathf.Min(currentSpeed + chainePerformance + accelerationAmount * wheelsPerformance, maxSpeed+ whellsPerformanceMax);
        hasSpeedChanged = true;  // Indique qu'une modification de la vitesse a eu lieu
    }

    // Mise à jour du compteur de vitesse (convertir la vitesse en km/h et l'afficher)
    void UpdateSpeedometer()
    {
        if (speedometerText != null)
        {
            // Conversion de la vitesse de Unity (en unités/sec) en km/h
            float speedInKmH = currentSpeed * 0.1f; // 1 unité/sec = 0.1 km/h
            speedometerText.text = "Vitesse: " + Mathf.Round(speedInKmH) + " km/h";  // Affichage
        }
    }

    // Réduire la décélération avec une limite minimale
    public void ReduceDeceleration(float amount)
    {
        decelerationRate = Mathf.Max(decelerationRate - amount, minDecelerationRate);
    }
}
