using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreItemsManager : MonoBehaviour
{
    public GameObject ItemPrefabs;
    private StoreItems currentItem = null;

    public StoreItemsManager()
    {
        currentItem = null;
    }

    public void EquipSkin(StoreItems newItem)
    {
        if(currentItem != null)
        {
            currentItem.Unequip();
        }

        currentItem = newItem;
    }

    public void UnequipItem(StoreItems item)
    {
        if(currentItem == item)
        {
            currentItem = null;
        }
    }
}
