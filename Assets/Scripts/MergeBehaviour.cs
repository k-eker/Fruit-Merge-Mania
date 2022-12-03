using DG.Tweening;
using DG.Tweening.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject[] m_Models;
    [SerializeField] private Rigidbody m_Rbody;

    public int Power { get; private set; }

    private const float MERGE_DURATION = 0.5f;
    public bool CanMerge
    {
        get
        {
            return m_Models[Power].GetComponent<Collider>().enabled;
        }
        set
        {
            m_Models[Power].GetComponent<Collider>().enabled = value;
        }
    }

    private void Start()
    {
        m_Models[Power].SetActive(true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag(this.gameObject.tag))
        {
            TryMerge(collision.collider.transform.parent.GetComponent<MergeBehaviour>());
        }
        else if (collision.collider.CompareTag("Player"))
        {
            if (Power == m_Models.Length - 2)
            {
                Power++;

                Vector3 midPoint = GetMidPoint(collision.collider.transform.position, transform.position);
                midPoint.y = 0;
                MergeBehaviour otherMergeBehaviour = collision.collider.transform.parent.parent.GetComponent<MergeBehaviour>();

                OnMerge(otherMergeBehaviour);

                otherMergeBehaviour.AnimateMerge(otherMergeBehaviour.Power, midPoint, MERGE_DURATION);
                AnimateMerge(Power - 1, midPoint, MERGE_DURATION).onComplete = () =>
                {
                    OnMergeAnimationComplete(otherMergeBehaviour);
                    GameManager.Instance.CurrentGameState = GameState.Win;
                };
            }
        }
    }

    private void TryMerge(MergeBehaviour other)
    {
        if (other.Power == Power && CanMerge)
        {
            other.CanMerge = false;
            CanMerge = false;

            OnMerge(other);

            Vector3 midPoint = GetMidPoint(other.transform.position, transform.position);
            midPoint.y = 0;
            Power++;
            other.AnimateMerge(other.Power, midPoint, MERGE_DURATION);
            AnimateMerge(Power - 1, midPoint, MERGE_DURATION).onComplete = () =>
            {
                OnMergeAnimationComplete(other);
            };

        }
    }

    private void OnMerge(MergeBehaviour other)
    {
        Level2GameManager level2Manager = GameManager.Instance as Level2GameManager;
        level2Manager.allMergeBehaviours.Remove(other);

        level2Manager.UpdateProgress();
    }

    private void OnMergeAnimationComplete(MergeBehaviour other)
    {
        Destroy(other.gameObject);


        GameObject prevModel = m_Models[Power - 1];
        GameObject newModel = m_Models[Power];

        prevModel.SetActive(false);
        newModel.SetActive(true);

        Vector3 prevScale = newModel.transform.localScale;
        newModel.transform.localScale = Vector3.zero;
        newModel.transform.DOScale(prevScale, MERGE_DURATION);
        m_Rbody.isKinematic = false;
        CanMerge = true;
    }

    public TweenerCore<Vector3, Vector3, DG.Tweening.Plugins.Options.VectorOptions> AnimateMerge(int power, Vector3 midPoint, float duration)
    {
        m_Rbody.isKinematic = true;
        m_Models[power].transform.DOScale(Vector3.zero, duration);
        return m_Models[power].transform.DOMove(midPoint, duration);
    }

    private Vector3 GetMidPoint(Vector3 A, Vector3 B)
    {
        return (A + B) / 2;
    }
}
