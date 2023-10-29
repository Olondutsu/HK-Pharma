// // // using UnityEngine;
// // // using UnityEngine.UI;
// // // using UnityEngine.EventSystems;

// // // public class TestSlot : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
// // // {
// // //     private ItemInPlayerInventory itemInSlot;
// // //     private Image itemVisual;
// // //     private CanvasGroup canvasGroup;
// // //     private ItemInPlayerInventory draggingItem;

// // //     private Vector3 originalPosition;
// // //     private Vector3 pointerOffset; // Pour suivre la position relative au clic

// // //     public GameObject itemPrefab;

// // //     private void Awake()
// // //     {
// // //         itemVisual = GetComponentInChildren<Image>(); // Obtenez l'image de l'enfant (le contenu du slot)
// // //         canvasGroup = GetComponent<CanvasGroup>();
// // //     }

// // //     public void OnPointerDown(PointerEventData eventData)
// // //     {
// // //         itemInSlot = GetItemInSlot();

// // //         if (itemInSlot != null)
// // //         {
// // //             canvasGroup.blocksRaycasts = false;
// // //             originalPosition = itemVisual.transform.position;
// // //             pointerOffset = itemVisual.transform.position - Input.mousePosition; // Calculez l'écart entre le clic et la position actuelle de l'image
// // //             draggingItem = itemInSlot;
// // //         }
// // //     }

// // //     public void OnBeginDrag(PointerEventData eventData)
// // //     {
// // //         if (itemInSlot != null)
// // //         {
// // //             // Instanciez un nouvel GameObject (visuel de l'élément)
// // //             GameObject itemVisualGO = new GameObject("ItemVisual");
// // //             Image itemVisualImage = itemVisualGO.AddComponent<Image>();
// // //             itemVisualImage.sprite = itemInSlot.itemData.inventoryVisual;
// // //             itemVisualImage.rectTransform.sizeDelta = new Vector2(40, 40); // Modifiez la taille en fonction de vos besoins

// // //             // Placez l'élément visuel au même endroit que l'élément précédent
// // //             itemVisualGO.transform.position = itemVisual.transform.position;
// // //             itemVisualImage.transform.SetParent(transform); // Assurez-vous que l'élément visuel est un enfant du slot

// // //             itemVisual = itemVisualImage;
// // //         }
// // //     }

// // //     public void OnDrag(PointerEventData eventData)
// // //     {
// // //         if (itemVisual != null)
// // //         {
// // //             itemVisual.transform.position = Input.mousePosition + pointerOffset; // Déplacez l'image selon la position relative au clic
// // //         }
// // //     }

// // //     public void OnEndDrag(PointerEventData eventData)
// // //     {
// // //         if (itemVisual != null)
// // //         {
// // //             canvasGroup.blocksRaycasts = true;

// // //             if (IsDroppedInValidZone(eventData))
// // //             {
// // //                 PlayerInventory.instance.RemoveItem(draggingItem.itemData);

// // //                 // Créez un nouvel objet dans la scène à partir de la préfab
// // //                 GameObject newItemObject = Instantiate(itemPrefab);

// // //                 // Assurez-vous que le nouvel objet a les composants Draggable et ItemStats
// // //                 Draggable newDraggableComponent = newItemObject.GetComponent<Draggable>();
// // //                 ItemStats newItemStatsComponent = newItemObject.GetComponent<ItemStats>();

// // //                 // Copiez les données de l'élément glissé dans le nouvel objet
// // //                 newItemStatsComponent.itemData = draggingItem.itemData;

// // //                 // Positionnez le nouvel objet à l'endroit où vous avez relâché l'objet visuel
// // //                 newItemObject.transform.position = itemVisual.transform.position;

// // //                 Destroy(itemVisual.gameObject); // Détruisez l'élément visuel après le retrait

// // //                 // Vous pouvez également effectuer d'autres actions ici, comme activer le script Draggable sur le nouvel objet
// // //             }
// // //             else
// // //             {
// // //                 itemVisual.transform.position = originalPosition; // Replacez l'image à sa position d'origine
// // //                 Destroy(itemVisual.gameObject); // Détruisez l'élément visuel s'il n'est pas déposé dans une zone valide
// // //             }

// // //             itemInSlot = null;
// // //             itemVisual = null;
// // //         }
// // //     }
// // //     private bool IsDroppedInValidZone(PointerEventData eventData)
// // //     {
// // //         return false;
// // //     }

// // //     private ItemInPlayerInventory GetItemInSlot()
// // //     {
// // //         int slotIndex = GetSlotIndexFromTransform(transform);

// // //         if (slotIndex >= 0 && slotIndex < PlayerInventory.instance.GetContent().Count)
// // //         {
// // //             return PlayerInventory.instance.GetContent()[slotIndex];
// // //         }

// // //         return null;
// // //     }

// // //     private int GetSlotIndexFromTransform(Transform slotTransform)
// // //     {
// // //         for (int i = 0; i < PlayerInventory.instance.inventorySlotsParent.childCount; i++)
// // //         {
// // //             if (PlayerInventory.instance.inventorySlotsParent.GetChild(i) == slotTransform)
// // //             {
// // //                 return i;
// // //             }
// // //         }

// // //         return -1;
// // //     }
// // // }
// // using UnityEngine;
// // using UnityEngine.UI;
// // using UnityEngine.EventSystems;

// // public class TestSlot : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
// // {
// //     private ItemInPlayerInventory itemInSlot;
// //     private Image itemVisual;
// //     private CanvasGroup canvasGroup;
// //     private ItemInPlayerInventory draggingItem;

// //     private Vector3 originalPosition;
// //     private Vector3 pointerOffset; // Pour suivre la position relative au clic

// //     public GameObject itemPrefab; // La préfab de l'objet que vous souhaitez créer dans la scène

// //     private void Awake()
// //     {
// //         itemVisual = GetComponentInChildren<Image>(); // Obtenez l'image de l'enfant (le contenu du slot)
// //         canvasGroup = GetComponent<CanvasGroup>();
// //     }

// //     public void OnPointerDown(PointerEventData eventData)
// //     {
// //         itemInSlot = GetItemInSlot();

// //         if (itemInSlot != null)
// //         {
// //             canvasGroup.blocksRaycasts = false;
// //             originalPosition = itemVisual.transform.position;
// //             pointerOffset = itemVisual.transform.position - Input.mousePosition; // Calculez l'écart entre le clic et la position actuelle de l'image
// //             draggingItem = itemInSlot;
// //         }
// //     }

// //     public void OnBeginDrag(PointerEventData eventData)
// //     {
// //         if (itemInSlot != null)
// //         {
// //             // Instanciez un nouvel GameObject (visuel de l'élément)
// //             // Definir que le nouveau gameObjet ItemVisual est l'ItemVisual (?!)
// //             GameObject itemVisualGO = new GameObject("ItemVisual");

// //             // recuperer l'image de ll'itemvisualimage ? Maybe we should take the whole itemData instead
// //             Image itemVisualImage = itemVisualGO.AddComponent<Image>();
// //             itemVisualImage.sprite = itemInSlot.itemData.inventoryVisual;
// //             itemVisualImage.rectTransform.sizeDelta = new Vector2(40, 40); // Modifiez la taille en fonction de vos besoins

// //             // Placez l'élément visuel au même endroit que l'élément précédent
// //             itemVisualGO.transform.position = itemVisual.transform.position;
// //             itemVisualImage.transform.SetParent(transform); // Assurez-vous que l'élément visuel est un enfant du slot

// //             itemVisual = itemVisualImage;
// //         }
// //     }

// //     public void OnDrag(PointerEventData eventData)
// //     {
// //         if (itemVisual != null)
// //         {
// //             itemVisual.transform.position = Input.mousePosition + pointerOffset; // Déplacez l'image selon la position relative au clic
// //         }
// //     }

// //     // public void OnEndDrag(PointerEventData eventData)
// //     // {
// //     //     if (itemVisual != null)
// //     //     {
// //     //         canvasGroup.blocksRaycasts = true;

// //     //         if (IsDroppedInValidZone(eventData))
// //     //         {
// //     //             PlayerInventory.instance.RemoveItem(draggingItem.itemData);

// //     //             // Créez un nouvel objet dans la scène à partir de la préfab
// //     //             GameObject newItemObject = Instantiate(itemPrefab);

// //     //             // Assurez-vous que le nouvel objet a les composants Draggable et ItemStats
// //     //             Draggable newDraggableComponent = newItemObject.GetComponent<Draggable>();
// //     //             ItemStats newItemStatsComponent = newItemObject.GetComponent<ItemStats>();

// //     //             // Copiez les données de l'élément glissé dans le nouvel objet
// //     //             newItemStatsComponent.itemData = draggingItem.itemData;

// //     //             // Positionnez le nouvel objet à l'endroit où vous avez relâché l'objet visuel
// //     //             newItemObject.transform.position = itemVisual.transform.position;

// //     //             Destroy(itemVisual.gameObject); // Détruisez l'élément visuel après le retrait
// //     //         }
// //     //         else
// //     //         {
// //     //             itemVisual.transform.position = originalPosition; // Replacez l'image à sa position d'origine
// //     //             Destroy(itemVisual.gameObject); // Détruisez l'élément visuel s'il n'est pas déposé dans une zone valide
// //     //         }

// //     //         itemInSlot = null;
// //     //         itemVisual = null;
// //     //     }
// //     // }
// //     public void OnEndDrag(PointerEventData eventData)
// //     {
// //         if (itemVisual != null)
// //         {
// //             canvasGroup.blocksRaycasts = true;

// //             // Trouvez le nouvel emplacement de l'inventaire où l'objet a été relâché
// //             Slot newSlot = GetInventorySlotUnderPointer(eventData);

// //             if (newSlot != null)
// //             {
// //                 // Réorganisez l'objet dans le nouvel emplacement de l'inventaire
// //                 PlayerInventory.instance.ReorganizeItem(draggingItem, newSlot);

// //                 // Vous pouvez mettre en œuvre la logique de réorganisation dans votre script PlayerInventory
// //             }
// //             else
// //             {
// //                 itemVisual.transform.position = originalPosition; // Replacez l'image à sa position d'origine
// //             }

// //             Destroy(itemVisual.gameObject); // Détruisez l'élément visuel après le retrait

// //             itemInSlot = null;
// //             itemVisual = null;
// //         }
// //     }

// //     private bool IsDroppedInValidZone(PointerEventData eventData)
// //     {
// //         return false;
// //     }

// //     private ItemInPlayerInventory GetItemInSlot()
// //     {
// //         int slotIndex = GetSlotIndexFromTransform(transform);

// //         if (slotIndex >= 0 && slotIndex < PlayerInventory.instance.GetContent().Count)
// //         {
// //             return PlayerInventory.instance.GetContent()[slotIndex];
// //         }

// //         return null;
// //     }

// //     private int GetSlotIndexFromTransform(Transform slotTransform)
// //     {
// //         for (int i = 0; i < PlayerInventory.instance.inventorySlotsParent.childCount; i++)
// //         {
// //             if (PlayerInventory.instance.inventorySlotsParent.GetChild(i) == slotTransform)
// //             {
// //                 return i;
// //             }
// //         }

// //         return -1;
// //     }
// //     private Slot GetInventorySlotUnderPointer(PointerEventData eventData)
// // {   
// //     // Utilisez eventData.pointerEnter pour obtenir le GameObject sur lequel l'objet a été déposé
// //     GameObject droppedOn = eventData.pointerEnter;

// //     if (droppedOn != null)
// //     {
// //         // Essayez d'obtenir le composant InventorySlot depuis le GameObject sur lequel l'objet a été déposé
// //         Slot newSlot = droppedOn.GetComponent<Slot>();

// //         if (newSlot != null)
// //         {
// //             return newSlot;
// //         }
// //     }

// //     return null;
// //     }
// // }
// using UnityEngine;
// using UnityEngine.UI;
// using UnityEngine.EventSystems;

// public class TestSlot : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
// {
//     private ItemInPlayerInventory itemInSlot;
//     private Image itemVisual;
//     private CanvasGroup canvasGroup;
//     private ItemInPlayerInventory draggingItem;
//     private Vector3 originalPosition;
//     private Vector3 pointerOffset;
//     public GameObject itemPrefab;

//     public GameObject validDropPanel;

//     private void Awake()
//     {
//         //Recup les Component
//         itemVisual = GetComponentInChildren<Image>();
//         canvasGroup = GetComponent<CanvasGroup>();
//     }

//     public void OnPointerDown(PointerEventData eventData)
//     {
//         // Recup ItemInSlot avec la methode GetItemInSlot (fonctionne but should add ItemData instead of just visual ?)
//         itemInSlot = GetItemInSlot();


//         if (itemInSlot != null)
//         {
//             // Bloc raycast chomu ? ne znayu
//             canvasGroup.blocksRaycasts = false;
//             // recup la position originale
//             originalPosition = itemVisual.transform.position;
//             // Commencer a calculer l'offset du  Pointer ?
//             pointerOffset = itemVisual.transform.position - Input.mousePosition;
//             // draggingItem = itemInSlot
//             draggingItem = itemInSlot;
//         }
//     }

//     public void OnBeginDrag(PointerEventData eventData)
//     {
//         if (itemInSlot != null)
//         {
//             // Initialisation d'un GameObject ItemVisualGO en créant un nouvel gameObet ItemVisuel (qui s'appelle ainsi dans la scène en vrai c pas mal lce truc là)
//             GameObject itemVisualGO = new GameObject("ItemVisual");
//             // Y a jouter le component Image
//             Image itemVisualImage = itemVisualGO.AddComponent<Image>();
//             // faire en sorte que l'image de ce truc soit l'inventoryvisual de mon objet
//             itemVisualImage.sprite = itemInSlot.itemData.inventoryVisual;

//             // changemnt du RectTransform pour une question que j'ignore
//             itemVisualImage.rectTransform.sizeDelta = new Vector2(40, 40);

//             // Changer la position par rapport a ?
//             itemVisualGO.transform.position = itemVisual.transform.position;

//             // Set Parent comme etant le Transform
//             itemVisualImage.transform.SetParent(transform);

//             itemVisual = itemVisualImage;
//         }
//     }

//     public void OnDrag(PointerEventData eventData)
//     {
//         if (itemVisual != null)
//         {
//             // Calcul de la nouvelle position du transform
//             itemVisual.transform.position = Input.mousePosition + pointerOffset;
//         }
//     }

//     // public void OnEndDrag(PointerEventData eventData)
//     // {
//     //     if (itemVisual != null)
//     //     {
//     //         // Bloquer les raycasts du CanvasGroup
//     //         canvasGroup.blocksRaycasts = true;

//     //         if (IsDroppedInValidZone(eventData))
//     //         {
//     //             // Si c'est une zone valide, supprime rl'item et spawn l'item
//     //             PlayerInventory.instance.RemoveItem(draggingItem.itemData);
//     //             SpawnItem(draggingItem.itemData);
//     //         }
//     //         else
//     //         {   // ligne suivante est ajout Debug
//     //             SpawnItem(draggingItem.itemData);
                
//     //             itemVisual.transform.position = originalPosition;
//     //             Destroy(itemVisual.gameObject);
//     //         }

//     //         itemInSlot = null;
//     //         itemVisual = null;
//     //     }
//     // }
//     public void OnEndDrag(PointerEventData eventData)
//     {
//         if (itemVisual != null)
//         {
//             Vector3 spawnWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            
//             // Activer les raycasts du CanvasGroup
//             canvasGroup.blocksRaycasts = true;

//             if (IsDroppedInValidZone(eventData))
//             {
//                 // Si c'est une zone valide, supprimez l'élément de l'inventaire et instanciez-le à la position de la souris
//                 PlayerInventory.instance.RemoveItem(draggingItem.itemData);
//                 SpawnItem(draggingItem.itemData, Input.mousePosition);
//             }
//             else
//             {
//                 SpawnItem(draggingItem.itemData, Input.mousePosition);
//                 itemVisual.transform.position = originalPosition; // Replacez l'image à sa position d'origine
//             }

//             Destroy(itemVisual.gameObject);
//             itemInSlot = null;
//             itemVisual = null;
//         }
//     }

//     private bool IsDroppedInValidZone(PointerEventData eventData)
//     {
//         // Obtenez le GameObject sur lequel l'objet a été déposé
//         GameObject droppedObject = eventData.pointerEnter;

//         // Vérifiez si l'objet a été déposé dans le GameObject du panneau valide
//         if (droppedObject != null && droppedObject == validDropPanel) {
//             // Si l'objet a été déposé dans le panneau valide, c'est une zone valide
//             return true;
//         }

//         return false;
//     }

//     private ItemInPlayerInventory GetItemInSlot()
//     {
//         int slotIndex = GetSlotIndexFromTransform(transform);

//         if (slotIndex >= 0 && slotIndex < PlayerInventory.instance.GetContent().Count)
//         {
//             return PlayerInventory.instance.GetContent()[slotIndex];
//         }

//         return null;
//     }

//     private int GetSlotIndexFromTransform(Transform slotTransform)
//     {
//         for (int i = 0; i < PlayerInventory.instance.inventorySlotsParent.childCount; i++)
//         {
//             if (PlayerInventory.instance.inventorySlotsParent.GetChild(i) == slotTransform)
//             {
//                 return i;
//             }
//         }

//         return -1;
//     }

//     // private void SpawnItem(ItemData itemData)
//     // {
//     //     // Créez un nouvel objet dans la scène à partir de la préfab
//     //     GameObject newItemObject = Instantiate(itemPrefab);

//     //     // Assurez-vous que le nouvel objet a les composants Draggable et ItemStats
//     //     Draggable newDraggableComponent = newItemObject.GetComponent<Draggable>();
//     //     ItemStats newItemStatsComponent = newItemObject.GetComponent<ItemStats>();

//     //     // Copiez les données de l'élément glissé dans le nouvel objet
//     //     newItemStatsComponent.itemData = itemData;

//     //     // Positionnez le nouvel objet à l'endroit où vous avez relâché l'objet visuel
//     //     newItemObject.transform.position = itemVisual.transform.position;

//     //     Destroy(itemVisual.gameObject); // Détruisez l'élément visuel après le retrait
//     // }

//     // lla derniere quetait interessante mais en vrai pas tant que ça celle du dessus est mieux 
//     // private void SpawnItem(ItemData itemData)
//     // {
//     //     // Créez un nouvel objet dans la scène à partir de la préfab
//     //     GameObject newItemObject = Instantiate(itemPrefab);

//     //     // Assurez-vous que le nouvel objet a les composants Draggable et ItemStats
//     //     Draggable newDraggableComponent = newItemObject.GetComponent<Draggable>();
//     //     ItemStats newItemStatsComponent = newItemObject.GetComponent<ItemStats>();

//     //     // Copiez les données de l'élément glissé dans le nouvel objet
//     //     newItemStatsComponent.itemData = itemData;

//     //     // Convertissez les coordonnées de l'écran en coordonnées du monde pour définir la position de l'objet
//     //     Vector3 spawnScreenPosition = new Vector3(Screen.width / 2, Screen.height / 2, 0); // Les coordonnées sont au milieu de l'écran
//     //     Vector3 spawnWorldPosition = Camera.main.ScreenToWorldPoint(spawnScreenPosition);

//     //     newItemObject.transform.position = spawnWorldPosition;

//     //     Destroy(itemVisual.gameObject); // Détruisez l'élément visuel après le retrait
//     // }
//     private void SpawnItem(ItemData itemData, Vector3 spawnPosition)
//     {
//         // Créez un nouvel objet dans la scène à partir de la préfab
//         GameObject newItemObject = Instantiate(itemPrefab);
        

//         // Assurez-vous que le nouvel objet a les composants Draggable et ItemStats
//         Draggable newDraggableComponent = newItemObject.GetComponent<Draggable>();
//         ItemStats newItemStatsComponent = newItemObject.GetComponent<ItemStats>();

//         // Copiez les données de l'élément glissé dans le nouvel objet
//         newItemStatsComponent.itemData = itemData;

//         // Définissez la position de l'objet sur la position de la souris
//         newItemObject.transform.position = spawnPosition;

//         Destroy(itemVisual.gameObject); // Détruisez l'élément visuel après le retrait
//     }
// }
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TestSlot : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler
{
    private ItemInPlayerInventory itemInSlot;
    private Image itemVisual;
    private CanvasGroup canvasGroup;
    private ItemInPlayerInventory draggingItem;
    private Vector3 originalPosition;
    private Vector3 pointerOffset;
    public GameObject itemPrefab;

    private void Awake()
    {
        itemVisual = GetComponentInChildren<Image>();
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        itemInSlot = GetItemInSlot();

        if (itemInSlot != null)
        {
            canvasGroup.blocksRaycasts = false;
            originalPosition = itemVisual.transform.position;
            pointerOffset = itemVisual.transform.position - Input.mousePosition;
            draggingItem = itemInSlot;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (itemInSlot != null)
        {
            GameObject itemVisualGO = new GameObject("ItemVisual");
            Image itemVisualImage = itemVisualGO.AddComponent<Image>();
            itemVisualImage.sprite = itemInSlot.itemData.inventoryVisual;
            itemVisualImage.rectTransform.sizeDelta = new Vector2(40, 40);
            itemVisualGO.transform.position = itemVisual.transform.position;
            itemVisualImage.transform.SetParent(transform);
            itemVisual = itemVisualImage;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (itemVisual != null)
        {
            itemVisual.transform.position = Input.mousePosition + pointerOffset;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (itemVisual != null)
        {
            canvasGroup.blocksRaycasts = true;

            // Convertissez les coordonnées de l'écran en coordonnées du monde pour définir la position de l'objet
            Vector3 spawnWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            // Utilisez la méthode SpawnItem pour instancier l'objet à la position de la souris
            SpawnItem(draggingItem.itemData, spawnWorldPosition);

            Destroy(itemVisual.gameObject);
        }
    }

    private void SpawnItem(ItemData itemData, Vector3 spawnPosition)
    {
         spawnPosition.z = 0.1f;
        // Créez un nouvel objet dans la scène à partir de la préfab
        GameObject newItemObject = Instantiate(itemPrefab, spawnPosition, Quaternion.identity);

        // Assurez-vous que le nouvel objet a les composants Draggable et ItemStats
        Draggable newDraggableComponent = newItemObject.GetComponent<Draggable>();
        ItemStats newItemStatsComponent = newItemObject.GetComponent<ItemStats>();

        // Copiez les données de l'élément glissé dans le nouvel objet
        newItemStatsComponent.itemData = itemData;
    }

    private ItemInPlayerInventory GetItemInSlot()
    {
        int slotIndex = GetSlotIndexFromTransform(transform);

        if (slotIndex >= 0 && slotIndex < PlayerInventory.instance.GetContent().Count)
        {
            return PlayerInventory.instance.GetContent()[slotIndex];
        }

        return null;
    }

    private int GetSlotIndexFromTransform(Transform slotTransform)
    {
        for (int i = 0; i < PlayerInventory.instance.inventorySlotsParent.childCount; i++)
        {
            if (PlayerInventory.instance.inventorySlotsParent.GetChild(i) == slotTransform)
            {
                return i;
            }
        }

        return -1;
    }
}