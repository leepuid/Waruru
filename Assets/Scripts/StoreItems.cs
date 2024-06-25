using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreItems : MonoBehaviour
{
    [SerializeField] private GameObject equipImage; // 장착 표시
    public Image itemImage;
    public TMP_Text itemName;
    public Material skin;
    private Material skinMaterial;
    private bool isPurchased = false; // 구매 여부

    private bool isEquip = false; // 장착 여부

    private StoreItemsManager storeItemsManager;

    public void SetStoreItemsManager(StoreItemsManager manager)
    {
        storeItemsManager = manager;
    }
    public void SetSkinMaterial(Material material)
    {
        skinMaterial = material;
    }

    public void Purchased()
    {
        if(!isPurchased)
        {
            Debug.Log("구매되지 않았습니다.");
            return;
        }

        if (!isEquip)
        {
            storeItemsManager.EquipSkin(this);
            isEquip = true;
            Debug.Log(itemName.text + "장착 O ");
            equipImage.SetActive(true);
            skinMaterial = skin;
            storeItemsManager.ItemPrefabs.GetComponent<Renderer>().material = skinMaterial;
        }
        else
        {
            Unequip();
        }
    }

    public void Unequip()
    {
        isEquip = false;
        Debug.Log(itemName.text + "장착 X ");
        equipImage.SetActive(false);

        storeItemsManager.UnequipItem(this);
    }
}
