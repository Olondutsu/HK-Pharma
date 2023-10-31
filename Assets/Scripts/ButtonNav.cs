using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ButtonNav : MonoBehaviour{

    public GameObject navCanvas;
    public GameObject mainCanvas;
    public GameObject storePanel;
    public GameObject navButtonDown;
    public GameObject navButtonUp;
    public GameObject sleepButton;
    public GameObject City2Destroyed;
    public GameObject City2;
    public GameObject navDynamite;
    [SerializeField]
    private TimeManager timeManager;
    [SerializeField]
    ThermometerTemp thermometerTemp;
    [SerializeField]
    private ParticleSystem[] particleSystems;
    public List<ItemData> possibleItems;
    [SerializeField]
    private StoreInventory storeInventory;
    [SerializeField]
    private PlayerInventory playerInventory;
    public bool isExploded = true;
    public bool isShopOpened = false;

    void Start(){
        isExploded = false;
        ParticleSystem[] particleSystems = GetComponentsInChildren<ParticleSystem>();
    
    }

    public void OnClickNavButtonUp(){
        navCanvas.SetActive(true);
        mainCanvas.SetActive(false);
        storePanel.SetActive(false);
        navButtonUp.SetActive(false);
        navButtonDown.SetActive(true);
        
    }
    public void OnClickNavButtonDown(){

        if(!isShopOpened)
        {
            mainCanvas.SetActive(true);
            navCanvas.SetActive(false);
            navButtonDown.SetActive(false);
            navButtonUp.SetActive (true);
        }

        if(isShopOpened)
        {
            mainCanvas.SetActive(false);
            navCanvas.SetActive(true);
            isShopOpened = false;
            navButtonDown.SetActive(true);
            navButtonUp.SetActive(false);
            storePanel.SetActive(false);
        }
    }

    public void OnShopClick(){
        isShopOpened = true;
        mainCanvas.SetActive(false);
        navCanvas.SetActive(false);
        storePanel.SetActive(true);
        navButtonDown.SetActive(true);
    }
    
    public void OnClickDynamite(){
        isExploded = true;
        PostDynamite();
        foreach (ParticleSystem ps in particleSystems)
        {
            ps.Emit(1);
        }
    }
 
    public void PostDynamite(){

        var dynamItem = playerInventory.content.Find(item => item.itemData.itemName == "Dynamite");
        
        if (dynamItem != null)
        {
            if(isExploded){
                navDynamite.SetActive(false);
                City2Destroyed.SetActive(true);
                City2.SetActive(false);
            
            }  
        }
    }

    public void OnTreesClick(){
        GatherWood();
    }

    public void GatherWood(){
        int amount = UnityEngine.Random.Range(3, 6);
        
        ItemData woodItem = possibleItems.Find(item => item.itemName == "Wood");

        if (woodItem != null)
            {
                playerInventory.AddItem(woodItem, amount);
            }
    }

    public void OnLakeClick(){
        GatherWater();
    }

    public void GatherWater(){
        int amount = UnityEngine.Random.Range(1, 2);
        
        ItemData waterItem = possibleItems.Find(item => item.itemName == "Water");

        if (waterItem != null)
            {
                playerInventory.AddItem(waterItem, amount);
            }
    }

    public void OnSleepClick(){

        timeManager.TimeUpdate();
        timeManager.SeasonHandler();
        thermometerTemp.RefreshTemp();
        storeInventory.RandomizeInventory();
        storeInventory.RefreshContent();
        playerInventory.RefreshContent();
    }
}
