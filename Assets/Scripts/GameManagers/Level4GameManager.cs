using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level4GameManager : GameManager
{
    [HideInInspector] public List<Brick> allBricks = new List<Brick>();

    private void Start()
    {
        InitializeBricks();
    }

    private void InitializeBricks()
    {
        GameObject[] spawnedObjects = objectSpawner.SpawnGridObjects();

        for (int i = 0; i < spawnedObjects.Length; i++)
        {
            allBricks.Add(spawnedObjects[i].GetComponent<Brick>());
        }
    }
}
