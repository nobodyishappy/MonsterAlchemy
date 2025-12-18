using UnityEngine;

public class TableInteractable : Interactable
{
    public override Transform TakeItem()
    {   
        Transform temp = GetItem();
        RemoveItem();
        SetHasItem(false);
        return temp;
    }

    public override bool PlaceItem(Transform newItem)
    {   
        if (GetHasItem())
        {
            return false;
        } else
        {
            SetItem(newItem);
            return true;
        }
    }
}
