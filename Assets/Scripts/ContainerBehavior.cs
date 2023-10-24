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

    public PlayerInventory playerInventory;

    [SerializeField]
    ThermometerTemp thermometerTemp;

    [SerializeField]
    private Transform parentTransform;
    
    public float woodSupply;
    public float containerTemp;


    void Start(){
        parentTransform = transform.parent;
        containerTemp = thermometerTemp.currentTemperature;
        
        DisplayButtons(parentTransform.position);
    }
    
   
    public void OverTimeTemp(){
    }

    public void RaiseTemp(){
        Debug.Log("Temp is Raising");
    }
    public void LowerTemp(){
        Debug.Log("Temp is Lowering");
    }

    public void OnClickWoodButton(){
            RaiseTemp();
    }

    public void OnClickIceButton(){
            LowerTemp();
    }

    public void DisplayButtons(UnityEngine.Vector3 contPosition){

        parentTransform.position = contPosition;

        if (playerInventory.content.Exists(item => item.itemData.itemName == "Ice")){
            // Le joueur possède du bois, implémentez la logique ici.
                putIceButton.SetActive(true);
                

                putIceButton.transform.position = parentTransform.position + new Vector3(2.0f, 0.0f, 0.0f);
        }
        else{
            putIceButton.SetActive(false);
        }

        if (playerInventory.content.Exists(item => item.itemData.itemName == "Wood")){
            // Le joueur possède du bois, implémentez la logique ici.
                putWoodButton.SetActive(true);

                putWoodButton.transform.position = parentTransform.position + new Vector3(1.0f, 0.0f, 0.0f);
        }
        else{
            putWoodButton.SetActive(false);
        }
    }
}
