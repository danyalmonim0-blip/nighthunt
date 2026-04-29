using UnityEngine;

public class Gun : MonoBehaviour
{
    public int startingBullets = 2;
    public int currentBullets;
    public bool isGunEmpty = false;

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
    }

    // Update is called once per frame
    void Update()
    {
        if (currentBullets <= 0)
        {
            currentBullets = 0;
            //display text "reloading"
            Debug.Log("No bullets");
            isGunEmpty = true;
        }
    }

    public void Reloading()
    {
        currentBullets = startingBullets;
    }

    public void ReloadBtn()
    {

    }
}
