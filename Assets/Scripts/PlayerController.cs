using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int m_PlayerNumber = 1;
    public float m_Speed = 12f;

    private string m_VertMovementAxisName;
    private string m_HorzMovementAxisName;
    private Rigidbody m_Rigidbody;
    private float m_VertMovementInputValue;
    private float m_HorzMovementInputValue;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }


    private void OnEnable()
    {
        m_Rigidbody.isKinematic = false;
        m_VertMovementInputValue = 0f;
        m_HorzMovementInputValue = 0f;
    }


    private void OnDisable()
    {
        m_Rigidbody.isKinematic = true;
    }


    private void Start()
    {
        m_VertMovementAxisName = "Vertical" + m_PlayerNumber;
        m_HorzMovementAxisName = "Horizontal" + m_PlayerNumber;
    }

    private void Update()
    {
        // Store the value of both input axes.
        m_VertMovementInputValue = Input.GetAxis(m_VertMovementAxisName);
        m_HorzMovementInputValue = Input.GetAxis(m_HorzMovementAxisName);
    }

    private void FixedUpdate()
    {
        Move();
    }


    private void Move()
    {
        // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
        var transform1 = transform;
        Vector3 direction = Time.deltaTime  * m_Speed * (transform1.forward * m_VertMovementInputValue +  m_HorzMovementInputValue * transform1.right);

        // Apply this movement to the rigidbody's position.
        m_Rigidbody.MovePosition(m_Rigidbody.position + direction);
    }

}