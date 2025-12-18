using UnityEngine;

public class DustbinInteractable : Interactable
{
    public override bool PlaceItem(Transform newItem)
    {   
        if (newItem.TryGetComponent<Ingredient>(out var ingredient))
        {
            Destroy(newItem.gameObject);
            return true;
        }
        return false;
    }

    public override bool UseItemOnItem(Transform newItem)
    {
        if(newItem.TryGetComponent<LiquidContainer>(out var container))
        {
            container.EmptyContainer();
        } 
        return false;
    }
}
