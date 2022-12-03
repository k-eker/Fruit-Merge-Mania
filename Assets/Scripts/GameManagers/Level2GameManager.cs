using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level2GameManager : GameManager
{
    [HideInInspector] public List<MergeBehaviour> allMergeBehaviours = new List<MergeBehaviour>();
    private void Start()
    {
        InitializeCollectables();
    }


    private void InitializeCollectables()
    {
        GameObject[] spawnedObjects = objectSpawner.SpawnRandomlyPlacedObjects();

        for (int i = 0; i < spawnedObjects.Length; i++)
        {
            allMergeBehaviours.Add(spawnedObjects[i].GetComponent<MergeBehaviour>());
        }
    }

    public void UpdateProgress()
    {
        int spawnAmount = objectSpawner.spawnAmount;
        float currentProgress = spawnAmount - allMergeBehaviours.Count + 1;
        uiController.SetProgressBar(currentProgress / (float)spawnAmount);
    }
}
