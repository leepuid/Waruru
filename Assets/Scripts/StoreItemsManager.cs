using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreItemsManager : MonoBehaviour
{
    public GameObject ItemPrefabs;
    public List<StoreItems> storeItemsList;
    private StoreItems currentItem = null;

    private void Start()
    {
        InitializeStoreItems();
    }

    private void InitializeStoreItems()
    {
        for (int i = 0; i < storeItemsList.Count; i++)
        {
            storeItemsList[i].SetSkinID(i);
            storeItemsList[i].SetStoreItemsManager(this);
        }
    }

    public void EquipSkin(StoreItems newItem)
    {
        if (currentItem != null)
        {
            currentItem.Unequip();
        }

        currentItem = newItem;
    }

    public void UnequipItem(StoreItems item)
    {
        if (currentItem == item)
        {
            currentItem = null;
        }
    }
}
