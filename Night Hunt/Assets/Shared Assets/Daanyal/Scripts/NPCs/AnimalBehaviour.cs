using UnityEngine;

public class AnimalBehaviour : MonoBehaviour
{
    // These are the movement spots where Animal prefab can move.
    public Transform spawnPoint;
    public Transform[] stage1SpotsAn;
    public Transform[] stage2SpotsAn;
    public Transform[] stage3SpotsAn;

    // These are the scales which Animal prefab will use while moving to different stages.
    public Vector3 spawnScale = new Vector3(0.35f, 0.35f, 1f);
    public Vector3 stage1Scale = new Vector3(0.35f, 0.35f, 1f);
    public Vector3 stage2Scale = new Vector3(0.5f, 0.5f, 1f);
    public Vector3 stage3Scale = new Vector3(0.8f, 0.8f, 1f);

    public float moveSpeed = 5f;
    public float scaleSpeed = 2f; 

    public float timerRand = 5f;
    public float currentTimerRand;

    public AnimalSpawner animalSpawner;

    private int currentStage = 0;
    private Transform currentTargetSpot;
    private Vector3 targetScale;
    private bool isMoving = false;
    private bool hasBeenRemoved = false;
    private int randomNum;
    private float spawnTime;

    private bool despawnTimerStarted = false;

     private Animator enemymator;

    void Start()
    {
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.position;
        }
        else { }

        transform.localScale = spawnScale;
        targetScale = spawnScale;
        currentTimerRand = timerRand;
 
        enemymator = GetComponent<Animator>();
    }

    void Update()
    {
        if (gameState.Instance == null || gameState.Instance.isPlaying == false)
        {
            return;
        }
        
        if (hasBeenRemoved)
        {
            return;
        }

        if (isMoving)
        {
            MoveAnimal();
            return;
        }

        currentTimerRand -= Time.deltaTime;
        if (currentTimerRand <= 0f)
        {
            ChooseRandomNum();
            currentTimerRand = timerRand;
        }
        else { }
    }

    public void ChooseRandomNum()
    {
        // This function enables the behaviour script to randomize the movement of the Animal prefabs and increase the movement ratio as days increase.
        randomNum = Random.Range(21, 27);

        if (DayCounterManager.Instance.dayCounter >= 0 && DayCounterManager.Instance.dayCounter <= 4)
        {
            if (randomNum == 21)
            {
                MoveToNextStageAnimal();
            }
            else { }
        }
        else if (DayCounterManager.Instance.dayCounter >= 5 && DayCounterManager.Instance.dayCounter <= 8)
        {
            if (randomNum == 21 || randomNum == 22)
            {
                MoveToNextStageAnimal();
            }
            else { }
        }
        else if (DayCounterManager.Instance.dayCounter >= 9 && DayCounterManager.Instance.dayCounter <= 13)
        {
            if (randomNum == 21 || randomNum == 22 || randomNum == 23)
            {
                MoveToNextStageAnimal();
            }
            else { }
        }
        else if (DayCounterManager.Instance.dayCounter >= 14)
        {
            if (randomNum == 21 || randomNum == 22 || randomNum == 23 || randomNum == 24)
            {
                MoveToNextStageAnimal();
            }
            else { }
        }
    }

    void MoveAnimal()
    {
        // This function is responsible for making the Animal prefab move to the designated places using the scales and spots.
        transform.position = Vector3.MoveTowards(transform.position, currentTargetSpot.position, moveSpeed * Time.deltaTime);

        transform.localScale = Vector3.MoveTowards(transform.localScale, targetScale, scaleSpeed * Time.deltaTime);

        bool reachedPostion = Vector3.Distance(transform.position, currentTargetSpot.position) < 0.01f;
        bool reachedScale = Vector3.Distance(transform.localScale, targetScale) < 0.01f;

        if (reachedPostion && reachedScale)
        {
            transform.position = currentTargetSpot.position;
            transform.localScale = targetScale;
            isMoving = false;

            //Debug.LogWarning("Animal changed position now at stage " + currentStage);
            //Debug.Log("Animal position: " + transform.position + " | Spot position: " + currentTargetSpot.position);

            if (enemymator != null)
            {
                enemymator.SetTrigger("Appear");
            }
            else { }
        }
        else { }
    }

    void MoveToNextStageAnimal()
    {
        //This function is responsible for making the Animal prefab move along in different stages.
        enemymator.SetTrigger("Move");

        int chosenStage = Random.Range(1, 4);
        Transform[] chosenStageSpots = GetStageSpotsAnimal(chosenStage);
        Transform chosenSpot = chosenStageSpots[Random.Range(0, chosenStageSpots.Length)];

        if (currentTargetSpot != null && chosenSpot == currentTargetSpot && chosenStageSpots.Length > 1)
        {
            int randomIndex = Random.Range(0, chosenStageSpots.Length);
            chosenSpot = chosenStageSpots[randomIndex];
        }
        else { }

        currentTargetSpot = chosenSpot;
        targetScale = GetStageScale(chosenStage);
        currentStage = chosenStage;
        isMoving = true;  
        
        if (!despawnTimerStarted)
        {
            despawnTimerStarted = true;
            spawnTime = Time.time;
            Invoke(nameof(DisappearAfterTime), 30f);    
        }
        else { }
        //Debug.LogWarning("Animal moving to stage " + chosenStage + " spot: " + currentTargetSpot.name);
    }

    Transform[] GetStageSpotsAnimal(int stageNumber)  
    {
        //This statement makes the Monster prefab move along the stages in a random way.
        switch (stageNumber)
        {
            case 1:
                return stage1SpotsAn;
            case 2:
                return stage2SpotsAn;
            case 3:
                return stage3SpotsAn;
            default:
                return null;
        }
    }

    Vector3 GetStageScale(int stageNumber)
    {
        switch (stageNumber)
        {
            //This makes the Monster prefab to change the scales while moving to stages.
            case 0:
                return spawnScale;
            case 1:
                return stage1Scale;
            case 2:
                return stage2Scale;
            case 3:
                return stage3Scale;
            default:
                return transform.localScale;
        }
    }

    void DisappearAfterTime()
    {
        if (hasBeenRemoved)
        {
            return;
        }
        else { }

            hasBeenRemoved = true;

        float livedFor = Time.time - spawnTime;
        //Debug.Log("Animal disappeared after timer. Lived for: " + livedFor + " seconds.");

        if (animalSpawner != null)
        {
            animalSpawner.RegisterAnimalDespawn();
        }
        else 
        {
           Debug.LogWarning("AnimalSpawner reference was null when animal despawned.");
        }
        Destroy(gameObject);
    }

    public void MarkAsRemoved()
    {
        hasBeenRemoved = true;
        CancelInvoke(nameof(DisappearAfterTime));
        //Debug.Log("Animal timer cancelled because player removed it.");
    }
}