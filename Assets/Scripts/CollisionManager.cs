using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CollisionManager : MonoBehaviour {
    [Header("Audio Settings")]
    [SerializeField] private AudioClip explosionAudioClip;
    [SerializeField] private AudioClip landingSuccessAudioClip;
    [SerializeField] private AudioClip enemyHitSound;
    [SerializeField] private AudioClip mineExplosionSound;

    [Header("Particle Systems")]
    [SerializeField] private ParticleSystem explosionParticles;
    [SerializeField] private ParticleSystem landingParticles;
    [SerializeField] private float crashDelay = 2f;

    [Header("HUD elements")]
    [SerializeField] private TextMeshProUGUI healthHUD;

    AudioSource audioSource;
    PlayerHealth playerHealth;
    bool isControllable = true;

    void Start(){
        playerHealth = GetComponent<PlayerHealth>();
        audioSource = GetComponent<AudioSource>();

        UpdateHealth();
    }

    void OnCollisionEnter(Collision collision){
        if (!isControllable) return;

        switch (collision.gameObject.tag){
            case "LandingPad":
                DieOrSuccess(landingSuccessAudioClip, landingParticles);
                break;
            case "Explosion":
                playerHealth.TakeDamage(playerHealth.currentHealth);
                DieOrSuccess(explosionAudioClip, explosionParticles);
                break;
            case "Enemy":
                EnemyHit(collision.gameObject);
                break;                      
        }

        UpdateHealth();
    }

    /// For the mine explosion
    void OnTriggerEnter(Collider other){
        EnterMineZone(other.gameObject);
        UpdateHealth();
    }

    private void PlayAudio(AudioClip audioClip){
        audioSource.Stop();
        audioSource.PlayOneShot(audioClip);
    }

    /// <summary>
    /// Set the current health in the HUD
    /// </summary>
    private void UpdateHealth(){
        healthHUD.SetText(
            "Health -> " + playerHealth.currentHealth + 
            "/" + playerHealth.maxHealth
        );
    }

    /// <summary>
    /// Manages when the player enters the explosion range of a mine
    /// </summary>
    private void EnterMineZone(GameObject mineObj){
        playerHealth = GetComponent<PlayerHealth>();
        playerHealth.TakeDamage(playerHealth.currentHealth);

        PlayAudio(mineExplosionSound);

        mineObj.GetComponent<EnemyHealth>().TakeDamage(mineObj.GetComponent<EnemyHealth>().currenthealth);
    }

    /// <summary>
    /// Manages the enemy collision
    /// </summary>
    /// <param name="obj">the gameobject to get the enemy damage</param>
    private void EnemyHit(GameObject obj){
        PlayAudio(enemyHitSound);

        GetComponent<PlayerHealth>().TakeDamage(
            obj.GetComponent<EnemyHealth>().enemyData.damage
        );
    }

    /// <summary>
    /// Manages the collision event, disabling player control, reproducing the audioClip.
    /// </summary>
    /// <param name="audioClip">the audio played when the collision occurs</param>
    private void DieOrSuccess(AudioClip audioClip, ParticleSystem particleSystem){
        isControllable = false;

        PlayAudio(audioClip);

        particleSystem.Play();

        GetComponent<PlayerMovement>().enabled = false;

        Invoke(nameof(ReloadScene), crashDelay);
    }

    void ReloadScene(){
        // Reload the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
