using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class NightVision : MonoBehaviour
{
    public GameObject ForestBG;

    public void Start()
    {
        ColorChanging();
    }
    public void ColorChanging()
    {
        //ForestBG.color = new Color(1, 1, 1, 1);

        ForestBG.GetComponent<Image>().color = new Color32(255, 0, 0, 255);

    }
}
