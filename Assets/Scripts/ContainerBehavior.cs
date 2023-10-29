using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ContainerBehavior : MonoBehaviour
{
    [SerializeField]
    private GameObject putWoodButton;

    [SerializeField]
    private GameObject putIceButton;

    [SerializeField]
    private GameObject putSaltButton;

    public PlayerInventory playerInventory;

    [SerializeField]
    ThermometerTemp thermometerTemp;

    [SerializeField]
    private Transform parentTransform;

    public float woodSupply;
    public float containerTemp;
    public float targetTemperature;
    
    private bool saltAdded = false;
    public bool iceAdded = false;
    private bool isHeating = false;
    private bool isFreezing = false;
    private float freezingRate = 0.0f;
    private float heatingRate = 0.0f;

    private float maxTemp = 100.0f;
    private float minTemp = 0.0f;
    private float ambiantRate = 1.0f;


    void Start(){
        parentTransform = transform.parent;
        freezingRate = 0.0f;
        heatingRate = 0.0f;
    }
    
    void Update(){
        OverTimeTemp();
        DisplayButtons(parentTransform.position);
    }

    public void OverTimeTemp(){
        if(!isFreezing || !isHeating){
            // Créer une targetTemperature qui est CurrentTemperature et retourner doucement à la currentTemp
            targetTemperature = thermometerTemp.currentTemperature;

            // Si la temperature du container n'est pas = a currentTemperature

            if(containerTemp < targetTemperature)
            {
                containerTemp += ambiantRate * Time.deltaTime;
            }

            if(containerTemp > targetTemperature)
            {
                containerTemp -= ambiantRate * Time.deltaTime;
            }
        }

        if(isFreezing){

            if(saltAdded){

                minTemp = -15.0f;
            }

            if(containerTemp > minTemp){

                containerTemp -= freezingRate * Time.deltaTime;
            }
        }
        
        if(isHeating){

            if(containerTemp < maxTemp){

                containerTemp += heatingRate * Time.deltaTime;
            }
        }

    }

    public void AddWood(){
        isHeating = true;
        heatingRate += 1;
       // a ajouter : Multiplier le Heating Rate selon le nombre d'Items
        Debug.Log("Temp is Raising");
    }

    public void AddIce(){
        isFreezing = true;
        freezingRate += 1;
        iceAdded = true;
        // a ajouter : Multiplier le Freezing Rate selon le nombre d'Items
        Debug.Log("Temp is Lowering");
    }

    public void OnClickWoodButton(){

            var woodItem = playerInventory.content.Find(item => item.itemData.itemName == "Wood");
                if (woodItem != null)
                {
                    AddWood();
                    playerInventory.RemoveItem(woodItem.itemData); // Passer l'ItemData au lieu de l'ItemInPlayerInventory
                }
                else{
                    Debug.Log("Je bug salement");
                }
    }

    public void OnClickIceButton(){
        putIceButton.transform.position = parentTransform.position + new Vector3(2.0f, 0.0f, 0.0f);
        var iceItem = playerInventory.content.Find(item => item.itemData.itemName == "Ice");
        if (iceItem != null)
        {
            playerInventory.RemoveItem(iceItem.itemData); // Passer l'ItemData au lieu de l'ItemInPlayerInventory
            AddIce();
        }
        
    }

    public void OnClickSaltButton(){
        var saltItem = playerInventory.content.Find(item => item.itemData.itemName == "Salt");
        if (saltItem != null)
        {
            playerInventory.RemoveItem(saltItem.itemData); // Passer l'ItemData au lieu de l'ItemInPlayerInventory
            saltAdded = true;
            
        }
        
    }

    public void DisplayButtons(UnityEngine.Vector3 contPosition){

        parentTransform.position = contPosition;

        if (playerInventory.content.Exists(item => item.itemData.itemName == "Salt") && iceAdded ){

                putSaltButton.SetActive(true);
        
                putSaltButton.transform.position = parentTransform.position + new Vector3(3.0f, 0.0f, 0.0f);
        
        }
        else{

            putSaltButton.SetActive(false);
        }

        if (playerInventory.content.Exists(item => item.itemData.itemName == "Ice" && !isHeating)){

                putIceButton.SetActive(true);
                
        }
        else{

            putIceButton.SetActive(false);
        }

        if (playerInventory.content.Exists(item => item.itemData.itemName == "Wood" && !iceAdded)){
            
                putWoodButton.SetActive(true);

                putWoodButton.transform.position = parentTransform.position + new Vector3(1.0f, 0.0f, 0.0f);
                

        }
        else{

            putWoodButton.SetActive(false);
        }
    }
}
