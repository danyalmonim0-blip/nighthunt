using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public Camera mainCamera;
    public float swayReturn = 5f;
    /*                                                                                                Original Values              Tuning
      public float swayAmplitude = 0.3f;     world units (try 0.05.0.20)                              swayAmplitude = 0.10         Too shaky?  amplitude or frequency
      public float swayFrequency = 0.1f;     speed of drift (try 0.41.5)                              swayFrequency = 0.8          Too slow?   frequency
      public float swayReturn = 5f;          smoothing back toward target (try 8.20)                  swayReturn = 12              
                                             Too perfect? ? amplitude slightly
    */

    public float seed = 100f;

    private Vector3 smoothedPos;
    private float currentSwayAmplitude;
    private float currentSwayFrequency;

    public static Crosshair Instance;
    
    private void Awake()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
        else { }

        smoothedPos = transform.position;

        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        //This statement enables the crosshair to follow the mouse in game scene.
        Vector3 mouseWorld = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;

        SetSwayFromFatigue();

        float t = Time.time * currentSwayFrequency;

        //This statement depicts the values for the lerp movememnt of the crosshair.
        float nx = (Mathf.PerlinNoise(seed, t) * 2f) - 1f;
        float ny = (Mathf.PerlinNoise(seed + 999f, t) * 2f) - 1f;

        Vector3 offset = new Vector3(nx, ny, 0f) * currentSwayAmplitude;
        Vector3 target = mouseWorld + offset;

        smoothedPos = Vector3.Lerp(smoothedPos, target, 1f - Mathf.Exp(-swayReturn * Time.deltaTime));
        transform.position = smoothedPos;
    }

    void SetSwayFromFatigue()
    {
        //This funtion is responsible for increasing and decreasing crosshair sway based on the fatigue value.
        if (Player.Instance == null)
        {
            currentSwayAmplitude = 0.02f;
            currentSwayFrequency = 0.2f;
            return;
        }
        else { }

        float fatigue = Player.Instance.currentFatigue;

        if (fatigue >= 35f)
        {
            currentSwayAmplitude = 0.02f;
            currentSwayFrequency = 0.2f;
        }
        else if (fatigue >= 25f)
        {
            currentSwayAmplitude = 0.06f;
            currentSwayFrequency = 0.4f;
        }
        else if (fatigue >= 15f)
        {
            currentSwayAmplitude = 0.12f;
            currentSwayFrequency = 0.7f;
        }
        else if (fatigue >= 5f)
        {
            currentSwayAmplitude = 0.20f;
            currentSwayFrequency = 1.0f;
        }
        else
        {
            currentSwayAmplitude = 0.28f;
            currentSwayFrequency = 1.3f;
        }
    }
}
