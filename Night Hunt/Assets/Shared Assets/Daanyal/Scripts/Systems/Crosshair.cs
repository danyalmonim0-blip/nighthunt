using UnityEngine;

public class Crosshair : MonoBehaviour
{
    public Camera mainCamera;

    [Header("Perlin Sway (Human Aim)")]
    public float swayReturn = 5f;
                                                                                                                         // Original Values              // Tuning
    /*public float swayAmplitude = 0.3f;   // world units (try 0.05.0.20)                                               // swayAmplitude = 0.10         // Too shaky? ? amplitude or ? frequency
    public float swayFrequency = 0.1f;    // speed of drift (try 0.41.5)                                               // swayFrequency = 0.8          // Too slow? ? frequency
    public float swayReturn = 5f;        // smoothing back toward target (try 8.20)                                   // swayReturn = 12              // Too floaty / laggy? ? swayReturn
                                        // Too perfect? ? amplitude slightly

    //public float maxAmplitudeTime;
    //public float maxFrequencyTime;*/

    [Tooltip("Different seed so each run feels unique.")]
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
        Vector3 mouseWorld = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;

        SetSwayFromFatigue();

        float t = Time.time * currentSwayFrequency;

        float nx = (Mathf.PerlinNoise(seed, t) * 2f) - 1f;
        float ny = (Mathf.PerlinNoise(seed + 999f, t) * 2f) - 1f;

        Vector3 offset = new Vector3(nx, ny, 0f) * currentSwayAmplitude;
        Vector3 target = mouseWorld + offset;

        smoothedPos = Vector3.Lerp(smoothedPos, target, 1f - Mathf.Exp(-swayReturn * Time.deltaTime));
        transform.position = smoothedPos;
    }

    void SetSwayFromFatigue()
    {
        if (Player.Instance == null)
        {
            currentSwayAmplitude = 0.02f;
            currentSwayFrequency = 0.2f;
            return;
        }

        float fatigue = Player.Instance.currentFatigue;

        // Higher fatigue number = better condition = less sway
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

    /*private void Start()
    {
        //maxAmplitudeTime = Player.Instance.currentFatigue;
        //maxFrequencyTime = Player.Instance.currentFatigue;
    }*/

    /*private void Update()
    {
        //maxAmplitudeTime += Time.deltaTime;
        //maxFrequencyTime += Time.deltaTime;

        swayAmplitude += 0.2f * Time.deltaTime;
        swayFrequency += 0.2f * Time.deltaTime;

        if (swayAmplitude >= 0.7f)
        {
            swayAmplitude = 0.5f;
        }


        if (swayFrequency >= 0.7f)
        {
            swayFrequency = 0.5f;
        }

        //if (Player.Instance.currentFatigue == 30)
        //{
        //    Debug.Log("30");
        //    swayAmplitude = 1f;
        //    swayFrequency = 0.5f;
        //}
        //else if (Player.Instance.currentFatigue == 20)
        //{
        //    Debug.Log("20");
        //    swayAmplitude = 1.5f;
        //    swayFrequency = 4f;
        //}
        //else if (Player.Instance.currentFatigue == 10)
        //{
        //    Debug.Log("10");

        //    swayAmplitude = 2f;
        //    swayFrequency = 15f;
        //}
        //else {
        //    Debug.Log("40");

        //    swayAmplitude = 0;
        //    swayFrequency = 0;
        //}

        Vector3 mouseWorld = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mouseWorld.z = 0f;

        // Smooth Perlin noise in range [-1, 1]              Perlin noise = sway 
        float t = Time.time * swayFrequency;

        float nx = (Mathf.PerlinNoise(seed, t) * 2f) - 1f;
        float ny = (Mathf.PerlinNoise(seed + 999f, t) * 2f) - 1f;

        Vector3 offset = new Vector3(nx, ny, 0f) * swayAmplitude;

        Vector3 target = mouseWorld + offset;

        // Smooth so it feels like "hand control" not teleporting
        smoothedPos = Vector3.Lerp(smoothedPos, target, 1f - Mathf.Exp(-swayReturn * Time.deltaTime));
        transform.position = smoothedPos;
    }*/
}
