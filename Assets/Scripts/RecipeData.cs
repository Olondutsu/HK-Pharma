using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Recipe", menuName = "Recipes/New Recipe")]
[System.Serializable]
public class RecipeData : ScriptableObject
{
    public ItemData craftableItem;
    public ItemData[] requiredItems;
    public List<IngredientInfo> usedIngredients = new List<IngredientInfo>();
}

public class IngredientInfo
{
    public ItemData ingredient;

    public int usedQuantity;
    // public float purityPercentage;
}