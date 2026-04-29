using UnityEngine;

public class VolumeLoader : MonoBehaviour
{
    private const string VolumeKey = "MasterVolume";

    void Awake()
    {
        AudioListener.volume = PlayerPrefs.GetFloat(VolumeKey, 1f);
    }
}
