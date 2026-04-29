using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public DayCounter dayCounterNewGame;

    public AudioSource MenuSource;

    public AudioClip ButtonS;

    public GameObject settingspanel;
    public Slider volumeSlider;

    private const string VolumeKey = "MasterVolume";

    private void Start()
    {
        if (settingspanel != null)
        {
            settingspanel.SetActive(false);
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
        SceneManager.LoadScene("HutTest");

        MenuSource.PlayOneShot(ButtonS);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("the game quits");
        MenuSource.PlayOneShot(ButtonS);
    }

    public void OpenSettings()
    {
        MenuSource.PlayOneShot(ButtonS);
   
   
        settingspanel.SetActive(true);
        
    }
    public void ExitSettings()
    {
        MenuSource.PlayOneShot(ButtonS);


        settingspanel.SetActive(false);
    }
    public void ChangeVolume(float value)
    {
        AudioListener.volume = value;
        PlayerPrefs.SetFloat(VolumeKey, value);
        PlayerPrefs.Save();
    }

    /*public void NewGame()
    {
        SceneManager.LoadScene("HutTest");
        //DayCounterManager.Instance.dayCounterNewGame.dayCounter = 0;
        MenuSource.PlayOneShot(ButtonS);
    }*/
}