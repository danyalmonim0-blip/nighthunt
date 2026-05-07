using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject settingsPanel;
    public Slider volumeSlider;

    public bool isPaused = false;
    private const string VolumeKey = "MasterVolume";

    
    void Start()
    {
        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
        else { }

        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }
        else { }

        float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 1f);
        AudioListener.volume = savedVolume;

        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;
            volumeSlider.onValueChanged.AddListener(ChangeVolume);
        }
        else { }
    }

    void Update()
    {
        if (UIElements.Instance != null && UIElements.Instance.EndPanel.activeSelf)
        {
            return;
        }
        else { }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                if (settingsPanel != null && settingsPanel.activeSelf)
                {
                    ExitSettings();
                }
                else
                {
                    ResumeGame();
                }
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        //This function is responsible for pausing the game.
        if (pausePanel != null)
        {
            pausePanel.SetActive(true);
        }
        else { }

        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        //This function is responsible for resuming the game.
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }
        else { }

        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
        else { }

        Time.timeScale = 1f;
        isPaused = false;
    }

    public void GoToMainMenu()
    {
        //This function is responsible for emtering the main menu scene.
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void RestartGame()
    {
        //This function is responsible for restarting the game.
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainGame");
    }

    public void Setings()
    {
        //This function is responsible for opening settings panel.
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(true);
        }
        else { }
    }

    public void ExitSettings()
    {
        //This function is responsible for exiting settings panel.
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }
        else { }
    }

    public void ChangeVolume(float value)
    {
        //This function is responsible for controling master volume through pause.
        AudioListener.volume = value;
        PlayerPrefs.SetFloat(VolumeKey, value);
        PlayerPrefs.Save();
    }
}