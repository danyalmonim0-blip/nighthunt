// using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public Transform spawnPoint;
    
    // Empty objects where enemy can move randomly when the die lands on 1
    public Transform[] stage1Spots;
    public Transform[] stage2Spots;
    public Transform[] stage3Spots;
    public Transform[] stage4Spots;      // This is the Danger Stage

    // When the object moves from lower to higher stage the size will increase
    public Vector3 spawnScale = new Vector3(1f, 1f, 1f);
    public Vector3 stage1Scale = new Vector3(1.2f, 1.2f, 1f);
    public Vector3 stage2Scale = new Vector3(1.5f, 1.5f, 1f);
    public Vector3 stage3Scale = new Vector3(1.8f, 1.8f, 1f);
    public Vector3 stage4Scale = new Vector3(3f, 3f, 1f);

    public float moveSpeed = 2f;
    public float scaleSpeed = 2f;     // lerp used for both

    public float timerRand = 3f;
    public float currentTimerRand;

    private int currentStage = 0;
    private Transform currentTargetSpot;
    private Vector3 targetScale;
    private bool isMoving = false;
    private bool scaryZone = false;
    private int randomNum;

    void Start()
    {
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.position;
        }

        transform.localScale = spawnScale;
        targetScale = spawnScale;

        currentTimerRand = timerRand;
    }

    void Update()
    {
        if (scaryZone)                                                // if (Input.GetKeyDown(KeyCode.M) && !isMoving && !scaryZone)
        {
                                                                      //{ MoveToNextStage(); }
            return;
        }

        if (isMoving)
        {
            MoveEnemy();
            return;
        }

        currentTimerRand -= Time.deltaTime;
        if (currentTimerRand <= 0)
        {
            RandomNum();
            currentTimerRand = timerRand;
        }
    }

    public void RandomNum()
    {
        randomNum = Random.RandomRange(8, 14);
        Debug.Log(randomNum); 

        if (randomNum == 8)
        {
            MoveToNextStage();
        }

        if (randomNum == 8)
        {
            MoveEnemy();
        }
    }



    void MoveEnemy()            // only job is to move enem in the SAME STAGE using the scale and speed, will be replaced by lerp later on over here
    {
        transform.position = Vector3.MoveTowards(transform.position, currentTargetSpot.position, moveSpeed * Time.deltaTime);
        
        transform.localScale = Vector3.MoveTowards(transform.localScale, targetScale, scaleSpeed * Time.deltaTime);

        bool reachedPostion = Vector3.Distance(transform.position, currentTargetSpot.position) < 0.01f;
        bool reachedScale = Vector3.Distance(transform.localScale, targetScale) < 0.01f;
       
        if (reachedPostion && reachedScale)
        {
            transform.position = currentTargetSpot.position;
            transform.localScale = targetScale;
            isMoving = false;

            Debug.LogWarning("the object changed position now at stage " + currentStage);

            if (currentStage == 4)
            {
                scaryZone = true;

                Debug.LogWarning("Game Over -- you suck as a ranger");
            }
        }
    }

    void MoveToNextStage()              // only job is to move among diff stages (spots in those stages)
    {
        int nextStage = currentStage + 1;
        Transform[] nextStageSpots = GetStageSpots(nextStage);

        if (nextStageSpots == null || nextStageSpots.Length == 0)
        {
            Debug.LogWarning("No Spot left " + nextStage);
            return;
        }

        int randomIndex = Random.Range(0, nextStageSpots.Length);
        currentTargetSpot = nextStageSpots[randomIndex];

        targetScale = GetStageScale(nextStage);
        currentStage = nextStage;
        isMoving = true;
   
        Debug.LogWarning("Moving to stage " + nextStage + " spot is " + currentTargetSpot.name);
    }

    Transform[] GetStageSpots(int stageNumber)             // Switch will be used
    {
        switch (stageNumber)
        {
            case 1:
                return stage1Spots;
            case 2:
                return stage2Spots;
            case 3:
                return stage3Spots;
            case 4:
                return stage4Spots;
            default:
                return null;

        }
    }

    Vector3 GetStageScale(int stageNumber)
    {
        switch (stageNumber)
        {
            case 0:
                return spawnScale;
            case 1:
                return stage1Scale;
            case 2:
                return stage2Scale;
            case 3:
                return stage3Scale;
            case 4:
                return stage4Scale;
            default:
                return transform.localScale;
        }
    }


}







// This goes in enemy behaviour
/* private Vector3 endPosition = new Vector3 (5, -2, 0);
   private Vector3 startPosition;
   private float desiredDuration = 3f;
   private float elapsedTime;
   [SerializeField] private AnimationCurve curve; */
// This goes in void start
/*startPosition = transform.position; */
// This goes in void update
/* elapsedTime += Time.deltaTime;
        float percentageComplete = elapsedTime / desiredDuration;
        transform.position = Vector3.Lerp(startPosition, endPosition, curve.Evaluate (percentageComplete)); */