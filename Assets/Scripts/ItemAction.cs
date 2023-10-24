using System.Numerics;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ItemAction : MonoBehaviour
{
	[Header("Other Script References")]
		
	public GameObject actionPanel;

	[SerializeField]
	private PlayerStats playerStats;

    [SerializeField]  
    private MarketPlace marketPlace;

	[Header("Inventory System")]

    [SerializeField]
    private GameObject sellItemButton;
    
    [SerializeField]
    private GameObject buyItemButton;

    [SerializeField]
    private GameObject storeActionPanel;
	
	[HideInInspector]
	public ItemData itemCurrentlySelected;
	
	public void OpenActionPanel(ItemData item, UnityEngine.Vector3 slotPosition){
		itemCurrentlySelected = item;
			
		if(item == null){

			actionPanel.SetActive(false);
			return;
		}

        sellItemButton.SetActive(true);		
		actionPanel.transform.position = slotPosition;
		actionPanel.SetActive(true);
	}
		
    public void OpenStoreActionPanel(ItemData item, UnityEngine.Vector3 slotStorePosition){

        itemCurrentlySelected = item;
        if(item == null){

            storeActionPanel.SetActive(false);
            return;
        }

        storeActionPanel.SetActive(true);
        storeActionPanel.transform.position = slotStorePosition;
        buyItemButton.SetActive(true);

        Button buyButtonComponent = buyItemButton.GetComponent<Button>();

        buyButtonComponent.interactable = false;
        CheckMoney();
    }

    public void CheckMoney(){

        Button buyButtonComponent = buyItemButton.GetComponent<Button>();
            
        if(playerStats.currentMoney >= itemCurrentlySelected.buyingPrice){

            buyButtonComponent.interactable = true;
        }
        else{

            buyButtonComponent.interactable = false;
        }
    }

    public bool IsItemInInventory(ItemData item){

        return PlayerInventory.instance.GetContent().Any(inventoryItem => inventoryItem.itemData == item);
    }

	public void CloseActionPanel(){

		actionPanel.SetActive(false);
		itemCurrentlySelected = null;
	}

	public void SellItemButton(){

        if (itemCurrentlySelected != null){

            if (IsItemInInventory(itemCurrentlySelected)){
                
                PlayerInventory.instance.RemoveItem(itemCurrentlySelected);
                playerStats.AddMoney(itemCurrentlySelected.sellingPrice);
                StoreInventory.instance.AddItem(itemCurrentlySelected);
                PlayerInventory.instance.RefreshContent();
            }
            else{
                
                PlayerInventory.instance.RefreshContent();
            }
        }
    }

    public void BuyItemButton(){

        if(playerStats.currentMoney >= itemCurrentlySelected.buyingPrice){

            PlayerInventory.instance.AddItem(itemCurrentlySelected);
            playerStats.RemoveMoney(itemCurrentlySelected.buyingPrice);
            StoreInventory.instance.RemoveItem(itemCurrentlySelected);
            PlayerInventory.instance.RefreshContent();
            StoreInventory.instance.RefreshContent();
            CheckMoney();
        }
        else{

            CheckMoney();
        }
    }
		
		
		// public void DropActionButton()
		// {
		// 	GameObject instantatiedItem = Instantiate(itemCurrentlySelected.prefab);
		// 	instantatiedItem.transform.position = dropPoint.position;
		// 	Inventory.instance.RemoveItem(itemCurrentlySelected);
		// 	CloseActionPanel();
		// 	Inventory.instance.RefreshContent();
		// }
		
		// public void DestroyActionButton()
		// {
		// 	Inventory.instance.RemoveItem(itemCurrentlySelected);
		// 	CloseActionPanel();
			
		// }
}
