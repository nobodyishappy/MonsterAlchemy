using UnityEngine;

public class RecipeSheetInteractable : Interactable
{
    public override Transform TakeItem()
    {   
        Transform temp = GetItem();
        temp.localScale = new Vector3(1f, 1f, 1f);
        RemoveItem();
        SetHasItem(false);
        return temp;
    }

    public override bool PlaceItem(Transform newItem)
    {
        if (!newItem.TryGetComponent(out RecipeSheet recipeSheet))
        {
            return false;
        }

        if (GetHasItem())
        {
            return false;
        } else
        {
            newItem.localScale = new Vector3(1.3f, 1.3f, 1.3f);
            SetItem(newItem);
            return true;
        }
    }
}
