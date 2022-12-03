using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    public InventoryItemData itemData;
    [SerializeField] private ParticleSystem m_SparkleParticle;

    private void Reset()
    {
        m_SparkleParticle = GetComponentInChildren<ParticleSystem>();
    }

    public void PlaySparkleParticle()
    {
        m_SparkleParticle.Play();
    }
}
