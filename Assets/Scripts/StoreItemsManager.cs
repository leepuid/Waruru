using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreItemsManager : MonoBehaviour
{
    //public GameObject ItemPrefabs;
    //public List<StoreItems> storeItemsList;
    //private StoreItems currentItem = null;

    //private void Start()
    //{
    //    InitializeStoreItems();
    //}

    //public void InitializeStoreItems()
    //{
    //    int lastEquippedSkinID = PlayerPrefs.GetInt("LastEquippedSkinID", 0);

    //    for (int i = 0; i < storeItemsList.Count; i++)
    //    {
    //        storeItemsList[i].SetSkinID(i);
    //        storeItemsList[i].SetStoreItemsManager(this);

    //        if (storeItemsList[i].IsSkinPurchased())
    //        {
    //            storeItemsList[i].UpdateDisplay();
    //            if (i == lastEquippedSkinID)
    //            {
    //                EquipSkin(storeItemsList[i]);
    //            }
    //        }
    //        else
    //        {
    //            storeItemsList[i].UpdateDisplay();
    //        }
    //    }
    //}

    //public void EquipSkin(StoreItems newItem)
    //{
    //    foreach (StoreItems item in storeItemsList)
    //    {
    //        if (item != newItem && item.isEquip)
    //        {
    //            item.UnequipInternal();
    //        }
    //    }

    //    currentItem = newItem;
    //    newItem.EquipInternal();

    //    PlayerPrefs.SetInt("LastEquippedSkinID", newItem.skinID);
    //    PlayerPrefs.Save();
    //}

    //public void UnequipItem(StoreItems item)
    //{
    //    if (currentItem == item)
    //    {
    //        currentItem = null;
    //    }
    //}
}
