using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionZone : MonoBehaviour
{
    [SerializeField] private float m_PullStrength = 3f;
    private List<Collectable> m_CollectedCollectables = new List<Collectable>();
    private Level1GameManager m_Level1GameManager;

    private void Start()
    {
        m_Level1GameManager = GameManager.Instance as Level1GameManager;
        if (m_Level1GameManager == null)
        {
            Debug.LogError("Incorrect level");
        }
    }
    private void FixedUpdate()
    {
        if (GameManager.Instance.CurrentGameState != GameState.Play)
        {
            return;
        }

        for (int i = 0; i < m_CollectedCollectables.Count; i++)
        {
            Vector3 direction = (transform.position - m_CollectedCollectables[i].transform.position).normalized;
            m_CollectedCollectables[i].rbody.velocity = direction * m_PullStrength;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (GameManager.Instance.CurrentGameState != GameState.Play)
        {
            return;
        }

        if (other.CompareTag("Collectable"))
        {
            Collectable collectable = other.GetComponent<Collectable>();
            GameManager.Instance.player.IgnoreCollision(other);
            OnCollectableCollected(collectable);
        }
    }

    private void OnCollectableCollected(Collectable collectable)
    {
        m_CollectedCollectables.Add(collectable);
        m_Level1GameManager.allCollectables.Remove(collectable);
        int spawnAmount = GameManager.Instance.objectSpawner.spawnAmount;
        GameManager.Instance.uiController.SetProgressBar((float)(spawnAmount - m_Level1GameManager.allCollectables.Count) / (float)spawnAmount);

        if (m_Level1GameManager.allCollectables.Count == 0)
        {
            GameManager.Instance.CurrentGameState = GameState.Win;
            ExplodeCollectionZone(150f, 150f);
        }
    }

    private void ExplodeCollectionZone(float force, float radius)
    {
        for (int i = 0; i < m_CollectedCollectables.Count; i++)
        {
            m_CollectedCollectables[i].rbody.useGravity = false;
            m_CollectedCollectables[i].rbody.AddExplosionForce(force, transform.position, radius);
        }

        Camera.main.transform.DOShakePosition(3f, 0.5f, 5);
    }
}
