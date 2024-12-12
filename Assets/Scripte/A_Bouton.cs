using UnityEngine;

public class ButtonClickHandler : MonoBehaviour
{
    // Référence vers l'image à activer/désactiver
    public GameObject imageToToggle;

    // Méthode appelée lorsque le bouton est cliqué
    public void OnButtonClick()
    {
        if (imageToToggle != null)
        {
            // Inverse l'état actif de l'image
            imageToToggle.SetActive(!imageToToggle.activeSelf);
        }
        else
        {
            Debug.LogWarning("Aucune image assignée dans l'inspecteur !");
        }
    }
}
