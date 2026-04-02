using UnityEngine;

[CreateAssetMenu(fileName = "PlayerHUD", menuName = "Scriptable Objects/PlayerHUD")]
public class PlayerHUD : ScriptableObject
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
