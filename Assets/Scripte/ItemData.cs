using UnityEngine;  // Nécessaire pour utiliser GameObject et ScriptableObject

[CreateAssetMenu(fileName = "New Shop Item", menuName = "Game/ShopItem", order = 1)]
public class ItemData : ScriptableObject  // Hérite de ScriptableObject
{
    public string itemName;       // Nom de l'article
    public float price;           // Prix de l'article
    public string description;    // Description de l'article
    public GameObject prefab;     // Prefab de l'article à afficher dans le shop
}
