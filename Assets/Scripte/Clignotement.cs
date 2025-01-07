using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SimpleBlink : MonoBehaviour
{
    public float blinkSpeed = 2.0f; // Vitesse du clignotement
    public float minAlpha = 0.2f;  // Opacité minimale
    public float maxAlpha = 1.0f;  // Opacité maximale

    private Image imageComponent;

    void OnEnable()
    {
        imageComponent = GetComponent<Image>();
        StartCoroutine(BlinkCoroutine());
    }

    void OnDisable()
    {
        StopAllCoroutines(); // Arrête toutes les coroutines lorsque l'objet est désactivé
    }

    private IEnumerator BlinkCoroutine()
    {
        while (true)
        {
            for (float t = 0; t <= 1; t += Time.unscaledDeltaTime * blinkSpeed)
            {
                if (!gameObject.activeInHierarchy) yield break;
                SetAlpha(Mathf.Lerp(minAlpha, maxAlpha, t));
                yield return null;
            }

            for (float t = 1; t >= 0; t -= Time.unscaledDeltaTime * blinkSpeed)
            {
                if (!gameObject.activeInHierarchy) yield break;
                SetAlpha(Mathf.Lerp(minAlpha, maxAlpha, t));
                yield return null;
            }
        }
    }

    private void SetAlpha(float alpha)
    {
        if (imageComponent != null)
        {
            Color color = imageComponent.color;
            color.a = alpha;
            imageComponent.color = color;
        }
    }
}
