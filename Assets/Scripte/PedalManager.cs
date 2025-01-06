using UnityEngine;
using UnityEngine.UI;  // Pour les interactions avec l'UI

public class PedalManager : MonoBehaviour
{
    public SpeedManager speedManager;  // Référence au gestionnaire de vitesse

    // Fonction appelée lorsque le joueur appuie sur le pédalier (clic)
    public void OnPedalClick()
    {
        if (speedManager != null)
        {
            speedManager.OnPedalClick();  // Augmente la vitesse d'un clic
        }
    }
}
