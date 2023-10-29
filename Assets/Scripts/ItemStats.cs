using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class ItemStats : MonoBehaviour
{
    [Header("Quantity")]
    public List<Item> playerItems = new List<Item>();
    
    [Header("Reference")]

    [SerializeField]
    Rigidbody2D rb2D;

    [SerializeField]
    private ContainerStats containerStats;

    [SerializeField]
    private Draggable draggable;

    [SerializeField]
    private ItemData item;

    public float currentQuantity;

    public static ItemStats instance;

    [SerializeField]
    private WastedLiquid wasted;

    public GameObject raycastHitObject;

    public ItemData itemData;

    [Header("Quantity")]

    public bool isPouring = false;

    [SerializeField]
    public float pouringRate = 10.0f;

    public float poured = 0f;

    public float pouringFloatConv = 0f;

    [SerializeField]
    private Image quantityBarFill;

    public float currentQuantityConvItem;

    public bool isEmpty = false;

    private bool hasRotated = false;





    void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        currentQuantity = item.maxQuantity;
        draggable = GetComponent<Draggable>();
        instance = this;
    }

    void FixedUpdate(){
            if(draggable.isUsed){
                
                if(!isEmpty){

                    isPouring = true;

                    // Cast a ray straight down.
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, Mathf.Infinity, LayerMask.GetMask("Container"));

                    float poured =+ pouringRate * Time.deltaTime;

                    Debug.Log("poured"+ poured);

                    WastedLiquid wastedLiq = wasted.GetComponent<WastedLiquid>();
                    wastedLiq.SetItemStats(this);

                    // If it hits something...
                    if (hit.collider != null){                    
                        if (hit.collider.GetComponent<ContainerStats>() != null)
                        {
                            Inventory containerInventory = hit.collider.GetComponent<Inventory>();

                            Item currentItem = transform.GetComponent<Item>();

                            ContainerStats container = hit.collider.GetComponent<ContainerStats>();
                            container.SetItemStat(this);
                            container.convItemQuantity = Mathf.CeilToInt(container.currentQuantity);

                            containerInventory.AddItem(currentItem.itemData);


                            // if(!containerInventory.itemAdded){                         
                            //     containerInventory.AddItem(currentItem.itemData, container.convItemQuantity);
                            //     // containerInventory.itemAdded = true;
                            // }                
                            // if(containerInventory.itemAdded  && currentItem.itemData.stackable){                
                            //     containerInventory.AddItem(currentItem.itemData, container.convItemQuantity);
                            //     // containerInventory.itemAdded = true;
                            // }

                            container.ReceiveLiquid(currentItem, poured);
                            PourLiquid(poured);
                        }
                        // ...
                        else{
                            Debug.Log("Pas de collider");
                            // Debug.Log("Ce n'est pas un container");
                            PourLiquid(poured);
                            wastedLiq.WasteLiquid(poured);
                        }
                    }
                    else{
                        // Debug.Log("Oops c'est gaspille");
                        //Ajouter ItemData Item en argumnet ?
                        wastedLiq.WasteLiquid(poured);
                        PourLiquid(poured);
                    }
                }        
            if(!hasRotated)
                {
                    ItemRotate();
                }
            }
        }

   
    private void ItemRotate(){
        hasRotated = true;
        if(draggable.isUsed)
        {
        transform.Rotate(Vector3.forward, 90f);
        }
        else{
            transform.Rotate(Vector3.down, 45f);
        }
    }
    public void PourLiquid(float poured, bool overTime=false){
        if(draggable.isUsed && !isEmpty){
            isPouring = true;
            currentQuantity -= poured * Time.deltaTime;
        }
        if(currentQuantity <= 0 && !isEmpty){
            EmptyContainer();
            isEmpty = true;
            isPouring = false;
        }
        if(currentQuantity < 0){
            currentQuantity = 0;
        };
    }
    private void EmptyContainer()
    {
        isEmpty = true;
        isPouring = false;
        Debug.Log("Bah Bravo Morray");
    }

    // void UpdateQuantityBarFill()
    // {
    //     quantityBarFill.fillAmount = item.currentQuantity / item.maxQuantity;
    // }
}
