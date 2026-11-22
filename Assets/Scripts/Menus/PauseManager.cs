using UnityEngine;
using UnityEngine.InputSystem;

public class PauseManager : MonoBehaviour {
    
    [SerializeField] private GameObject pausePanel;

    private bool isPaused = false;

    void Start(){
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
    }

    // Esc for pause
    void Update(){
        if (Keyboard.current != null &&
            Keyboard.current.escapeKey.wasPressedThisFrame){
            
            if (isPaused)
                Resume();
            else
                Pause();
        }
    }

    public void Pause(){
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void Resume(){
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }
}
