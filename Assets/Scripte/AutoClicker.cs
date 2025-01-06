using UnityEngine;

public class AutoClicker : MonoBehaviour
{
    public SpeedManager speedManager;  // Référence au script SpeedManager
    public float clickInterval = 1f;   // Intervalle de temps entre chaque clic automatique (en secondes)
    public bool isAutoClickerActive = true;  // Booléen pour activer/désactiver l'auto-clic

    private float timer = 0f;

    void Update()
    {
        if (isAutoClickerActive)
        {
            timer += Time.deltaTime;

            if (timer >= clickInterval)
            {
                speedManager.OnPedalClick();  // Simule un clic sur le pédalier
                timer = 0f;  // Réinitialise le timer
            }
        }
    }

    // Fonction pour activer/désactiver l'auto-clic
    public void ToggleAutoClicker()
    {
        isAutoClickerActive = !isAutoClickerActive;
    }
}
