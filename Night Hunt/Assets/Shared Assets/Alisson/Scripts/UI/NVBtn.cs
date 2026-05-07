using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class NVBtn : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public SpriteRenderer ForestCol;
    public AudioSource NVSource;
    private bool isSoundOnPlayed;

    public AudioClip NVSoundOn;
    public AudioClip NVSoundOff;

    public static NVBtn Instance;

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

    public bool isButtonPressed;

    //Changes forest opacity
    void Start()
    {
        ForestCol = ForestCol.GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        //Sets opacity to 255 (full) when nNight Vision battery is 0
        if (GameItems.Instance.currentNVBattery <= 0)
        {
            ForestCol.color = new Color32(0, 0, 0, 255);
        }
        else { }
    }

    //When clicking (holding) on Night Vision UI: change opacity of forest
    public void OnPointerDown(PointerEventData eventData)
    {
        isButtonPressed = true;
        ForestCol.color = new Color32(0, 0, 0, 67);
        
        if (!isSoundOnPlayed)
        {
            NVSource.PlayOneShot(NVSoundOn);
            isSoundOnPlayed = true;
        }
        else { }
    }

    //When realising Night Vision UI: change opacity of forest (full)
    public void OnPointerUp(PointerEventData eventData)
    {
        isButtonPressed = false;
        ForestCol.color = new Color32(0, 0, 0, 255);
        NVSource.PlayOneShot(NVSoundOff);
        isSoundOnPlayed = false;
    }
}
