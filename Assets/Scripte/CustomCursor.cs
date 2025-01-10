using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems; // Nécessaire pour l'utilisation des événements de souris

public class CustomCursor : MonoBehaviour
{
    public Image cursorImage; // Référence à l'image du curseur
    public Sprite defaultCursorSprite; // Curseur par défaut
    public Sprite buttonCursorSprite; // Curseur lorsqu'il survole un bouton

    public Vector2 hotspot = new Vector2(16, 16); // Le point d'ancrage du curseur

    private bool isOverButton = false; // Indicateur pour savoir si la souris est sur un bouton

    private void Start()
    {
        // Cache le curseur système par défaut
        Cursor.visible = false;

        // Assurez-vous que l'image du curseur ne bloque pas les événements UI
        cursorImage.raycastTarget = false;
        cursorImage.sprite = defaultCursorSprite; // Mettre le curseur par défaut

        // Trouver tous les boutons dans la scène
        Button[] buttons = FindObjectsOfType<Button>();

        // Ajouter des listeners pour les boutons
        foreach (Button button in buttons)
        {
            AddEventTriggers(button);
        }
    }

    private void Update()
    {
        // Suivre la position de la souris
        Vector2 mousePosition = Input.mousePosition;

        // Déplacer l'image du curseur en fonction de la position de la souris
        cursorImage.transform.position = mousePosition;

        // Ajuster le point d'ancrage du curseur (le hotspot)
        cursorImage.rectTransform.pivot = new Vector2(hotspot.x / cursorImage.rectTransform.rect.width, hotspot.y / cursorImage.rectTransform.rect.height);

        // Vérifier les boutons activés dynamiquement
        CheckForNewButtons();

        // Vérifier si le curseur est dans le bon état
        if (!isOverButton && cursorImage.sprite != defaultCursorSprite)
        {
            cursorImage.sprite = defaultCursorSprite;
        }
    }

    // Ajouter des événements de survol pour un bouton
    private void AddEventTriggers(Button button)
    {
        EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entryEnter = new EventTrigger.Entry();
        entryEnter.eventID = EventTriggerType.PointerEnter;
        entryEnter.callback.AddListener((eventData) => { OnPointerEnter(null); });
        trigger.triggers.Add(entryEnter);

        EventTrigger.Entry entryExit = new EventTrigger.Entry();
        entryExit.eventID = EventTriggerType.PointerExit;
        entryExit.callback.AddListener((eventData) => { OnPointerExit(null); });
        trigger.triggers.Add(entryExit);
    }

    // Lorsqu'on entre dans un bouton (ou un élément interactif)
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Change le curseur pour celui spécifique aux boutons
        cursorImage.sprite = buttonCursorSprite;
        isOverButton = true;
    }

    // Lorsqu'on quitte un bouton (ou un élément interactif)
    public void OnPointerExit(PointerEventData eventData)
    {
        // Remet le curseur par défaut
        cursorImage.sprite = defaultCursorSprite;
        isOverButton = false;
    }

    // Vérifier les boutons activés dynamiquement
    private void CheckForNewButtons()
    {
        Button[] buttons = FindObjectsOfType<Button>();

        foreach (Button button in buttons)
        {
            if (button.gameObject.activeInHierarchy && button.GetComponent<EventTrigger>() == null)
            {
                AddEventTriggers(button);
            }
        }
    }
}
