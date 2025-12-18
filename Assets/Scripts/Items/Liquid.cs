using System.Collections.Generic;
using UnityEngine;

public class Liquid : ScriptableObject
{
    private string baseLiquid;
    private List<Ingredient> ingredients = new();
    private int count;
    private Color liquidColor;

    private float brewingCurrentTime = 0f;
    private PotionRecipe potionRecipe;

    private bool isPotion;


    public void SetNewLiquid(string baseType, int baseCount, Color newColor)
    {
        baseLiquid = baseType;
        count = baseCount;
        liquidColor = newColor;
        isPotion = false;
    }

    public bool IsWater()
    {
        return ingredients.Count == 0 & baseLiquid == "Water";
    }

    public void AddIngredient(Ingredient ingredient)
    {
        isPotion = false;
        ingredients.Add(ingredient);
    }

    public string GetBaseLiquid()
    {
        return baseLiquid;
    }

    public List<Ingredient> GetIngredients()
    {
        return ingredients;
    }

    public List<string> GetIngredientNames()
    {
        List<string> ingredientNames = new();
        foreach(Ingredient ingredient in ingredients)
        {
            ingredientNames.Add(ingredient.GetIngredientName());
        }
        return ingredientNames;
    }

    public Color GetLiquidColor()
    {
        return liquidColor;
    }

    public void BrewingSwitch()
    {
        baseLiquid = potionRecipe.GetPotionName();
        liquidColor = potionRecipe.GetPotionColor();
        ingredients = new();
        brewingCurrentTime = 0f;
        potionRecipe = null;
        isPotion = true;
    }

    public bool BrewingTimeUpdate()
    {
        brewingCurrentTime += Time.deltaTime;

        if (brewingCurrentTime >= potionRecipe.GetBrewingTime())
        {
            BrewingSwitch();
            return false;
        }

        return true;
    }
    
    public bool StartPotionBrewing(PotionRecipe newPotionRecipe)
    {
        potionRecipe = newPotionRecipe;
        return true;
    }

    public float GetBrewingPercentage()
    {
        return brewingCurrentTime / potionRecipe.GetBrewingTime();
    }

    public bool HasRecipe()
    {
        return potionRecipe != null;
    }

    public Liquid TakeOneServing()
    {
        if (count == 0 | !isPotion)
        {
            return null;
        }

        count--;
        Liquid oneServing = ScriptableObject.CreateInstance<Liquid>();
        oneServing.SetNewLiquid(baseLiquid, 1, liquidColor);
        return oneServing;
    }

    public int GetCount()
    {
        return count;
    }
}
