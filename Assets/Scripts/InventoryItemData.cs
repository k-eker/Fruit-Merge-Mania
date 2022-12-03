using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory Item Data", menuName = "ALICTUS/Inventory Item Data", order = 0)]
public class InventoryItemData : ScriptableObject
{
    public string itemName;
    public RenderTexture itemTexture;
    [SerializeField] private int m_Amount;
    public int Amount {
        get
        {
            return m_Amount;
        }
        set
        {
            m_Amount = value;

            GameManager.Instance.uiController.UpdateItemUI(this);
        }
    }
}
