using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    [SerializeField] private CounterInteractable[] counters;

    [SerializeField] private PotionRecipe[] recipes;

    [Header("Customer Spawning")]
    [SerializeField] private float intervalBetweenCustomer;
    [SerializeField] private float maxWaitingTime;
    [SerializeField] private Transform customerPrefab;
    [SerializeField] private Transform customerSpawn;
    [SerializeField] private Transform customerExit;
    [SerializeField] private int maxSizeQueue;

    [Header("Scoring System")]
    [SerializeField] private int basePoints;
    [SerializeField] private int minusPoints;
    [SerializeField] private int maxBonusPoints;
    
    private bool customerCooldown = false;

    private CustomerQueue customerQueue;

    private void Awake()
    {
        Vector3 startingPosition = transform.position;
        List<Vector3> queuePositions = new();
        for (int i = 0; i < maxSizeQueue; i++)
        {
            queuePositions.Add(startingPosition - i * new Vector3(1, 0, 0));
        }
        customerQueue = new CustomerQueue(queuePositions);
    }

    private void Update()
    {
        if (!customerCooldown & customerQueue.HasSpaceInQueue())
        {
            customerCooldown = true;
            Invoke(nameof(ResetCustomerCooldown), intervalBetweenCustomer);

            Transform customer = Instantiate(customerPrefab, customerSpawn.position, customerSpawn.rotation);
            customer.GetComponent<CustomerController>().InitializeCustomer(recipes[Random.Range(0, recipes.Length)], customerExit.position, maxWaitingTime, basePoints, maxBonusPoints);

            customerQueue.AddCustomerToQueue(customer);
        }
    }

    private void ResetCustomerCooldown()
    {
        customerCooldown = false;
    }

    public bool GetNextCustomerFromQueue(Vector3 counterPosition)
    {
        Transform customer = customerQueue.NextCustomer();
        if (customer == null)
        {
            return false;
        }
        customer.GetComponent<CustomerController>().SetNewDestination(counterPosition);
        return true;
    }

    public void EndGame()
    {
        customerQueue.EndGame();
        CustomerController[] customerControllers = FindObjectsByType<CustomerController>(FindObjectsSortMode.None);
        for (int i = 0; i < customerControllers.Length; i++)
        {
            Destroy(customerControllers[i].gameObject);
        }
        enabled = false;
    }
}
