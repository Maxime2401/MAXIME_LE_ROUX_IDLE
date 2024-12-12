using UnityEngine;
using TMPro;  // Nécessaire pour TextMeshPro

public class MoneyManager : MonoBehaviour
{
    public float currentMoney = 0f;  // Montant d'argent disponible
    public TextMeshProUGUI moneyText; // Référence au composant TextMeshProUGUI pour afficher l'argent

    // Ajouter de l'argent
    public void AddMoney(float amount)
    {
        currentMoney += amount;
        UpdateMoneyDisplay();  // Met à jour l'affichage chaque fois qu'on ajoute de l'argent
    }

    // Dépenser de l'argent
    public bool SpendMoney(float amount)
    {
        if (currentMoney >= amount)
        {
            currentMoney -= amount;
            UpdateMoneyDisplay();  // Met à jour l'affichage chaque fois qu'on dépense de l'argent
            return true;
        }
        else
        {
            Debug.LogWarning("Pas assez d'argent !");
            return false;
        }
    }

    // Met à jour l'affichage de l'argent
    private void UpdateMoneyDisplay()
    {
        if (moneyText != null)
        {
            moneyText.text = "Argent: $" + currentMoney.ToString("F2");  // Affiche avec deux décimales
        }
    }

    void Start()
    {
        UpdateMoneyDisplay();  // Initialiser l'affichage au démarrage
    }
}
