using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;
using UnityEditor.Build.Content;

public class StoreInventory : MonoBehaviour
{
    [Header("References")]

    [SerializeField]
    public List<ItemInStoreInventory> content = new List<ItemInStoreInventory>();

    public static StoreInventory instance;

    [SerializeField]
    public RecipeData[] recipeData;

    public List<ItemData> possibleItems;

    [SerializeField]
	private Transform inventorySlotsParent;	

    public Sprite emptySlotVisual;

    [Header("Variables")]
    const int InventorySize = 24;

    public bool itemAdded = false;


        private void Awake(){
            instance = this;
        }

        private void Start(){
        }

        void Update(){
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
				ItemInStoreInventory[] itemInStoreInventory = content.Where(elem => elem.itemData == item).ToArray();	// requete Linq qui sera stocké dans inventory si trouvé sinon null
				
				// Remet a false lorsque cela s'amorce
				bool itemAdded = false;

				// Code pour le Stack
				// Length est utilisée pour obtenir le nombre d'éléments dans le tableau itemInInventory., signifie qu'on ajoute au stack s'il y en a au moins 1 quand l'actin s'effectue
				if(itemInStoreInventory.Length > 0 && item.stackable)
				{
					// la boucle s'effectue a i = 0 et s'execute tant que i est inférieur a ItemInInventory.Length,, on incrémente +1 i
					// Initialise une boucle dans les cas où i < itemInInventory.Length qui ajoute i++
					for (int i = 0; i < itemInStoreInventory.Length; i++){
						// Si le compte de i est inférieur au MaxStack
						// SI l'objet est encore stackable, sinon on passe a la suite
						if (itemInStoreInventory[i].count < item.maxStack){
							// On ajoute l'item du count à l'item i et on le signale avec itemAdded
							itemAdded = true;
							itemInStoreInventory[i].count++;
							// Retour a la ligne de code qui suit
							break;
						}
					}
                    
					if(!itemAdded)
					{
						content.Add(
									new ItemInStoreInventory
									{
										itemData = item,
										count = 1
									}
								);
					}
				}

			else{
					
					content.Add(
									new ItemInStoreInventory
								{
									itemData = item,
									count = 1
								}
								);
			}
				
				RefreshContent();
		}

    public void RemoveItem(ItemData item, int quantity = 1){
        ItemInStoreInventory existingItem = content.Find(elem => elem.itemData == item);

        if (existingItem != null){
            existingItem.count -= quantity;

            if (existingItem.count <= 0){
                content.Remove(existingItem);
            }
            Debug.Log("Retrait de " + quantity + " " + item.itemName);
        }

        RefreshContent();
        
    }

    public List<ItemInStoreInventory> GetContent(){
        return content;
    }

	public void RefreshContent(){

		for (int i = 0; i < inventorySlotsParent.childCount; i++){

            // ItemInStoreInventory currentItem = content[i];

			Slot currentSlot = inventorySlotsParent.GetChild(i).GetComponent<Slot>();

			currentSlot.item = null;

			currentSlot.itemVisual.sprite = emptySlotVisual;
			currentSlot.countText.enabled = false;
            // currentSlot.item = currentItem.itemData;
        }
        
		for (int i = 0; i < content.Count; i++){

			ItemInStoreInventory currentItem = content[i];

            Slot currentSlot = inventorySlotsParent.GetChild(i).GetComponent<Slot>();

			currentSlot.item = currentItem.itemData;
			currentSlot.itemVisual.sprite = currentItem.itemData.inventoryVisual;
					
            if (currentSlot.item.stackable){

                currentSlot.countText.enabled = true;
                currentSlot.countText.text = currentItem.count.ToString();
            }
					
        }
	}

        // Fonction pour randomizer le contenu
    public void RandomizeInventory()
    {
        // Mélanger le pool d'objets possibles
        possibleItems = possibleItems.OrderBy(item => UnityEngine.Random.value).ToList();

        // Efface l'inventaire actuel
        content.Clear();

        // Remplit l'inventaire avec un nombre aléatoire d'objets du pool
        int itemCount = UnityEngine.Random.Range(1, InventorySize); // Vous pouvez ajuster les limites en fonction de vos besoins
        for (int i = 0; i < itemCount && i < possibleItems.Count; i++)
        {
            content.Add(new ItemInStoreInventory
            {
                itemData = possibleItems[i],
                count = 1
            });
        }

        RefreshContent(); // Assurez-vous de rafraîchir l'affichage après la randomisation.
    }
    public bool IsFull(){
        return content.Count >= InventorySize;
    }
}

[System.Serializable]
public class ItemInStoreInventory{
	public ItemData itemData;
	public int count;

}
