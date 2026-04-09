using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private Vector3 endPosition = new Vector3 (5, -2, 0);
    private Vector3 startPosition;
    private float desiredDuration = 3f;
    private float elapsedTime;
    [SerializeField] private AnimationCurve curve;


    void Start()
    {
        startPosition = transform.position;
    }

    
    void Update()
    {
        elapsedTime += Time.deltaTime;
        float percentageComplete = elapsedTime / desiredDuration;
        transform.position = Vector3.Lerp(startPosition, endPosition, curve.Evaluate (percentageComplete));
    }
}
