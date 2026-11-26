using UnityEngine;
using UnityEngine.UI;

public class VolumeSettingsUI : MonoBehaviour
{
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;

    private void Start(){
        masterSlider.SetValueWithoutNotify(
            AudioManager.Instance.GetMasterVolume());

        musicSlider.SetValueWithoutNotify(
            AudioManager.Instance.GetMusicVolume());

        sfxSlider.SetValueWithoutNotify(
            AudioManager.Instance.GetSFXVolume());

        masterSlider.onValueChanged.AddListener(
            AudioManager.Instance.SetMasterVolume);

        musicSlider.onValueChanged.AddListener(
            AudioManager.Instance.SetMusicVolume);

        sfxSlider.onValueChanged.AddListener(
            AudioManager.Instance.SetSFXVolume);
    }

    private void OnDestroy(){
        if (AudioManager.Instance == null) return;

        masterSlider.onValueChanged.RemoveListener(
            AudioManager.Instance.SetMasterVolume);

        musicSlider.onValueChanged.RemoveListener(
            AudioManager.Instance.SetMusicVolume);

        sfxSlider.onValueChanged.RemoveListener(
            AudioManager.Instance.SetSFXVolume);
    }
}
