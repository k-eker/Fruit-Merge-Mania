using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1GameManager : GameManager
{
    [HideInInspector] public List<Collectable> allCollectables = new List<Collectable>();

    private void Start()
    {
        InitializeCollectables();
    }

    private void InitializeCollectables()
    {
        GameObject[] spawnedObjects = objectSpawner.SpawnRandomlyPlacedObjects();

        for (int i = 0; i < spawnedObjects.Length; i++)
        {
            allCollectables.Add(spawnedObjects[i].GetComponent<Collectable>());
        }
    }
}
