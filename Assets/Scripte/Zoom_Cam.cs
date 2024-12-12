using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Camera mainCamera; // Référence à la caméra principale
    public float zoomSpeed = 10f; // Vitesse de zoom
    public float minZoom = 5f; // Zoom minimum
    public float maxZoom = 20f; // Zoom maximum
    public float zoomStep = 2f; // Incrément de zoom
    public Vector3 initialPosition; // Position initiale de la caméra
    public float initialZoom; // Zoom initial de la caméra

    void Start()
    {
        // Enregistrer la position initiale et le zoom de la caméra
        initialPosition = mainCamera.transform.position;
        initialZoom = mainCamera.fieldOfView;
    }

    void Update()
    {
        // Vérifier si le joueur appuie sur le bouton de zoom
        if (Input.GetMouseButton(0))
        {
            // Récupérer la position de la souris
            Vector3 mousePosition = Input.mousePosition;
            // Convertir la position de la souris en coordonnées du monde
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, mainCamera.nearClipPlane));

            // Zoomer sur la position de la souris
            ZoomToPosition(worldPosition);
        }
        else
        {
            // Revenir à la position et au zoom initiaux
            ResetZoom();
        }
    }

    void ZoomToPosition(Vector3 position)
    {
        // Calculer la nouvelle position de la caméra
        Vector3 direction = position - mainCamera.transform.position;
        float distance = direction.magnitude;
        float zoomAmount = Mathf.Clamp(mainCamera.fieldOfView - zoomStep, minZoom, maxZoom);

        // Appliquer le zoom
        mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, zoomAmount, Time.deltaTime * zoomSpeed);

        // Déplacer la caméra vers la position de zoom
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, position, Time.deltaTime * zoomSpeed);
    }

    void ResetZoom()
    {
        // Revenir à la position et au zoom initiaux
        mainCamera.fieldOfView = Mathf.Lerp(mainCamera.fieldOfView, initialZoom, Time.deltaTime * zoomSpeed);
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, initialPosition, Time.deltaTime * zoomSpeed);
    }
}
