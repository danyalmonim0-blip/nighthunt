using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource MenuSource;

    public AudioClip ButtonS;

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


    /*public void Settings()
   {
       SceneManager.LoadScene("Settings");
   } */

}