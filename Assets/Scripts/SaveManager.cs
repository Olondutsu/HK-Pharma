using System.Collections.Generic;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    [SerializeField]
    private PlayerInventory playerInventory;

     [SerializeField]
    private PlayerStats playerStats;

    private void Start()
    {
        // Assurez-vous d'avoir référencé les composants PlayerInventory et PlayerStats
        playerInventory = GetComponent<PlayerInventory>();
        playerStats = GetComponent<PlayerStats>();

        // Charge les données sauvegardées au démarrage du jeu
        LoadData();
    }

    public void SaveData()
    {
        // Utilise PlayerPrefs pour sauvegarder les données
        string inventoryData = JsonUtility.ToJson(playerInventory.content);
        PlayerPrefs.SetString("InventoryData", inventoryData);

        // Sauvegarde les données de currentMoney et currentTension
        PlayerPrefs.SetFloat("CurrentMoney", playerStats.currentMoney);
        PlayerPrefs.SetInt("CurrentTension", playerStats.currentTension);

        // Sauvegarde les données
        PlayerPrefs.Save();
    }

    public void LoadData()
    {
        // Charge les données sauvegardées de currentMoney et currentTension
        if (PlayerPrefs.HasKey("CurrentMoney"))
        {
            playerStats.currentMoney = PlayerPrefs.GetFloat("CurrentMoney");
        }

        if (PlayerPrefs.HasKey("CurrentTension"))
        {
            playerStats.currentTension = PlayerPrefs.GetInt("CurrentTension");
        }

        // Charge les données sauvegardées de la liste content
        if (PlayerPrefs.HasKey("InventoryData"))
        {
            string inventoryData = PlayerPrefs.GetString("InventoryData");
            Debug.Log("Loaded Inventory Data: " + inventoryData); // Ajoutez cette ligne
            List<ItemInPlayerInventory> loadedInventory = JsonUtility.FromJson<List<ItemInPlayerInventory>>(inventoryData);
            Debug.Log("Loaded Inventory Count: " + loadedInventory.Count); // Ajoutez cette ligne
            playerInventory.content = loadedInventory;
        }
    }
}