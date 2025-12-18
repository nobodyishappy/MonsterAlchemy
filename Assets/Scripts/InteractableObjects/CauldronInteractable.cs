using UnityEngine;

public class CauldronInteractable : Interactable
{
    [SerializeField] private Transform liquidFullMesh;
    [SerializeField] private Transform liquidHalfMesh;
    private Liquid liquid;
    private bool hasLiquid;

    private bool potionBrewing = false;
    [SerializeField] private LoadingBar loadingBar;
    [SerializeField] private PotionRecipe[] potionRecipes;

    [SerializeField] private IngredientDisplay ingredientDisplay;

    private void Awake()
    {
        ResetCauldron();
    }
    
    private void Update()
    {
        if (potionBrewing & hasLiquid)
        {
            if(liquid.BrewingTimeUpdate())
            {
                loadingBar.SetLoadingValue(liquid.GetBrewingPercentage());
            } else
            {
                loadingBar.HideLoading();
                liquidFullMesh.GetComponent<Renderer>().material.color = liquid.GetLiquidColor();
                liquidHalfMesh.GetComponent<Renderer>().material.color = liquid.GetLiquidColor();
                potionBrewing = false;
                ingredientDisplay.RemoveAllIngredients();
            }
            
        }
    }

    public override bool PlaceItem(Transform newItem)
    {   
        if (potionBrewing)
        {
            return false;
        }

        if (newItem.TryGetComponent<Ingredient>(out var ingredient))
        {   
            if (hasLiquid)
            {
                if (!ingredientDisplay.AddOneIngredient(ingredient.GetIngredientSprite()))
                {
                    return false;
                }
                liquid.AddIngredient(ingredient);
                Destroy(newItem.gameObject);
                
                foreach(PotionRecipe recipe in potionRecipes)
                {
                    if (recipe.IsCorrectRecipe(liquid.GetBaseLiquid(), liquid.GetIngredientNames()))
                    {
                        potionBrewing = true;
                        liquid.StartPotionBrewing(recipe);
                        loadingBar.DisplayLoading();
                        loadingBar.SetLoadingValue(0f);
                        return true;
                    }
                }
                return true;
            }
            return false;
        }
        return false;
    }

    public override bool UseItemOnItem(Transform newItem)
    {
        if(newItem.TryGetComponent<Bucket>(out var bucket))
        {
            if (!hasLiquid & bucket.HasLiquid())
            {
                liquid = bucket.EmptyContainer();
                hasLiquid = true;
                liquidFullMesh.GetComponent<Renderer>().material.color = liquid.GetLiquidColor();
                liquidHalfMesh.GetComponent<Renderer>().material.color = liquid.GetLiquidColor();
                
                // Set liquid level (To be changed to use morph shape instead)
                if (liquid.GetCount() == 1)
                {
                    liquidHalfMesh.gameObject.SetActive(true);
                } else
                {
                    liquidFullMesh.gameObject.SetActive(true);
                }

                foreach(Ingredient ingredient in liquid.GetIngredients())
                {
                    ingredientDisplay.AddOneIngredient(ingredient.GetIngredientSprite());
                }
                
                // To continue brewing
                if (liquid.HasRecipe())
                {
                    potionBrewing = true;
                    loadingBar.DisplayLoading();
                    loadingBar.SetLoadingValue(liquid.GetBrewingPercentage());
                }
                return true;
            } else if (hasLiquid & !bucket.HasLiquid())
            {
                bucket.FillContainer(liquid);
                ResetCauldron();
            }
        } else if (newItem.TryGetComponent<Bottle>(out var bottle))
        {
            if (hasLiquid & !bottle.HasLiquid())
            {
                if (bottle.FillContainer(liquid.TakeOneServing()))
                {
                    if (liquid.GetCount() == 1)
                    {
                        liquidFullMesh.gameObject.SetActive(false);
                        liquidHalfMesh.gameObject.SetActive(true);
                    } else if (liquid.GetCount() == 0)
                    {
                        ResetCauldron();
                    }
                    return true;
                } 
            }
        }
        return false;
    }

    private void ResetCauldron()
    {
        liquid = null;
        liquidFullMesh.gameObject.SetActive(false);
        liquidHalfMesh.gameObject.SetActive(false);
        loadingBar.HideLoading();
        loadingBar.SetLoadingValue(0f);
        hasLiquid = false;
        potionBrewing = false;
        ingredientDisplay.RemoveAllIngredients();
    }
}
