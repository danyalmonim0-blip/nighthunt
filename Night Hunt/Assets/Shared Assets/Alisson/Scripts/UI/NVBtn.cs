using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;

public class NVBtn : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public SpriteRenderer ForestCol;

    public static NVBtn Instance;
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

    public bool buttonPressed;

    void Start()
    {
        ForestCol = GetComponent<SpriteRenderer>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        buttonPressed = true;
        MonsterSpawner.Instance.ChangeNPCColorGre();
        Debug.Log("Color Green");

        ForestCol.color = new Color32(0, 0, 0, 67);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        buttonPressed = false;
        MonsterSpawner.Instance.ChangeNPCColorBla();
        Debug.Log("Color Black");

        ForestCol.color = new Color32(0, 0, 0, 255);
    }
}
