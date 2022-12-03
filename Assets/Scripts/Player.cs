using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float m_MovementSpeed = 3f;
    [SerializeField] private float m_RotationSpeed = 5f;
    [SerializeField] private Rigidbody m_Rbody;
    [SerializeField] private Collider[] m_Colliders;
    [SerializeField] private Collider m_GroundCollider;
    [SerializeField] private bool m_Rotates = true;
    private Camera m_Camera;
    private void Reset()
    {
        m_Colliders = GetComponentsInChildren<Collider>();
        m_Rbody = GetComponentInChildren<Rigidbody>();
    }
    private void Start()
    {
        m_Camera = Camera.main;

        for (int i = 0; i < m_Colliders.Length; i++)
        {
            for (int j = 0; j < m_Colliders.Length; j++)
            {
                if (i != j)
                {
                    Physics.IgnoreCollision(m_Colliders[i], m_Colliders[j]);
                }
            }
            Physics.IgnoreCollision(m_Colliders[i], m_GroundCollider);
        }
    }

    private void FixedUpdate()
    {
        if (GameManager.Instance.CurrentGameState != GameState.Play)
        {
            return;
        }

        Vector3 direction = Vector3.zero;
#if UNITY_EDITOR

        if (Input.GetMouseButton(0))
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 10;
            Vector3 targetPos = m_Camera.ScreenToWorldPoint(mousePos);
            direction = (targetPos - transform.position).normalized;
            direction.y = 0;

            if(m_Rotates)
                LookAt(direction);
        }

#else

        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector3 touchPos = touch.position;
            Vector3 targetPos = m_Camera.ScreenToWorldPoint(touchPos);
            direction = (targetPos - transform.position).normalized;
            direction.y = 0;

            if(m_Rotates)
                LookAt(direction);
        }
#endif

        Move(direction);
    }

    private void Move(Vector3 direction)
    {
        m_Rbody.velocity = m_MovementSpeed * Time.deltaTime * direction;
    }

    private void LookAt(Vector3 direction)
    {
        Quaternion rotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * m_RotationSpeed);
    }

    public void IgnoreCollision(Collider other)
    {
        for (int i = 0; i < m_Colliders.Length; i++)
        {
            Physics.IgnoreCollision(m_Colliders[i], other);
        }
    }
}
