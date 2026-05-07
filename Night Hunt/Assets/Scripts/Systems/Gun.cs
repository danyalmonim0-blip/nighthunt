using UnityEngine;
using UnityEngine.UI;

public class Gun : MonoBehaviour
{
    public int startingBullets = 2;
    public int currentBullets;
    public bool isGunEmpty = false;

    public GameObject reloadingTxt;

    public Image gun;

    public static Gun Instance;
    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        currentBullets = startingBullets;
        reloadingTxt.SetActive(false);
    }

    void Update()
    {
        //Checks if gun is empty and show reloading UI
        if (currentBullets <= 0)
        {
            currentBullets = 0;
            reloadingTxt.SetActive(true);
            //Debug.Log("No bullets");
            GunEmpty();
        }
    }

    //Shows empty gun UI
    public void GunEmpty()
    {
        isGunEmpty = true;
        gun.color = Color.red;
    }

    //Reloading button
    public void ReloadBtn()
    {
        currentBullets = startingBullets;
        gun.color = Color.white;
        Debug.Log(currentBullets);
        isGunEmpty = false;
        reloadingTxt.SetActive(false);
    }
}
