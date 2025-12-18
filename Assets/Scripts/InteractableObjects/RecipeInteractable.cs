using UnityEngine;

public class RecipeInteractable : Interactable
{
    [SerializeField] private LoadingBar loadingBar;
    [SerializeField] private string ActionType;

    public override Transform TakeItem()
    {   
        Transform temp = GetItem();
        RemoveItem();
        SetHasItem(false);
        loadingBar.HideLoading();
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
            if (newItem.TryGetComponent<Ingredient>(out var ingredient))
            {
                if (ingredient.GetRecipeType() == GetActionType())
                {
                    loadingBar.DisplayLoading();
                    loadingBar.SetLoadingValue(GetItem().GetComponent<Ingredient>().GetRecipePercentage());
                }
            } else
            {
                loadingBar.HideLoading();
            }
            return true;
        }
    }

    public override bool ActionOnItem()
    {
        if (GetHasItem())
        {
            if (!GetItem().TryGetComponent<Ingredient>(out var ingredient))
            {
                return false;
            }

            bool temp = ingredient.RecipeTimeUpdate(transform);
            if (temp)
            {
                loadingBar.SetLoadingValue(GetItem().GetComponent<Ingredient>().GetRecipePercentage());
            }
            else
            {
                loadingBar.HideLoading();
            }
            return temp;
        } else
        {
            return false;
        }
    }

    public string GetActionType()
    {
        return ActionType;
    }
}
