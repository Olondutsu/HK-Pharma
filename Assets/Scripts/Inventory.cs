using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;
using UnityEditor.Build.Content;

public class Inventory : MonoBehaviour
{
    [Header("References")]
    [SerializeField]
    private List<ItemInInventory> content = new List<ItemInInventory>();


    public static Inventory instance;

    [SerializeField]
    public RecipeData[] recipeData;

    // public RecipeData currentRecipe;
    // public ItemData[] itemUsed;

    [SerializeField]
    private ContainerStats container;

    [SerializeField]
	private Transform inventorySlotsParent;	

    public Sprite emptySlotVisual;

    [Header("Variables")]
    const int InventorySize = 24;

    public bool itemAdded = false;

    private void Awake(){
        instance = this;
        container = GetComponent<ContainerStats>();
        // content = new List<ItemInContainer>();
    }

    private void Start(){
        // SearchForRecipesInInventory();
    }

    void Update(){
         CraftAllPossibleRecipes();
    }

    	public void AddItem(ItemData item, int quantity = 1)
		{
				// récupère la liste content
				// Where(elem => elem.itemData == item) est une méthode de filtrage de la liste content qui permet de sélectionner les éléments qui satisfont une condition donnée
				//Dans ce cas, la condition est elem.itemData == item, ce qui signifie que nous recherchons les éléments dont la propriété itemData est égale à l'objet item passé en paramètre.
				// En d'autres termes, cette expression lambda compare la propriété itemData de chaque élément de la liste avec l'objet item. Si l'élément a la même valeur pour sa propriété itemData que l'objet item, il est considéré comme une correspondance et sera inclus dans le résultat filtré.
				// ToArray() convertit le résultat de la méthode Where en un tableau (Array) d'objets de type ItemInInventory.
				// ItemInInventory[] itemInInventory est la variable qui stocke le tableau résultant.
				//ItemInInventory[] itemInInventory déclare une variable nommée itemInInventory qui est de type tableau ([]) d'objets de type ItemInInventory. Cela signifie que itemInInventory peut stocker plusieurs éléments de type ItemInInventory.
				// Ainsi, la méthode Where retournera un tableau (ou une liste) contenant les éléments de content qui satisfont la condition elem.itemData == item. Cela signifie que seuls les éléments dont la propriété itemData est égale à celle de l'objet item seront inclus dans le tableau résultant.
				ItemInInventory[] itemInInventory = content.Where(elem => elem.itemData == item).ToArray();	// requete Linq qui sera stocké dans inventory si trouvé sinon null
				
				// Remet a false lorsque cela s'amorce
				bool itemAdded = false;

				// Code pour le Stack
				// Length est utilisée pour obtenir le nombre d'éléments dans le tableau itemInInventory., signifie qu'on ajoute au stack s'il y en a au moins 1 quand l'actin s'effectue
				if(itemInInventory.Length > 0 && item.stackable)
				{
					// la boucle s'effectue a i = 0 et s'execute tant que i est inférieur a ItemInInventory.Length,, on incrémente +1 i
					// Initialise une boucle dans les cas où i < itemInInventory.Length qui ajoute i++
					for (int i = 0; i < itemInInventory.Length; i++){
						// Si le compte de i est inférieur au MaxStack
						// SI l'objet est encore stackable, sinon on passe a la suite
						if (itemInInventory[i].count < item.maxStack){
							// On ajoute l'item du count à l'item i et on le signale avec itemAdded
							itemAdded = true;
							itemInInventory[i].count++;
                            // if(itemInInventory[i].count < container.convItemQuantity){
                            //     itemInInventory[i].count++;
                            // }
							// Retour a la ligne de code qui suit
							break;
						}
					}
					// Si l'Item n'est pas déjà added
					if(!itemAdded){
						//
						content.Add(
									new ItemInInventory
									{
										itemData = item,
										count = 1
									}
								);
					}
				}
				//
			else{
					// l'ajouter...
					content.Add(
									new ItemInInventory
								{
									itemData = item,
									count = 1
								}
								);
				}
				
				// RefreshContent(); // Refresh pour faire apparaitre les Sprites
                // CraftRecipe();
		}


    public void RemoveItem(ItemData item, int quantity = 1){
        ItemInInventory existingItem = content.Find(elem => elem.itemData == item);

        if (existingItem != null){
            existingItem.count -= quantity;

            if (existingItem.count <= 0){
                content.Remove(existingItem);
            }
            Debug.Log("Retrait de " + quantity + " " + item.itemName);
        }

        // SearchForRecipesInInventory();
        // RefreshContent();
        
    }

    public List<ItemInInventory> GetContent(){
        return content;
    }

// public void RefreshContent(){

// 		for (int i = 0; i < content.Count  && i < inventorySlotsParent.childCount; i++){

//             ItemInInventory currentItem = content[i];

// 			Slot currentSlot = inventorySlotsParent.GetChild(i).GetComponent<Slot>();

// 			currentSlot.item = null;

// 			currentSlot.itemVisual.sprite = emptySlotVisual;
// 			currentSlot.countText.enabled = false;
//             currentSlot.item = currentItem.itemData;
//             }
			
// 				// On peuple le visuel des slots selon le contenu réel de l'inventaire
// 		for (int i = 0; i < content.Count; i++){
//             Debug.Log(" i est plus petit que le count du content donc lets go ajouter");

// 			ItemInInventory currentItem = content[i];

//             Slot currentSlot = inventorySlotsParent.GetChild(i).GetComponent<Slot>();
			
// 			currentSlot.item = currentItem.itemData;
// 			currentSlot.itemVisual.sprite = currentItem.itemData.inventoryVisual;
					
//             if (currentSlot.item.stackable){
//                 Debug.Log("L'item dans le slot est stackable");
//                 currentSlot.countText.enabled = true;
//                 currentSlot.countText.text = currentItem.count.ToString();
//             }
					
//         }
//     }

    public bool IsFull(){
        return content.Count >= InventorySize;
    }

     private void CraftAllPossibleRecipes()
    {
        foreach (RecipeData recipe in recipeData){
            if (CanCraftRecipe(recipe)){
                CraftRecipe(recipe);
            }
        }
    }

    private bool CanCraftRecipe(RecipeData recipe)
    {
        foreach (ItemData requiredItem in recipe.requiredItems){
            // Vérifie si chaque objet requis pour la recette est présent dans l'inventaire
            ItemInInventory itemInInventory = content.Find(elem => elem.itemData == requiredItem);
            if (itemInInventory == null || itemInInventory.count <= 0){
                return false; // La recette ne peut pas être fabriquée si un objet requis est manquant
            }
        }
        return true; // Tous les objets requis pour la recette sont présents
    }

    private void CraftRecipe(RecipeData recipe){

        int craftedQuantity = 0;

        foreach (ItemData requiredItem in recipe.requiredItems){

            ItemInInventory itemInInventory = content.Find(elem => elem.itemData == requiredItem);
            if (itemInInventory != null){
                craftedQuantity = itemInInventory.count;
            }
        }

        if (craftedQuantity > 0){
            foreach (ItemData requiredItem in recipe.requiredItems){

                if (requiredItem.itemName != recipe.craftableItem.itemName){
                    Debug.Log("LerequiredItem est différent de recipecraftableitem");
                    RemoveItem(requiredItem, craftedQuantity);
                }
                else{
                    Debug.Log("Les noms des items sont identiques, aucun élément ne sera détruit.");
                }
            }
            AddItem(recipe.craftableItem, craftedQuantity);
            // RefreshContent();
            Debug.Log("Fabriqué : " + craftedQuantity + " " + recipe.craftableItem.itemName);
        }
    }
//     private void CraftRecipe(RecipeData recipe)
// {
//     if (!CanCraftRecipe(recipe))
//     {
//         Debug.Log("Impossible de fabriquer la recette.");
//         return;
//     }

//     float overallPercentage = 1.0f;

//     foreach (IngredientInfo ingredientInfo in recipe.usedIngredients)
//     {
//         float ingredientPercentage = ingredientInfo.GetIngredientPercentage();

//         if (ingredientPercentage < 1.0f)
//         {
//             Debug.Log("Ingrédient utilisé en dessous de l'idéal : " + ingredientInfo.ingredient.itemName);
//             // Vous pouvez ajouter ici un comportement spécifique en fonction du pourcentage de chaque ingrédient.
//         }

//         overallPercentage = Mathf.Min(overallPercentage, ingredientPercentage);
//     }

//     if (overallPercentage >= 1.0f)
//     {
//         int craftedQuantity = 1; // Vous pouvez ajuster la quantité fabriquée en fonction du pourcentage global si nécessaire.
//         AddItem(recipe.craftableItem, craftedQuantity);
//         Debug.Log("Fabriqué : " + craftedQuantity + " " + recipe.craftableItem.itemName);
//     }
//     else
//     {
//         Debug.Log("Impossible de fabriquer la recette en raison de la faible utilisation des ingrédients.");
//     }
// }
}

[System.Serializable]
public class ItemInInventory{
	public ItemData itemData;
	public int count;

}
