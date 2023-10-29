using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrefabLibrary : MonoBehaviour
{
    public List<ItemPrefabLibrary> content = new List<ItemPrefabLibrary>();
}

[System.Serializable]
public class ItemPrefabLibrary
{
    public ItemData itemData;
    public GameObject itemPrefab;
    public GameObject[] associatedElements;
}