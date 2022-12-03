using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
    public int spawnAmount;
    [SerializeField] private float m_OffsetY = 2.6f;
    [SerializeField] private float m_Distance;
    [SerializeField] private int m_Width;
    [SerializeField] private int m_Height;

    [SerializeField] private GameObject[] m_ObjectPrefabs;


    public GameObject[] SpawnRandomlyPlacedObjects()
    {
        if (spawnAmount > m_Width * m_Height)
        {
            throw new InvalidOperationException("Spawn amount can't be greater than the map resolution.");
        }

        List<Vector3> mapBounds = new List<Vector3>();
        GameObject[] spawnedObjects = new GameObject[spawnAmount];
        for (int y = 0; y < m_Height; y++) 
        {
            for (int x = 0; x < m_Width; x++)
            {
                float offsetX = -(m_Width / 2);
                Vector3 pos = new Vector3((x + offsetX) * m_Distance, 0, (y + m_OffsetY) * m_Distance);
                mapBounds.Add(pos);
            }
        }
        for (int i = 0; i < spawnAmount; i++)
        {
            int rand = UnityEngine.Random.Range(0, mapBounds.Count);
            Vector3 pos = mapBounds[rand];
            mapBounds.RemoveAt(rand);
            Quaternion rot = Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);
            GameObject go = Instantiate(m_ObjectPrefabs[UnityEngine.Random.Range(0, m_ObjectPrefabs.Length)], pos, rot);
            go.transform.parent = this.transform;
            spawnedObjects[i] = go;
        }
        return spawnedObjects;
    }

    public GameObject[] SpawnGridObjects()
    {
        if (spawnAmount > m_Width * m_Height)
        {
            throw new InvalidOperationException("Spawn amount can't be greater than the map resolution.");
        }

        GameObject[] spawnedObjects = new GameObject[spawnAmount];

        int i = 0;
        for (int y = 0; y < m_Height; y++)
        {
            for (int x = 0; x < m_Width; x++)

            {
                float offsetX = -(m_Width / 2);
                Vector3 pos = new Vector3(-(x + offsetX) * m_Distance, 0, -(y + m_OffsetY) * m_Distance);
                GameObject go = Instantiate(m_ObjectPrefabs[UnityEngine.Random.Range(0, m_ObjectPrefabs.Length)], pos, Quaternion.identity);
                go.transform.parent = this.transform;
                spawnedObjects[i] = go;
                i++;

                if (i == spawnAmount)
                {
                    return spawnedObjects;
                }
            }
        }
        return spawnedObjects;
    }

}
