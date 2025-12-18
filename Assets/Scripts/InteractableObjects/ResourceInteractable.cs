using UnityEngine;

public class ResourceInteractable : Interactable
{
    private void Start()
    {
        SetIsResource();
    }

    public override Transform TakeItem()
    {   
        Transform temp = Instantiate(GetItem());
        return temp;
    }

    public override bool PlaceItem(Transform newItem)
    {
        if (newItem.name.Contains(GetItem().name))
        {
            Destroy(newItem.gameObject);
            return true;
        } else
        {
            return false;
        }
    }
}
