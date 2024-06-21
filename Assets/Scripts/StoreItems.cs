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

    private bool isEquip = false; // 장착 여부

    public void Purchased()
    {
        if (!isEquip)
        {
            isEquip = true;
            Debug.Log("장착 O ");
            equipImage.SetActive(true);
        }
        else
        {
            isEquip = false;
            Debug.Log("장착 X ");
            equipImage.SetActive(false);
        }
    }
}
