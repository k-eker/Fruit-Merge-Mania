using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public InventoryItem[] inventoryItemPrefabs;
    Vector3 m_PlayerStartPos;

    private void Start()
    {
        m_PlayerStartPos = GameManager.Instance.player.transform.position;

        InitializeInventory();
    }

    private void InitializeInventory()
    {
        for (int i = 0; i < inventoryItemPrefabs.Length; i++)
        {
            if (inventoryItemPrefabs[i].itemData.Amount > 0)
            {
                GameManager.Instance.uiController.UpdateItemUI(inventoryItemPrefabs[i].itemData);
            }
        }
    }

    public void SpawnRandomInventoryItem()
    {
        int rand = Random.Range(0, inventoryItemPrefabs.Length);
        InventoryItem item = Instantiate(inventoryItemPrefabs[rand], m_PlayerStartPos, Quaternion.identity);

        inventoryItemPrefabs[rand].itemData.Amount++;


        float animationDuration = 3f;
        float offsetZ = 5f;
        Vector3 toPosition = new Vector3(m_PlayerStartPos.x, m_PlayerStartPos.y, m_PlayerStartPos.z + offsetZ);
        item.transform.DOJump(toPosition, 5f, 3, animationDuration).onComplete = () =>
        {
            Vector3 endPosition = Vector3.Lerp(Camera.main.transform.position, toPosition, 0.3f);
            item.transform.DOMove(endPosition, animationDuration);

            item.PlaySparkleParticle();
        };
    }

    public void ResetInventory()
    {
        for (int i = 0; i < inventoryItemPrefabs.Length; i++)
        {
            inventoryItemPrefabs[i].itemData.Amount = 0;
        }
    }
}
