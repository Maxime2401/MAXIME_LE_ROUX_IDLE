using UnityEngine;
using UnityEngine.UI;

public class PumpManager : MonoBehaviour
{
    public Slider pumpSlider; // Référence au Slider de la pompe
    public SpeedManager speedManager; // Référence au SpeedManager
    public float decelerationReduction = 4f; // Réduction de la décélération par pompage

    private float previousValue; // Valeur précédente du Slider

    void Start()
    {
        if (pumpSlider != null)
        {
            // Initialiser la valeur précédente
            previousValue = pumpSlider.value;

            // Ajouter un listener pour détecter les changements de valeur du Slider
            pumpSlider.onValueChanged.AddListener(OnPump);
        }
    }

    void OnPump(float value)
    {
        if (speedManager != null)
        {
            // Vérifier si le joueur pompe de haut en bas
            if (value < previousValue)
            {
                // Réduire la décélération
                speedManager.ReduceDeceleration(decelerationReduction);
            }

            // Mettre à jour la valeur précédente
            previousValue = value;
        }
    }
}
