using UnityEngine;
using UnityEngine.Audio;

public class AmmoPickUp : MonoBehaviour {
   
   [Header("ScriptableObject")]
   [SerializeField] private AmmoData ammoData;

   [Header("PowerUp Settings")]
   [SerializeField] AudioClip pickPowerUpSound;
   [SerializeField] private AudioMixerGroup sfxGroup;

    void OnTriggerEnter(Collider other){
        if (!other.CompareTag("Player")) return;

        PlayerShooting playerShoot = other.GetComponent<PlayerShooting>();

        if (playerShoot == null) return;

        AudioHelper.PlaySound(pickPowerUpSound, transform.position, sfxGroup);

        // Refill all bullets
        playerShoot.CurrentAmmo += ammoData.ammoAmount;

        playerShoot.UpdateAmmo();

        Destroy(gameObject);
    }
}
