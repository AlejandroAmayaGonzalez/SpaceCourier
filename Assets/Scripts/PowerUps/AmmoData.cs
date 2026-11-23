using UnityEngine;

[CreateAssetMenu(fileName = "New Ammo PowerUp", menuName = "PowerUp / Ammo PowerUp")]
public class AmmoData : ScriptableObject {
    
    public string powerUpName;
    public GameObject ammoPrefab;

    [Header("Characteristics")]
    public int ammoAmount;

}
