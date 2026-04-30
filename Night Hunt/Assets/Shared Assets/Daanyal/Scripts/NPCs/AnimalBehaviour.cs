using UnityEngine;

public class AnimalBehaviour : MonoBehaviour
{
    public Transform spawnPoint;
    public Transform[] stage1SpotsAn;
    public Transform[] stage2SpotsAn;
    public Transform[] stage3SpotsAn;

    public Vector3 spawnScale = new Vector3(0.2f, 0.2f, 1f);
    public Vector3 stage1Scale = new Vector3(0.2f, 0.2f, 1f);
    public Vector3 stage2Scale = new Vector3(0.35f, 0.35f, 1f);
    public Vector3 stage3Scale = new Vector3(0.4f, 0.4f, 1f);

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

    private Sprite enemyColor;

    //public DayCounterManager dayCounter;

   // private Animator enemymator;

    void Start()
    {
        if (spawnPoint != null)
        {
            transform.position = spawnPoint.position;
        }

        transform.localScale = spawnScale;
        targetScale = spawnScale;
        currentTimerRand = timerRand;
        Invoke(nameof(DisappearAfterTime), 30f);

        enemyColor = GetComponent<Sprite>();

       // enemymator = GetComponent<Animator>();
    }

    void Update()
    {
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
            RandomNum();
            currentTimerRand = timerRand;
        }
    }

    public void RandomNum()
    {
        randomNum = Random.Range(21, 27);
        //Debug.Log(randomNum); 

        if (DayCounterManager.Instance.dayCounter >= 0 && DayCounterManager.Instance.dayCounter <= 4)
        {
            if (randomNum == 21)
            {
                MoveToNextStageAnimal();
            }
        }
        else if (DayCounterManager.Instance.dayCounter >= 5 && DayCounterManager.Instance.dayCounter <= 8)
        {
            if (randomNum == 21 || randomNum == 22)
            {
                MoveToNextStageAnimal();
            }
        }
        else if (DayCounterManager.Instance.dayCounter >= 9 && DayCounterManager.Instance.dayCounter <= 13)
        {
            if (randomNum == 21 || randomNum == 22 || randomNum == 23)
            {
                MoveToNextStageAnimal();
            }
        }
        else if (DayCounterManager.Instance.dayCounter >= 14)
        {
            if (randomNum == 21 || randomNum == 22 || randomNum == 23 || randomNum == 24)
            {
                MoveToNextStageAnimal();
            }
        }
    }

    void MoveAnimal()
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

            Debug.LogWarning("Animal changed position now at stage " + currentStage);

            //if (enemymator != null)
            //{
            //    enemymator.SetTrigger("Appear");
            //}
        }
    }

    void MoveToNextStageAnimal()
    {
        //enemymator.SetTrigger("Move");

        int chosenStage = Random.Range(1, 4);
        Transform[] chosenStageSpots = GetStageSpotsAnimal(chosenStage);

        //if (chosenStageSpots == null || chosenStageSpots.Length == 0)
        //{
        //    Debug.LogWarning("No Spot left " + chosenStage);
        //    return;
        //}

        Transform chosenSpot = chosenStageSpots[Random.Range(0, chosenStageSpots.Length)];

        if (currentTargetSpot != null && chosenSpot == currentTargetSpot && chosenStageSpots.Length > 1)
        {
            int randomIndex = Random.Range(0, chosenStageSpots.Length);
            chosenSpot = chosenStageSpots[randomIndex];
        }

        currentTargetSpot = chosenSpot;
        targetScale = GetStageScale(chosenStage);
        currentStage = chosenStage;
        isMoving = true;  
    }

    Transform[] GetStageSpotsAnimal(int stageNumber)  
    {
        switch (stageNumber)
        {
            case 1:
                return stage1SpotsAn;
            case 2:
                return stage2SpotsAn;
            case 3:
                return stage3SpotsAn;
            default:
                return stage1SpotsAn;

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

        hasBeenRemoved = true;
        
        if (animalSpawner != null)
        {
            animalSpawner.RegisterAnimalDespawn();
        }
        Destroy(gameObject);
    }

    public void MarkAsRemoved()
    {
        hasBeenRemoved = true;
        CancelInvoke(nameof(DisappearAfterTime));
    }
}