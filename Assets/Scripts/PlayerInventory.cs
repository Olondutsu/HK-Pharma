using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;
using UnityEditor.Build.Content;

public class PlayerInventory : MonoBehaviour
{
		[Header("References")]
		[SerializeField]
		public List<ItemInPlayerInventory> content = new List<ItemInPlayerInventory>();

		public static PlayerInventory instance;

		[SerializeField]
		public RecipeData[] recipeData;

		// public RecipeData currentRecipe;
		public ItemData[] itemUsed;

		[SerializeField]
		public Transform inventorySlotsParent;	

		public Sprite emptySlotVisual;

		[Header("Variables")]
		const int InventorySize = 24;

		public bool itemAdded = false;



		private void Awake()
		{
			instance = this;
		}

		private void Start()
		{
		}

		void Update()
		{
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
				ItemInPlayerInventory[] itemInPlayerInventory = content.Where(elem => elem.itemData == item).ToArray();	// requete Linq qui sera stocké dans inventory si trouvé sinon null

				// Remet a false lorsque cela s'amorce
				bool itemAdded = false;

				// Code pour le Stack
				// Length est utilisée pour obtenir le nombre d'éléments dans le tableau itemInInventory., signifie qu'on ajoute au stack s'il y en a au moins 1 quand l'actin s'effectue
			if(itemInPlayerInventory.Length > 0 && item.stackable)
			{

					// la boucle s'effectue a i = 0 et s'execute tant que i est inférieur a ItemInInventory.Length,, on incrémente +1 i
					// Initialise une boucle dans les cas où i < itemInInventory.Length qui ajoute i++
				for (int i = 0; i < itemInPlayerInventory.Length; i++)
				{
						// Si le compte de i est inférieur au MaxStack
						// SI l'objet est encore stackable, sinon on passe a la suite
				if (itemInPlayerInventory[i].count < item.maxStack)
				{
							// On ajoute l'item du count à l'item i et on le signale avec itemAdded
					itemAdded = true;
							itemInPlayerInventory[i].count++;
							// Retour a la ligne de code qui suit
							break;
						}
					}
					if(!itemAdded){
						content.Add(
									new ItemInPlayerInventory
									{
										itemData = item,
										count = 1
									}
								);
					}
				}

			else{
					content.Add(
									new ItemInPlayerInventory
								{
									itemData = item,
									count = 1
								}
								);
				}
		RefreshContent();
	}

	// //Added by Slot
	// private void ReorganizeItems()
	// {
	// 	// Récupérez la liste des objets de l'inventaire
	// 	List<ItemInPlayerInventory> items = PlayerInventory.instance.GetContent();

	// 	// Parcourez les emplacements de l'inventaire
	// 	for (int i = 0; i < PlayerInventory.instance.inventorySlotsParent.childCount; i++)
	// 	{
	// 		Slot slot = PlayerInventory.instance.inventorySlotsParent.GetChild(i).GetComponent<Slot>();

	// 		// Si l'emplacement est vide et qu'il y a des objets dans la liste des objets
	// 		if (slot.GetItemInSlot() == null && items.Count > 0)
	// 		{
	// 			// Placez le premier objet de la liste des objets dans cet emplacement
	// 			slot.SetItemInSlot(items[0]);
	// 			items.RemoveAt(0); // Retirez l'objet de la liste des objets
	// 		}
	// 	}

	// 	// Mettez à jour l'affichage de l'inventaire
	// 	PlayerInventory.instance.RefreshContent();
	// }
    public void RemoveItem(ItemData item, int quantity = 1)
	{
		
        ItemInPlayerInventory existingItem = content.Find(elem => elem.itemData == item);

        if (existingItem != null)
		{

            existingItem.count -= quantity;

            if (existingItem.count <= 0)
			{
                content.Remove(existingItem);
            }
            Debug.Log("Retrait de " + quantity + " " + item.itemName);
        }
        RefreshContent();
    }

    public List<ItemInPlayerInventory> GetContent()
	{

        return content;
    }

	public void RefreshContent()
	{
		// pour chaque content.count et inférieur au nombre d'enfant de l'inventory slot
		// for (int i = 0; i < content.Count && i < inventorySlotsParent.childCount; i++)
		for (int i = 0; i < inventorySlotsParent.childCount; i++)
		{
			
            // ItemInPlayerInventory currentItem = content[i];

			Slot currentPlayerSlot = inventorySlotsParent.GetChild(i).GetComponent<Slot>();
			
			// retirer l'objet depeupler visuel
			currentPlayerSlot.item = null;
			
			currentPlayerSlot.itemVisual.sprite = emptySlotVisual;
			currentPlayerSlot.countText.enabled = false;
            // currentPlayerSlot.item = currentItem.itemData;

			Debug.Log("ça c le premier");
			
        }
		
		for (int i = 0; i < content.Count; i++){
			
			ItemInPlayerInventory currentItem = content[i];
			
			Slot currentPlayerSlot = inventorySlotsParent.GetChild(i).GetComponent<Slot>();
				
				
				currentPlayerSlot.item = null;

				currentPlayerSlot.item = currentItem.itemData;

				currentPlayerSlot.itemVisual.sprite = currentItem.itemData.inventoryVisual;
				
				if (currentPlayerSlot.item.stackable)
				{
					
					Debug.Log("L'item dans le slot est stackable");
					currentPlayerSlot.countText.enabled = true;
					currentPlayerSlot.countText.text = currentItem.count.ToString();
				}		
		}
	}

    public bool IsFull()
	{

        return content.Count >= InventorySize;
    }

}

[System.Serializable]
public class ItemInPlayerInventory
{

	public ItemData itemData;
	public int count;

}
