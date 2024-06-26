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
    private StoreItems storeItems;

    private bool isPurchased = false; // 구매 여부

    string[] strings = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };

    private void Start()
    {
        for(int i = 0; i < skins.Length;  i++)
        {
            GameObject item = Instantiate(itemPrefabs, itemGridTransform);
            storeItems = item.GetComponent<StoreItems>();

            item.GetComponent<StoreItems>().itemName.text = strings[i];
            item.GetComponent<StoreItems>().skin = skins[i];
            storeItems.SetStoreItemsManager(storeItemsManager);
            storeItems.SetSkinMaterial(skins[i]);

            // 구매가 되었을 때와 되지 않았을 때를 이미지로 구분
            if (!isPurchased)
            {
                //item.GetComponent<StoreItems>().itemImage.sprite = ;
                item.GetComponent<StoreItems>().itemImage.color = Color.white;
            }
            else
            {
                //item.GetComponent<StoreItems>().itemImage.sprite = ;
                item.GetComponent<StoreItems>().itemImage.color = Color.black;
            }
        }
    }
}
