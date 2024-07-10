using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreManager : MonoBehaviour
{
    [Header("Skins")]
    [SerializeField] private GameObject itemPrefabs;
    [SerializeField] private Material[] skins;
    [SerializeField] private Sprite[] skinImages;
    [SerializeField] private Transform itemGridTransform;
    [SerializeField] private StoreItemsManager storeItemsManager;

    [Header("PlaneCube")]
    public Color[] colorPick;
    [SerializeField] private GameObject imagePrefabs;
    [SerializeField] private Transform imageTransform;
    public int planeNumber;
    [SerializeField] private PlanCubeChange planCubeChange;

    private void Start()
    {
        planeNumber = 0;

        for (int i = 0; i < colorPick.Length; i++)
        {
            GameObject go = Instantiate(imagePrefabs, imageTransform);
            Image imageComponent = go.GetComponent<Image>();
            imageComponent.color = colorPick[i];

            int currentNumber = planeNumber;
            Button button = go.GetComponent<Button>();
            if (button != null) 
            {
                button.onClick.AddListener(() => planCubeChange.CubeColorChange(currentNumber));
            }

            Debug.Log("넣는 숫자 " + planeNumber);
            planeNumber++;
        }

        for (int i = 0; i < skins.Length; i++)
        {
            GameObject item = Instantiate(itemPrefabs, itemGridTransform);
            StoreItems storeItems = item.GetComponent<StoreItems>();

            storeItems.itemName.text = "스킨 " + (i + 1);
            storeItems.skin = skins[i];
            storeItems.itemImage.sprite = skinImages[i];
            storeItems.SetStoreItemsManager(storeItemsManager);
            storeItems.SetSkinID(i);

            if (storeItems.IsSkinPurchased())
            {
                storeItems.UpdateDisplay();
            }
            else
            {
                storeItems.UpdateDisplay();
            }

            storeItemsManager.storeItemsList.Add(storeItems);
        }

        storeItemsManager.InitializeStoreItems();
    }
}
