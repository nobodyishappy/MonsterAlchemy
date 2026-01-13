using UnityEngine;

public class FloorplanInteractable : Interactable
{
    public override Transform TakeItem()
    {   
        Transform temp = GetItem();
        temp.localScale = new Vector3(1f, 1f, 1f);
        temp.GetComponent<Blueprint>().ResetMorph();
        RemoveItem();
        SetHasItem(false);
        return temp;
    }

    public override bool PlaceItem(Transform newItem)
    {
        if (!newItem.TryGetComponent(out Blueprint blueprint))
        {
            return false;
        }

        blueprint.StartMorph();

        if (GetHasItem())
        {
            return false;
        } else
        {
            newItem.localScale = new Vector3(1.7f, 1.7f, 1.7f);
            SetItem(newItem);
            return true;
        }
    }
}
