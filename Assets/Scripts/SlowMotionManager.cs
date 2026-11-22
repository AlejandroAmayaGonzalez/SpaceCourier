using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class SlowMotionManager : MonoBehaviour {
    [Header("Slow Motion Settings")]
    [SerializeField] private float slowMotionScale = 0.5f;
    [SerializeField] private float transitionSpeed = 5f;

    [Header("Energy Parameters")]
    [SerializeField] private float maxEnergy = 100f;
    [SerializeField] private float energyConsumption = 25f;
    [SerializeField] private float energyRecovery = 15f;

    [Header("UI")]
    [SerializeField] private Slider slider;

    private float currentEnergy;
    private bool isSlowMotion;

    void Start(){
        currentEnergy = maxEnergy;

        slider.maxValue = maxEnergy;
        slider.value = currentEnergy;
    }

    void Update(){
        HandleInput();
        HandleEnergy();
        HandleTimeScale();
    }

    /// <summary>
    /// Q works as a toggle. Once pressed activates slow down and next
    /// deactivates it. When energy comes to zero, automatically deactivates.
    /// </summary>
    private void HandleInput(){
        if (Keyboard.current.qKey.wasPressedThisFrame && currentEnergy > 0){
            isSlowMotion = !isSlowMotion;
        }
        if (currentEnergy <= 0){ isSlowMotion = false; }
    }

    /// <summary>
    /// Handle the energy consumption and show it in the slider
    /// </summary>
    private void HandleEnergy(){
        if (isSlowMotion){
            currentEnergy -= energyConsumption * Time.unscaledDeltaTime;
        }else{
            currentEnergy += energyRecovery * Time.unscaledDeltaTime;
        }

        // Establish a minimum and maximum for the slider
        currentEnergy = Mathf.Clamp(currentEnergy, 0f, maxEnergy);

        slider.value = currentEnergy;
    }

    /// <summary>
    /// Slow downs or not depending the energy.
    /// </summary>
    private void HandleTimeScale(){
        float targetScale = isSlowMotion ? slowMotionScale : 1f;

        // Create a soft transition between scales
        Time.timeScale = Mathf.Lerp(
            Time.timeScale, // Initial speed
            targetScale,    // Needed speed
            Time.unscaledDeltaTime * transitionSpeed    // Speed in each frame
        );
    }

    /// <summary>
    /// Is called automatically when the gameObject is destroyed or deactivated
    /// </summary>
    private void OnDisable(){ Time.timeScale = 1f; }
}
