using UnityEngine;
using TMPro;

public class UIDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI dayCount;
    
    public void DayCount(UIData day)
    {
        dayCount.text = day.dayCount;
    }
    
    
}
