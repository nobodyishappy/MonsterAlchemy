using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "PotionRecipe", menuName = "Scriptable Objects/PotionRecipe")]
public class PotionRecipe : ScriptableObject
{
    [SerializeField] private string potionName;
    [SerializeField] private string baseLiquid;
    [SerializeField] private List<string> ingredients;
    [SerializeField] private Color potionColor;
    [SerializeField] private float brewingTime;
    [SerializeField] private Sprite potionSprite;

    public bool IsCorrectRecipe(string brewingBase, List<string> brewingIngredients)
    {
        if (ingredients.Count() != brewingIngredients.Count())
        {
            return false;
        }

        return ingredients.Except(brewingIngredients).ToList().Count() == 0;
    }

    public string GetPotionName()
    {
        return potionName;
    }

    public Color GetPotionColor()
    {
        return potionColor;
    }

    public float GetBrewingTime()
    {
        return brewingTime;
    }

    public Sprite GetPotionSprite()
    {
        return potionSprite;
    }
}
