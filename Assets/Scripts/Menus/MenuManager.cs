using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    [Header("Menu's Panels")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject volumePanel;
    [SerializeField] private GameObject controlsPanel;
    [SerializeField] private GameObject levelSelectionPanel;

    // If a panel is not found don't do nothing
    void Start(){
        mainMenuPanel?.SetActive(true);
        volumePanel?.SetActive(false);
        controlsPanel?.SetActive(false);
        levelSelectionPanel?.SetActive(false);

        Time.timeScale = 1f;
    }

    public void LoadMainMenu(){
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void LoadLevel(string levelName){
        Time.timeScale = 1f;
        SceneManager.LoadScene(levelName);
    }

    public void RetryLevel(){
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void SeeControls(){
        mainMenuPanel.SetActive(false);
        controlsPanel.SetActive(true);
    }

    public void OpenVolumeSettings(){
        mainMenuPanel.SetActive(false);
        volumePanel.SetActive(true);
    }

    public void SeeCredits(){
        mainMenuPanel.SetActive(false);
        SceneManager.LoadScene("Credits");
    }

    // Open level selection
    public void OpenLevelSelection(){
        mainMenuPanel.SetActive(false);
        levelSelectionPanel.SetActive(true);
    }

    // Return to main menu from other menus
    public void BackToMainMenu(){
        mainMenuPanel.SetActive(true);
        levelSelectionPanel.SetActive(false);
        volumePanel.SetActive(false);
        controlsPanel.SetActive(false);
    }

    public void ExitGame(){ Application.Quit(); }
}
