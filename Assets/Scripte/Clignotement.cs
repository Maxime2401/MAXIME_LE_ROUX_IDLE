using UnityEngine;
using UnityEngine.UI; // Nécessaire pour manipuler des UI Images

public class SmoothBlink : MonoBehaviour
{
    [Header("Configuration")]
    public float blinkSpeed = 2.0f; // Vitesse du clignotement (plus grand = plus rapide)
    public float minAlpha = 0.2f;   // Opacité minimale
    public float maxAlpha = 1.0f;   // Opacité maximale

    private Image imageComponent;   // Pour les UI Images
    private Renderer objectRenderer; // Pour les objets 3D (matériaux)
    private Material materialInstance; // Instance du matériau pour le Renderer

    void Start()
    {
        // Récupère le composant Image pour les objets UI
        imageComponent = GetComponent<Image>();

        // Pour les objets 3D, récupère le Renderer et crée une instance de matériau
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            materialInstance = objectRenderer.material;
        }
    }

    void Update()
    {
        // Calcule l'alpha (transparence) smooth avec PingPong
        float alpha = Mathf.Lerp(minAlpha, maxAlpha, Mathf.PingPong(Time.time * blinkSpeed, 1));

        // Applique l'alpha selon le type d'objet
        if (imageComponent != null)
        {
            // Pour les UI Images
            Color color = imageComponent.color;
            color.a = alpha;
            imageComponent.color = color;
        }
        else if (materialInstance != null)
        {
            // Pour les matériaux des objets 3D
            Color color = materialInstance.color;
            color.a = alpha;
            materialInstance.color = color;
        }
    }
}
