using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class AudioManager : MonoBehaviour {
    public static AudioManager Instance;

    [Header("Audio Mixer")]
    [SerializeField] private AudioMixer audioMixer;

    [Header("Master Volume Elements")]
    [SerializeField] private Slider masterSlider;
    [SerializeField] private TextMeshProUGUI masterText;

    [Header("Music Volume Elements")]
    [SerializeField] private Slider musicSlider;
    [SerializeField] private TextMeshProUGUI musicText;

    [Header("SFX Volume Elements")]
    [SerializeField] private Slider sfxSlider;
    [SerializeField] private TextMeshProUGUI sfxText;

    void Awake(){
        if (Instance != null && Instance != this){
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        AudioHelper.MasterVolume =
            PlayerPrefs.GetFloat("MasterVolume", 1f);

        AudioHelper.MusicVolume =
            PlayerPrefs.GetFloat("MusicVolume", 1f);

        AudioHelper.SFXVolume =
            PlayerPrefs.GetFloat("SFXVolume", 1f);

        ApplyVolumes();
    }

    private void OnEnable(){ SceneManager.sceneLoaded += OnSceneLoaded; }
    private void OnDisable(){ SceneManager.sceneLoaded -= OnSceneLoaded; }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode){
        
        masterSlider = GameObject.FindGameObjectWithTag("MasterSlider")
            ?.GetComponent<Slider>();

        masterText = GameObject.FindGameObjectWithTag("MasterText")
            ?.GetComponent<TextMeshProUGUI>();

        musicSlider = GameObject.FindGameObjectWithTag("MusicSlider")
            ?.GetComponent<Slider>();

        musicText = GameObject.FindGameObjectWithTag("MusicText")
            ?.GetComponent<TextMeshProUGUI>();

        sfxSlider = GameObject.FindGameObjectWithTag("SFXSlider")
            ?.GetComponent<Slider>();

        sfxText = GameObject.FindGameObjectWithTag("SFXText")
            ?.GetComponent<TextMeshProUGUI>();

        UpdateUI();
    }

    private void UpdateUI(){
        if (masterSlider != null)
            masterSlider.value = AudioHelper.MasterVolume;

        if (masterText != null)
            masterText.SetText($"{Mathf.RoundToInt(AudioHelper.MasterVolume * 100)}%");

        if (musicSlider != null)
            musicSlider.value = AudioHelper.MusicVolume;

        if (musicText != null)
            musicText.SetText($"{Mathf.RoundToInt(AudioHelper.MusicVolume * 100)}%");

        if (sfxSlider != null)
            sfxSlider.value = AudioHelper.SFXVolume;

        if (sfxText != null)
            sfxText.SetText($"{Mathf.RoundToInt(AudioHelper.SFXVolume * 100)}%");
    }

    public void SetMasterVolume(float value){
        AudioHelper.MasterVolume = value;

        audioMixer.SetFloat("MasterVolume", ToDecibels(value));

        masterText.SetText(Mathf.RoundToInt(value * 100) + "%");

        PlayerPrefs.SetFloat("MasterVolume", value);
    }

    public void SetMusicVolume(float value){
        AudioHelper.MusicVolume = value;

        audioMixer.SetFloat("MusicVolume", ToDecibels(value));

        musicText.SetText(Mathf.RoundToInt(value * 100) + "%");

        PlayerPrefs.SetFloat("MusicVolume", value);
    }

    public void SetSFXVolume(float value){
        AudioHelper.SFXVolume = value;

        audioMixer.SetFloat("SFXVolume", ToDecibels(value));

        sfxText.SetText(Mathf.RoundToInt(value * 100) + "%");

        PlayerPrefs.SetFloat("SFXVolume", value);
    }

    private float ToDecibels(float value){
        return Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20f;
    }

    private void ApplyVolumes(){
        audioMixer.SetFloat("MasterVolume",
            ToDecibels(AudioHelper.MasterVolume));

        audioMixer.SetFloat("MusicVolume",
            ToDecibels(AudioHelper.MusicVolume));

        audioMixer.SetFloat("SFXVolume",
            ToDecibels(AudioHelper.SFXVolume));
    }

    public float GetMasterVolume() => AudioHelper.MasterVolume;
    public float GetMusicVolume() => AudioHelper.MusicVolume;
    public float GetSFXVolume() => AudioHelper.SFXVolume;

    private void OnApplicationQuit(){ PlayerPrefs.Save(); }
}
