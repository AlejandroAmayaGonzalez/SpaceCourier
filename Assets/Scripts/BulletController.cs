using System;
using UnityEngine;
using UnityEngine.Audio;

public class BulletController : MonoBehaviour {
    
    [Header("Bullet Settings")]
    [SerializeField] private float bulletLifeTime = 3f;
    [SerializeField] private int bulletDamage = 1;
    [SerializeField] private ParticleSystem hitParticles;

    [Header("Bullet Audio")]
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioMixerGroup sfxGroup;

    void OnEnable(){
        Invoke(nameof(DeactivateBullet), bulletLifeTime);
    }

    private void DeactivateBullet(){ gameObject.SetActive(false); }

    void OnTriggerEnter(Collider other){
        // The bullet dont impact with the proximity collider
        if (other.isTrigger) return;

        if (other.CompareTag("Enemy") || other.CompareTag("Mine")){
            other.gameObject.GetComponent<EnemyHealth>().TakeDamage(bulletDamage);
        }
        
        // Play hit sound and particles
        AudioHelper.PlaySound(hitSound, transform.position, sfxGroup);
        hitParticles.Play();
        DeactivateBullet();
    }
}
