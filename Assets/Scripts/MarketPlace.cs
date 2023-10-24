using UnityEngine;

public class MarketPlace : MonoBehaviour
{
    [HideInInspector]
    public ItemData itemCurrentlySelected;

    [SerializeField]
    private PlayerStats playerStats;

    public void OnSellItem(ItemData item, Vector3 slotPosition){

        itemCurrentlySelected = item;
        Debug.Log("Pas d'Item ici");

            if(item == null)
                    {
                        // actionPanel.SetActive(false);
                        // return;*
                        playerStats.AddMoney(item.sellingPrice);
                    }

            

            // référence playerStats plus haut + fonction TakeDamage avec le damageDealt défini en réf
        }
    public void OnBuyItem(ItemData item){

        itemCurrentlySelected = item;
        
    }
}