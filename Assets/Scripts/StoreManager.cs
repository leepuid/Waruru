using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefabs;
    [SerializeField] private Material[] skins;
    [SerializeField] private Transform itemGridTransform;
    [SerializeField] private StoreItemsManager storeItemsManager;

    private void Start()
    {
        for(int i = 0; i < skins.Length;  i++)
        {
            GameObject item = Instantiate(itemPrefabs, itemGridTransform);
            StoreItems storeItems = item.GetComponent<StoreItems>();

            storeItems.itemName.text = "스킨 " + (i + 1);
            storeItems.skin = skins[i];
            storeItems.SetStoreItemsManager(storeItemsManager);
            storeItems.SetSkinMaterial(skins[i]);
            storeItems.SetSkinID(i);

            if (i == 0)
            {
                storeItems.Purchased();
            }
        }
    }
}
