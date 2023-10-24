using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Items/New item")]
public class ItemData : ScriptableObject
{
	[Header("Data")]
	public string itemName;
	public string description;
	public Sprite inventoryVisual;
	public Sprite gameVisual;
	public bool stackable;
	public int maxStack;
	public int sellingPrice;
	public int buyingPrice;
	
	[Header("Effect")]
	public int currentQuantity;

	public int maxQuantity = 100;

	public bool liquid;

	public List<IngredientInfo> usedIngredients = new List<IngredientInfo>();

}

