using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] private Image m_ProgressBarFillImage;
    [SerializeField] private InventoryItemUI m_InventoryItemUIPrefab;
    [SerializeField] private Transform m_InventoryRoot;
    [SerializeField] private Button m_StartButton;
    [SerializeField] private Button m_RestartButton;

    private List<InventoryItemUI> m_AllInventoryItemUIs = new List<InventoryItemUI>();
    public void SetProgressBar(float value, bool snap = false)
    {
        if (!snap)
        {
            float duration = 0.5f;
            m_ProgressBarFillImage.DOFillAmount(value, duration);
        }
        else
        {
            m_ProgressBarFillImage.fillAmount = value;
        }
    }

    public void AddStartButtonListener(UnityAction action)
    {
        m_StartButton.onClick.AddListener(action);
    }
    public void AddRestartButtonListener(UnityAction action)
    {
        m_RestartButton.onClick.AddListener(action);
    }

    public void ShowWinUI()
    {
    }


    #region Inventory UI
    public void UpdateItemUI(InventoryItemData data)
    {
        InventoryItemUI itemUi = GetItemUI(data);
        if (itemUi == null)
        {
            CreateInventoryItemUI(data);
        }
        else
        {
            if (data.Amount != 0)
            {
                itemUi.UpdateItemUI(data);
            }
            else
            {
                Destroy(itemUi.gameObject);
            }
        }
    }
    private void CreateInventoryItemUI(InventoryItemData data)
    {
        InventoryItemUI itemUi = Instantiate(m_InventoryItemUIPrefab, m_InventoryRoot);
        itemUi.UpdateItemUI(data);
        m_AllInventoryItemUIs.Add(itemUi);
    }

    private InventoryItemUI GetItemUI(InventoryItemData data)
    {
        for (int i = 0; i < m_AllInventoryItemUIs.Count; i++)
        {
            if (m_AllInventoryItemUIs[i].GetName() == data.itemName)
            {
                return m_AllInventoryItemUIs[i];
            }
        }
        return null;
    }
    #endregion
}
