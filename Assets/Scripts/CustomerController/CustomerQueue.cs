using System.Collections.Generic;
using UnityEngine;

public class CustomerQueue
{
    private List<Transform> customerQueue;
    private List<Vector3> queuePositions;

    public CustomerQueue(List<Vector3> newQueuePositions)
    {
        customerQueue = new();
        queuePositions = newQueuePositions;
    }

    public bool HasSpaceInQueue()
    {
        return customerQueue.Count < queuePositions.Count;
    }

    public void AddCustomerToQueue(Transform customer)
    {
        customerQueue.Add(customer);
        CustomerController customerScript = customer.GetComponent<CustomerController>();
        customerScript.SetNewDestination(queuePositions[customerQueue.Count - 1]);
        customerScript.StartTimer();
    }

    public Transform NextCustomer()
    {
        if (customerQueue.Count == 0)
        {
            return null;
        }
        Transform customer = customerQueue[0];
        customerQueue.Remove(customer);
        ReallocateCustomers();
        return customer;
    }

    public void EndGame()
    {
        customerQueue = new();
    }

    private void ReallocateCustomers()
    {
        for (int i = 0; i < customerQueue.Count; i++)
        {
            customerQueue[i].GetComponent<CustomerController>().SetNewDestination(queuePositions[i]);
        }
    }
}
