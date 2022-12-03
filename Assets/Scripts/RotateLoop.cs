using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateLoop : MonoBehaviour
{
    [SerializeField] private float m_Duration = 3f;
    private void Start()
    {
        transform.DORotate(new Vector3(0, 360, 0), m_Duration, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Incremental).SetEase(Ease.Linear);
    }
}
