using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public AudioSource MenuSource;

    public AudioClip ButtonS;

    public GameObject settingsPanel;
    public Slider volumeSlider;

    private const string VolumeKey = "MasterVolume";

    private void Start()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }

        float savedVolume = PlayerPrefs.GetFloat(VolumeKey, 1f);
        AudioListener.volume = savedVolume;

        if (volumeSlider != null)
        {
            volumeSlider.value = savedVolume;
            volumeSlider.onValueChanged.AddListener(ChangeVolume);
        }
    }

    public void StartGame()
    {
        //This function is responsible for entering the main game scene.
        SceneManager.LoadScene("MainGame");

        MenuSource.PlayOneShot(ButtonS);
    }

    public void Quit()
    {
        //This function is responsible for exiting the game.
        Application.Quit();
        MenuSource.PlayOneShot(ButtonS);
    }

    public void OpenSettings()
    {
        //This function is responsible for entering settings.
        MenuSource.PlayOneShot(ButtonS);
   
        settingsPanel.SetActive(true);   
    }
    public void ExitSettings()
    {
        //This function is responsible for exiting settings.
        MenuSource.PlayOneShot(ButtonS);

        settingsPanel.SetActive(false);
    }
    public void ChangeVolume(float value)
    {
        //This function is responsible for managing the master volume of the game.
        AudioListener.volume = value;
        PlayerPrefs.SetFloat(VolumeKey, value);
        PlayerPrefs.Save();
    }
}