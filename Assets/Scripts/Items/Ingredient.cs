using UnityEngine;

public class Ingredient : MonoBehaviour
{
    [SerializeField] private string ingredientName;
    [SerializeField] private Transform recipeIngredient;
    [SerializeField] private string recipeType;
    private float recipeCurrentTime = 0f;
    [SerializeField] private float recipeMaxTime;
    [SerializeField] private Sprite ingredientSprite;

    public string GetRecipeType()
    {
        return recipeType;
    }

    public virtual void RecipeSwitch(Transform RecipeInteractable)
    {
        Transform nextIngredient = Instantiate(recipeIngredient);
        RecipeInteractable.GetComponent<Interactable>().SetItem(nextIngredient);

        Destroy(transform.gameObject);
    }

    public virtual bool RecipeTimeUpdate(Transform RecipeInteractable)
    {
        if (recipeIngredient == null)
        {
            return false;
        }

        if (RecipeInteractable.GetComponent<RecipeInteractable>().GetActionType() != recipeType)
        {
            return false;
        }

        recipeCurrentTime += Time.deltaTime;

        if (recipeCurrentTime >= recipeMaxTime)
        {
            RecipeSwitch(RecipeInteractable);
            return false;
        }

        return true;
    }

    public float GetRecipePercentage()
    {
        return recipeCurrentTime / recipeMaxTime;
    }

    public string GetIngredientName()
    {
        return ingredientName;
    }

    public Sprite GetIngredientSprite()
    {
        return ingredientSprite;
    }
}
