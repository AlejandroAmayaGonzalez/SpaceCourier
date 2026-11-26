using UnityEngine;
using UnityEngine.Audio;

public static class AudioHelper {

    public static float MasterVolume = 1f;
    public static float MusicVolume = 1f;
    public static float SFXVolume = 1f;

    public static void PlaySound(
        AudioClip clip, 
        Vector3 position, 
        AudioMixerGroup group
    ){
        if (clip == null) return;

        GameObject soundObject = new GameObject("TemporaryAudio");
        soundObject.transform.position = position;

        AudioSource source = soundObject.AddComponent<AudioSource>();

        source.outputAudioMixerGroup = group;
        source.clip = clip;
        source.Play();

        Object.Destroy(soundObject, clip.length);
    }
}
