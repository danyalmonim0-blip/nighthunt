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


    void Start()
    {
        //player hits start shift

        NightEvents();
    }

    private void Update()
    {
        Timer(); //shift time ends
        //player hits pause
        //player dies
    }

    public void Timer()
    {
        currentTime -= 1 * Time.deltaTime; //shift timer : shift ends when 0
        if (currentTime <= 0)
        {
            currentTime = 0;
            ShiftEnds();
            NightEvents(); //add button to enable it
        }
    }
    public void NightEvents()
    {
        Debug.Log("Shift starts");
        currentTime = startingTime;
    }

    public void ShiftEnds()
    {
        Debug.Log("Shift ends");

        currentTime = startingTime; //-> loop
        night = false;
        DayCounter();
    }

    void DayCounter()
    {
        Debug.Log(dayCounter);
        dayCounter++;
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
