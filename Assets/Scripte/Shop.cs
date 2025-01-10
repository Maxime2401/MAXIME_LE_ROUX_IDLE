using UnityEngine;
using TMPro;
using UnityEngine.UI; // Pour gérer les boutons
using System.Collections.Generic; // Pour utiliser les queues

public class Shop : MonoBehaviour
{
    public ItemData[] items; // Liste des articles dans le shop
    public Transform[] itemSlots; // Emplacements où afficher les articles
    public TextMeshProUGUI moneyText; // Affichage de l'argent disponible

    // Références aux éléments UI pour afficher les informations des items
    public TextMeshProUGUI itemNameText;
    public TextMeshProUGUI itemDescriptionText;
    public TextMeshProUGUI itemPriceText;

    private Queue<ItemData> itemQueue; // File d'attente pour les items
    private MoneyManager moneyManager; // Référence au MoneyManager

    void Start()
    {
        // Trouver le MoneyManager dans la scène
        moneyManager = FindObjectOfType<MoneyManager>();
        if (moneyManager == null)
        {
            Debug.LogError("MoneyManager introuvable !");
        }

        // Initialiser la file d'attente des items
        itemQueue = new Queue<ItemData>(items);

        // Initialiser les articles dans le shop
        InitializeShopItems();
        UpdateMoneyDisplay(); // Assurez-vous que l'argent est affiché dès le début
    }

    void Update()
    {
        // Vérifier que l'argent affiché reste à jour
        UpdateMoneyDisplay();
    }

    // Met à jour l'affichage de l'argent du joueur
    void UpdateMoneyDisplay()
    {
        if (moneyText != null && moneyManager != null)
        {
            moneyText.text = "Argent: $" + moneyManager.currentMoney.ToString("F2");
        }
    }

    // Méthode pour initialiser les articles dans les emplacements du shop
    void InitializeShopItems()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            if (itemQueue.Count > 0)
            {
                UpdateItemSlot(i);
            }
        }
    }

    // Méthode pour mettre à jour les informations d'un item dans un slot
    void UpdateItemSlot(int slotIndex)
    {
        if (itemQueue.Count > 0)
        {
            ItemData itemData = itemQueue.Dequeue(); // Récupérer les données de l'article

            if (itemData.prefab != null)
            {
                // Instancier l'item à l'emplacement correspondant
                GameObject item = Instantiate(itemData.prefab, itemSlots[slotIndex].position, Quaternion.identity);

                // Parent l'item à l'emplacement
                item.transform.SetParent(itemSlots[slotIndex], false); // "false" garde la position locale
                item.transform.localPosition = Vector3.zero; // Réinitialiser la position locale

                // Ajouter les informations sur l'UI
                itemNameText.text = itemData.itemName; // Afficher le nom de l'article
                itemDescriptionText.text = itemData.description; // Afficher la description de l'article
                itemPriceText.text = "$" + itemData.price.ToString("F2"); // Afficher le prix

                // Ajouter un bouton pour l'achat de l'item
                Button buyButton = item.GetComponentInChildren<Button>();
                if (buyButton != null)
                {
                    buyButton.onClick.AddListener(() => BuyItem(slotIndex, itemData));
                }
            }
        }
        else
        {
            Debug.LogError("File d'attente des items vide lors de la mise à jour de l'item slot !");
        }
    }

    // Méthode pour acheter un item
    void BuyItem(int slotIndex, ItemData selectedItem)
    {
        if (moneyManager != null)
        {
            // Vérifier si le joueur a assez d'argent
            if (moneyManager.currentMoney >= selectedItem.price)
            {
                // Réduire l'argent du joueur
                moneyManager.SpendMoney(selectedItem.price);

                // Mettre à jour l'affichage de l'argent
                UpdateMoneyDisplay();

                // Afficher un message de confirmation
                Debug.Log($"Achat réussi: {selectedItem.itemName} pour ${selectedItem.price}.");

                // Remplacer l'item acheté par le suivant (si applicable)
                if (itemQueue.Count > 0)
                {
                    Destroy(itemSlots[slotIndex].GetChild(0).gameObject); // Détruire l'item actuel dans le slot
                    UpdateItemSlot(slotIndex);
                }
                else
                {
                    Destroy(itemSlots[slotIndex].GetChild(0).gameObject); // Détruire l'item actuel dans le slot
                }
            }
            else
            {
                Debug.Log("Pas assez d'argent !");
            }
        }
        else
        {
            Debug.LogError("MoneyManager introuvable !");
        }
    }
}
