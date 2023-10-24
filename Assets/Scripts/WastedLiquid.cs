using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;



public class WastedLiquid : MonoBehaviour{
    [SerializeField]
    private ItemStats itemStats;

    [SerializeField]
    private float currentQuantity;

    [SerializeField]
    private float maxQuantity = 100f;


      void Awake(){
        currentQuantity = 0f;
    }

    public void SetItemStats(ItemStats stats){
        itemStats = stats;
    }
    
    public void WasteLiquid(float poured, bool overTime=false) {
         // Ajout d'un argument overtime pour actualiser avecle temps
            if(itemStats.isPouring) {
            currentQuantity += poured * Time.deltaTime;
        }
        else{
            // Si pas OverTime/Decrease alors les dégats équivaudront a  une unité - currentArmorpoint / 100
            // currentHealth -= poured* (1 -  (currentArmorPoints / 100)); 
        }

//         UpdateQuantityBarFill();
    }


}
