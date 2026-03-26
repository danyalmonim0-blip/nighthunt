using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class gameState : MonoBehaviour
{
    bool night = false;
    string startingText;
    int dayCounter = 0;
    float currentTime;
    public float startingTime = 10f; //180f for 3min


    void Start()
    {
        //player hits start shift
        night = true;
        if (currentTime == 0)
        {
            night = false;
            Debug.Log("Night ends");
        }
    }

    public void NightEvents()
    {
        void StartingUI()
        {
            string startingText;
        }

        void Timer()
        {
            currentTime = startingTime;
            
        }

        void DayCounter()
        {
            dayCounter++;
        }
    }

    public void StartShift()
    {
        do
        {
            NightEvents();
            
        }
        while (night == true);
    }

    private void Update()
    {
        currentTime -= 1 * Time.deltaTime; //shift timer : shift ends when 0
        if (currentTime >= 0)

        {
            currentTime = 0;
        }
        //player hits pause
        //player dies

    }

    public void ShiftEnds()
    {
        void EndingUI()
        {
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
        }
    }
}
