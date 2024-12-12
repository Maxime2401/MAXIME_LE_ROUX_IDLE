using UnityEngine;

[CreateAssetMenu(fileName = "NewWheel", menuName = "Game/Wheel")]
public class WheelData : ScriptableObject
{
    public string wheelName;         // Nom de la roue
    public float speedModifier;      // Bonus sur la vitesse
    public float maxSpeedModifier;   // Bonus sur la vitesse maximale
    public int price;                // Prix de la roue
    public Sprite wheelSprite;       // Sprite associé à la roue
}
