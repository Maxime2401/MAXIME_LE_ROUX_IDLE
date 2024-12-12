using UnityEngine;
using UnityEngine.EventSystems; // Pour les événements de souris

public class ObjectHoverDisplay : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject objectToShow;  // L'objet à afficher lorsque la souris survole cet objet

    void Start()
    {
        // Assure-toi que l'objet à afficher est caché au début
        if (objectToShow != null)
        {
            objectToShow.SetActive(false);
        }
    }

    // Cette méthode est appelée lorsque la souris entre dans la zone de l'objet
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (objectToShow != null)
        {
            // Afficher l'objet
            objectToShow.SetActive(true);
        }
    }

    // Cette méthode est appelée lorsque la souris quitte la zone de l'objet
    public void OnPointerExit(PointerEventData eventData)
    {
        if (objectToShow != null)
        {
            // Cacher l'objet
            objectToShow.SetActive(false);
        }
    }
}
