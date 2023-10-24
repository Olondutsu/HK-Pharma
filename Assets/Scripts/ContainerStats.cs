using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ContainerStats : MonoBehaviour{

    [Header("Quantity")]
    public GameObject raycastHitObject;

    [SerializeField]
    public float currentQuantity;

    [SerializeField]
    public ItemData[] separateQuantities;

    [SerializeField]
    public float currentQuantityConv;

    [SerializeField]
    public int maxQuantity = 150;

    [SerializeField]
    private Image quantityBarFill;

    private ItemStats itemStats;

    public int convItemQuantity;
    
    public Inventory inventoryContainer;

    public static ContainerStats instance;

    [SerializeField]
    public List<ContainerItem> containerItems = new List<ContainerItem>();
    
    void Awake(){
        currentQuantity = 0;
    }

    void Update(){
    }

//Important car jappelle cette methode avec le RAycast2D
    public void SetItemStat(ItemStats stats){
        itemStats = stats;
    }

public void ReceiveLiquid(Item item, float poured, bool overTime=false) 
    {
        Item currentItem = itemStats.transform.GetComponent<Item>();

        if(itemStats.isPouring){
            currentQuantity += poured * Time.deltaTime;           
        }

        if(currentQuantity >= 99.90){  
            currentQuantity = 100;
        }
         ContainerItem containerItem = containerItems.Find(ci => ci.item == item);

        if (containerItem != null){
        // L'item existe dans la liste, mettez à jour sa quantité
        containerItem.quantity = convItemQuantity;
        
        }
        else
        {
        
        convItemQuantity = 0;
        // L'item n'existe pas encore, ajoutez-le à la liste
        containerItems.Add(new ContainerItem { item = item, quantity = poured });
        }
    }
}

[System.Serializable]
public class ContainerItem
{
    public Item item; // L'objet
    public float quantity; // La quantité de l'objet
}