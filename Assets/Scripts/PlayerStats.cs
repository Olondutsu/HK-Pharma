using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    [Header("Other Elements references")]

    [Header("Money")]
    
    public float currentMoney;

    [SerializeField]
    private float minMoney = 0f;

    [SerializeField]
    private TextMeshProUGUI moneyCountText;


    void Awake()
    {
        currentMoney = minMoney;
        DisplayMoney();
    }

    void Update()
    {
    //    UpdateHungerAndThirstBarFill();
    }

   
    public void AddMoney(float sellingPrice, bool overTime=false) 
    {
        currentMoney += sellingPrice;
        DisplayMoney();
    }

    public void RemoveMoney(float buyingPrice, bool overTime=false) 
    {
        currentMoney -= buyingPrice;
        DisplayMoney();
    }

    public void DisplayMoney(){
        moneyCountText.text = currentMoney.ToString();
    }
    
    // public void ConsumeItem(float health, float hunger, float thirst)
    // {
    //     currentHealth += health;
        
    //     if(currentHealth > maxHealth)
    //     {
    //         currentHealth = maxHealth;
    //     }


    //     currentHunger += hunger;

    //     if(currentHunger > maxHunger)
    //     {
    //         currentHunger = maxHunger;
    //     }


    //     currentThirst += thirst;

    //     if(currentThirst > maxThirst)
    //     {
    //         currentThirst = maxThirst;
    //     }

    //     UpdateHealthBarFill();
    // }

}
