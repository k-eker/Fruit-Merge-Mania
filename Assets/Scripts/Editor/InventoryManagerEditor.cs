using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InventoryManager))]
public class InventoryManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        InventoryManager inventoryManager = (InventoryManager)target;
        if (GUILayout.Button("Reset Inventory"))
        {
            inventoryManager.ResetInventory();
        }
    }

}
