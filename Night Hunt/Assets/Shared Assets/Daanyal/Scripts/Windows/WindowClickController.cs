using UnityEngine;
using UnityEngine.UI;

public class WindowClickController : MonoBehaviour
{
    public Animator HutMator;

    public GameObject LBtn;
    public GameObject RBtn;
    public GameObject BBtn;
    public GameObject LWBtn;
    public GameObject MWBtn;
    public GameObject RWBtn;

    public GameObject gameItems;
    public GameObject nightVision;

    private bool isZoomed;

    private int HutPos = 1; // 0 = left, 1 = mid, 2 = right;

    void Start()
    {
        LWBtn.SetActive(false);
        MWBtn.SetActive(true);
        RWBtn.SetActive(false);
        BBtn.SetActive(false);
        nightVision.SetActive(false);
        gameItems.SetActive(true);

    }


    void Update()
    {

    }

    public void isRPressed()
    {
        if (HutPos == 0)
        {
            HutMator.SetBool("isMZ", false);
            HutMator.SetBool("isRight", false);
            HutMator.SetBool("isLeft", false);
        }
        else if (HutPos == 1)
        {
            HutMator.SetBool("isRZ", false);
            HutMator.SetBool("isRight", true);
        }
        LBtn.SetActive(false);
        RBtn.SetActive(false);
        LWBtn.SetActive(false);
        MWBtn.SetActive(false);
        RWBtn.SetActive(false);

        HutPos++;

        Invoke("SetCorrectButtons", 0.45f);
    }

    public void isLPressed()
    {
        if (HutPos == 2)
        {
            HutMator.SetBool("isMZ", false);
            HutMator.SetBool("isLeft", false);
            HutMator.SetBool("isRight", false);
        }
        else if (HutPos == 1)
        {
            HutMator.SetBool("isLZ", false);
            HutMator.SetBool("isLeft", true);
        }
        LBtn.SetActive(false);
        RBtn.SetActive(false);
        LWBtn.SetActive(false);
        MWBtn.SetActive(false);
        RWBtn.SetActive(false);

        HutPos--;
    
        Invoke("SetCorrectButtons", 0.45f);
    }

    public void isRWPressed()
    {
        LBtn.SetActive(false);
        RWBtn.SetActive(false);
        HutMator.SetBool("isRZ", true);
        Invoke("SetCorrectButtons", 0.8f);
        isZoomed = true;
        gameItems.SetActive(false);
        nightVision.SetActive(true);
    }

    public void isMWPressed()
    {
        LBtn.SetActive(false);
        RBtn.SetActive(false);
        MWBtn.SetActive(false);

        HutMator.SetBool("isMZ", true);
        Invoke("SetCorrectButtons", 0.7f);
        isZoomed = true;
        gameItems.SetActive(false);
        nightVision.SetActive(true);
    }

    public void isLWPressed()
    {

        RBtn.SetActive(false);
        LWBtn.SetActive(false);
        HutMator.SetBool("isLZ", true);
        Invoke("SetCorrectButtons", 0.8f);
        isZoomed = true;
        gameItems.SetActive(false);
        nightVision.SetActive(true);
    }

    public void isBPressed()
    {

        HutMator.SetBool("isRZ", false);
        HutMator.SetBool("isMZ", false);
        HutMator.SetBool("isLZ", false);
        isZoomed = false;
        BBtn.SetActive(false);
        Invoke("SetCorrectButtons", 0.45f);
        gameItems.SetActive(true);
        nightVision.SetActive(false);

    }

    void SetCorrectButtons()
    {
        if (HutPos == 0 && !isZoomed)
        {
            RBtn.SetActive(true);
            LWBtn.SetActive(true);
        }
        else if (HutPos == 1 && !isZoomed)
        {
            LBtn.SetActive(true);
            RBtn.SetActive(true);
            MWBtn.SetActive(true);
        }
        else if (HutPos == 2 && !isZoomed)
        {
            LBtn.SetActive(true);
            RWBtn.SetActive(true);
        }
        else if (isZoomed)
        {
            BBtn.SetActive(true);

        }
    }
}
