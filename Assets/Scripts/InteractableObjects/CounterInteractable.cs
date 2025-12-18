using UnityEngine;

public class CounterInteractable : TableInteractable
{
    [SerializeField] private CustomerManager customerManager;
    [SerializeField] private Transform customerPoint;
    private bool hasCustomer;
    private Transform currentCustomer;

    private void Start()
    {
        hasCustomer = false;
    }

    private void Update()
    {
        if (!hasCustomer & customerManager != null)
        {
            hasCustomer = customerManager.GetNextCustomerFromQueue(customerPoint.position);
        }
    }

    public override bool PlaceItem(Transform newItem)
    {   
        if (base.PlaceItem(newItem))
        {
            CheckItemWithCustomer();
            return true;
        }
        return false;        
    }

    private void CheckItemWithCustomer()
    {
        if (hasCustomer)
        {
            if (GetItem().TryGetComponent<Bottle>(out var bottle))
            {
                if (currentCustomer.TryGetComponent<CustomerController>(out var customer))
                {
                    if (customer.SendInOrder(bottle.GetLiquidStoredName()))
                    {
                        Destroy(GetItem().gameObject);
                        RemoveItem();
                    }
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        other.transform.GetComponentInParent<CustomerController>().LookAtCounter(transform.forward);
        currentCustomer = other.transform.root;
        hasCustomer = true;
    }

    private void OnTriggerExit(Collider other)
    {
        hasCustomer = false;
    }

    public Vector3 GetCounterPosition()
    {
        return customerPoint.position;
    }
}
