using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StoreItems : MonoBehaviour
{
    [SerializeField] private GameObject equipImage; // 장착 표시
    public Image itemImage; // 스킨 이미지
    public TMP_Text itemName;   // 스킨 이름
    public Material skin; // 머터리얼 데이터 
    private int price = 2; // 스킨 가격
    private Material skinMaterial; // 넘겨줄 머터리얼
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

    private void SkinBuy()
    {
        if (UIManager.money >= price)
        {
            UIManager.money -= price;
            isPurchased = true;
        }
        else
        {
            Debug.Log("돈이 부족합니다.");
        }
    }

    public void Purchased()
    {
        if(isPurchased)
        {
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
        else
        {
            Debug.Log("구매되어 있지 않습니다. 그러므로 구매하겠습니다.");
            SkinBuy();
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
