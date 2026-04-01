using UnityEngine;

[CreateAssetMenu(fileName = "SOPlayer", menuName = "Scriptable Objects/SOPlayer")]
public class SOPlayer : ScriptableObject
{
    public int playerHealth;
    public int currentHealth;
    public int nightVision;
    public int lamp;
    public int sanity;
    public int fatigue;
    public int nightVisionBattery;
    public int lampBattery;

    [SerializeField] public bool playerIsDead = false;

}
