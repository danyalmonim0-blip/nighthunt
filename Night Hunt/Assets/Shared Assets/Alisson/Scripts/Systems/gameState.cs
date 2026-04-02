using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class gameState : MonoBehaviour
{
    bool night = true;
    string startingText;
    int dayCounter = 1;
    float currentTime;
    public float startingTime = 5f; //180f for 3min
    float surviveTime;

    public Player playerHealth;
    public Player currentHealth;
    public Player playerIsDead;


    void Start()
    {
        Debug.Log("Shift starts");
        currentTime = startingTime;
        //player hits start shift
        //NightEvents();
    }

    private void Update()
    {
        currentTime -= 1 * Time.deltaTime; //shift timer : shift ends when 0
        if (currentTime <= 0)
        {
            currentTime = 0;
            night = false;
            Debug.Log("Shift ends");
            DayCounterDisplay();
            SceneManager.LoadScene("MainMenu"); //after shift menu
        }
        else if (Player.Instance.playerIsDead == true)
        {
            Debug.Log("Player is dead");
            SurviveTime();
            night = false;
            Debug.Log("Player lost");
            SceneManager.LoadScene("MainMenu");
        }
        //player hits pause
        //player dies
    }

    //public void NightEvents()
    //{
        
        
        
    //}

    void DayCounterDisplay()
    {
        Debug.Log(dayCounter);
        dayCounter++;
    }

    void SurviveTime()
    {
        surviveTime = currentTime;
        Debug.Log("You had " + currentTime + " hours left!");
    }



    void EndingUI()
    {
        
    }

    void StartNewShift()
    {

    }

    void Statistics()
    {
        //kills counter (animals & monsters)
        //end of shift UI (day 1 over)
    }
    void ReplayMenu()
    {
        //retry
        //back to menu
    }

    void StartingUI()
    {
    //    string startingText;
    }
}
