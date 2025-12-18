using UnityEngine;
using UnityEngine.AI;

public class CustomerController : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private PotionRecipe potionOrder;
    private NavMeshPath path;
    private Vector3 exitLocation;
    private Vector3 lookDirection;
    private bool isRotating;

    [SerializeField] private float rotSpeed;
    private ItemDisplay itemDisplay;

    private float maxWaitingTime;
    private float currentWaitingTime;
    private bool isWaiting;

    private int basePoints;
    private int maxBonusPoints;

    private void Awake()
    {
        navMeshAgent = transform.GetComponent<NavMeshAgent>();
        path = new NavMeshPath();
        itemDisplay = transform.GetComponentInChildren<ItemDisplay>();
        itemDisplay.gameObject.SetActive(false);
        isWaiting = false;
        currentWaitingTime = 0f;
    }

    private void Update()
    {   
        Vector3 cameraPos = Camera.main.transform.position;
        Vector3 lookAtPoint = cameraPos + (transform.position - cameraPos) * 2;
        itemDisplay.transform.LookAt(lookAtPoint);

        if (isWaiting)
        {
            currentWaitingTime -= Time.deltaTime;
            itemDisplay.UpdatePercentage(currentWaitingTime / maxWaitingTime);

            if (currentWaitingTime <= 0)
            {
                isWaiting = false;
                GameHandler.Instance.ScoreHandler.AddScore(-basePoints, 0);
                HeadToExit();
            }
        }

        if (isRotating)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(lookDirection), rotSpeed * Time.deltaTime);

            if (Quaternion.Angle(transform.rotation, Quaternion.LookRotation(lookDirection)) < 2f)
            {
                isRotating = false;
                lookDirection = new Vector3();
            }
        }
    }

    public void InitializeCustomer(PotionRecipe newPotionOrder, Vector3 exitPosition, float newWaitingTime, int newBasePoints, int newBonusPoints)
    {
        exitLocation = exitPosition;
        potionOrder = newPotionOrder;

        maxWaitingTime = newWaitingTime;
        
        basePoints = newBasePoints;
        maxBonusPoints = newBonusPoints;
    }

    public void StartTimer()
    {
        itemDisplay.UpdateDisplay(potionOrder.GetPotionSprite());
        itemDisplay.UpdatePercentage(1);
        itemDisplay.gameObject.SetActive(true);
        currentWaitingTime = maxWaitingTime;
        isWaiting = true;
    }

    public bool SetNewDestination(Vector3 targetPosition)
    {
        if (navMeshAgent.CalculatePath(targetPosition, path))
        {
            return navMeshAgent.SetPath(path);
        }
        return false;
    }

    public void LookAtCounter(Vector3 location)
    {
        isRotating = true;
        lookDirection = location;
    }

    public bool SendInOrder(string potionName)
    {
        if (currentWaitingTime <= 0)
        {
            return false;
        }

        if (potionOrder.GetPotionName().Equals(potionName))
        {
            float points = maxBonusPoints * (currentWaitingTime / maxWaitingTime);
            GameHandler.Instance.ScoreHandler.AddScore(basePoints, (int)points);
            HeadToExit();
            return true;
        }

        return false;
    }

    private void HeadToExit()
    {
        itemDisplay.gameObject.SetActive(false);
        SetNewDestination(exitLocation);
    }
}
