using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour {
    
    [Header("HUD panels")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject controlPanel;
    [SerializeField] private GameObject volumePanel;

    private bool isPaused = false;

    void Start(){
        if (pausePanel != null){ pausePanel.SetActive(false); }
        if (controlPanel != null){ controlPanel.SetActive(false); }
        if (volumePanel != null){ volumePanel.SetActive(false); }

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

    public void SeeControls(){
        pausePanel.SetActive(false);
        controlPanel.SetActive(true); 
    }

    public void OpenVolume(){
        pausePanel.SetActive(false);
        volumePanel.SetActive(true);
    }

    public void Back(){
        pausePanel.SetActive(true);
        controlPanel.SetActive(false);
        volumePanel.SetActive(false);
    }

    public void LoadMainMenu(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
