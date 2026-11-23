using UnityEngine;

public class AmmoPickUp : MonoBehaviour {
   
   [Header("ScriptableObject")]
   [SerializeField] private AmmoData ammoData;

   [Header("PowerUp Settings")]
   [SerializeField] AudioClip pickPowerUpSound;
   
   AudioSource audioSource;

    void Start(){ audioSource = GetComponent<AudioSource>(); }

    void OnTriggerEnter(Collider other){
        if (!other.CompareTag("Player")) return;

        PlayerShooting playerShoot = other.GetComponent<PlayerShooting>();

        if (playerShoot == null) return;

        audioSource.PlayOneShot(pickPowerUpSound);

        // Refill all bullets
        playerShoot.CurrentAmmo += ammoData.ammoAmount;

        playerShoot.UpdateAmmo();

        Destroy(gameObject);
    }
}
