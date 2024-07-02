using GooglePlayGames;
using System;
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
    public bool isPurchased = false; // 구매 여부
    public bool isEquip = false; // 장착 여부
    public int skinID; // 스킨 번호
    private StoreItemsManager storeItemsManager;

    private void Awake()
    {
        SetSkinID(skinID);

        isPurchased = IsSkinPurchased();

        if (isDefaultSkin())
        {
            isPurchased = true;
            PlayerPrefs.SetInt("Skin" + skinID.ToString(), 1);
            PlayerPrefs.SetInt("SkinEquip" + skinID.ToString(), 1);
            PlayerPrefs.Save();
        }

        UpdateDisplay();
    }

    public void SetSkinID(int id)
    {
        skinID = id;
    }

    public void EquipInternal()
    {
        isEquip = true;

        skinMaterial = skin;
        storeItemsManager.ItemPrefabs.GetComponent<Renderer>().material = skinMaterial;

        PlayerPrefs.SetInt("LastEquippedSkinID", skinID);
        PlayerPrefs.SetInt("SkinEquip" + skinID.ToString(), 1);
        PlayerPrefs.Save();

        UpdateDisplay();
        Debug.Log(itemName.text + " 장착 완료");
    }

    public void UnequipInternal()
    {
        if (isEquip)
        {
            isEquip = false;
            equipImage.SetActive(false);
            Debug.Log(itemName.text + " 장착 해제");

            PlayerPrefs.SetInt("SkinEquip" + skinID.ToString(), 0);
            PlayerPrefs.Save();
        }
    }

    public bool IsSkinPurchased()
    {
        return PlayerPrefs.GetInt("Skin" + skinID.ToString(), 0) == 1;
    }

    public void UpdateDisplay()
    {
        itemImage.color = isPurchased ? Color.white : Color.black;
        equipImage.SetActive(isEquip);
    }

    private bool isDefaultSkin()
    {
        return skinID == 0;
    }

    private void SkinBuy()
    {
        if (UIManager.money >= price)
        {
            UIManager.money -= price;
            Crypto.SaveEncryptedData("Money", UIManager.money.ToString());

            isPurchased = true;
            PlayerPrefs.SetInt("Skin" + skinID.ToString(), 1);
            itemImage.color = Color.white;
            PlayGamesPlatform.Instance.UnlockAchievement(GPGSIds.achievement_fashionista, (bool success) => { });

            storeItemsManager.EquipSkin(this);
        }
        else
        {
            Debug.Log("돈이 부족합니다.");
        }
    }

    public void Purchased()
    {
        if (isPurchased)
        {
            storeItemsManager.EquipSkin(this);
        }
        else
        {
            Debug.Log("구매되어 있지 않습니다. 구매를 진행합니다.");
            SkinBuy();
        }
    }

    public void SetStoreItemsManager(StoreItemsManager manager)
    {
        storeItemsManager = manager;
    }
}
