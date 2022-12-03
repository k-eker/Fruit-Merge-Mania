using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_NameText;
    [SerializeField] private TextMeshProUGUI m_AmountText;
    [SerializeField] private RawImage m_ItemImage;

    
    public void UpdateItemUI(InventoryItemData data)
    {
        m_NameText.text = data.itemName;
        m_AmountText.text = data.Amount.ToString();
        m_ItemImage.texture = data.itemTexture;
    }

    public string GetName()
    {
        return m_NameText.text;
    }
}
