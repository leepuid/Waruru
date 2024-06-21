using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StoreManager : MonoBehaviour
{
    [SerializeField] private GameObject itemPrefabs;
    [SerializeField] private Transform itemGridTransform;

    private bool isPurchased = false; // 구매 여부

    string[] strings = { "1", "2", "3", "4", "5", "6", "7", "8", "9" };

    private void Start()
    {
        for(int i = 0; i < strings.Length;  i++)
        {
            GameObject item = Instantiate(itemPrefabs, itemGridTransform);
            item.GetComponent<StoreItems>().itemName.text = strings[i];

            if (!isPurchased)
            {
                //item.GetComponent<StoreItems>().itemImage.sprite = ;
            }
            else
            {
                //item.GetComponent<StoreItems>().itemImage.sprite = ;
            }
        }
    }
}
