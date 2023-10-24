using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour
// , IPointerEnterHandler, IPointerExitHandler
{
	public ItemData item;
	public Image itemVisual;
	public Text countText;
    public GameObject sellButton;
	
	//Dragging
	private bool isDragging = false;
    private Vector3 offset;
    public MarketPlace marketPlace;
	[SerializeField]
	private ItemAction itemAction;
	
	// public void OnPointerEnter(PointerEventData eventData){
	// 	if(item != null)
	// 	{
	// 	ToolTipSystem.instance.Show(item.description, item.name);
	// 	}
	// }
	
	// public void OnPointerExit(PointerEventData eventData)
	// {
	// 	ToolTipSystem.instance.Hide();
	// }
	
	public void ClickOnSlot(){
		itemAction.OpenActionPanel(item, transform.position);	
	}

    public void ClickOnStoreSlot(){
		itemAction.OpenStoreActionPanel(item, transform.position);
	}

    public void ClickOnSell(){
        marketPlace.OnSellItem(item, transform.position);
    }

    private void OnMouseDown(){
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;
    }

    private void OnMouseDrag(){
        if (isDragging){
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            transform.position = newPosition;
        }
    }

    private void OnMouseUp(){
        isDragging = false;
    }

}
