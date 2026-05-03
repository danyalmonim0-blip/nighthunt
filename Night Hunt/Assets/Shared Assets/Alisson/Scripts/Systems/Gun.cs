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
        else if (Instance != null)
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        currentBullets = startingBullets;
        reloadingTxt.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (currentBullets <= 0)
        {
            currentBullets = 0;
            reloadingTxt.SetActive(true);
            //Debug.Log("No bullets");
            GunEmpty();
        }
    }

    public void GunEmpty()
    {
        isGunEmpty = true;
        gun.color = Color.red;
    }

    public void ReloadBtn()
    {
        currentBullets = startingBullets;
        gun.color = Color.white;
        Debug.Log(currentBullets);
        isGunEmpty = false;
        reloadingTxt.SetActive(false);
    }
}
