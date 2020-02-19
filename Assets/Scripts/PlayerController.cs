using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int m_PlayerNumber = 1;
    public float m_Speed = 12f;
    private Rigidbody m_Rigidbody;

    private string m_VertMovementAxisName;
    private string m_HorzMovementAxisName;
    private float m_VertMovementInputValue;
    private float m_HorzMovementInputValue;

    private string m_VertRotationAxisName;
    private string m_HorzRotationAxisName;
    private float m_VertRotationInputValue;
    private float m_HorzRotationInputValue;

    private void Awake()
    {
        m_Rigidbody = GetComponent<Rigidbody>();
    }


    private void OnEnable()
    {
        m_Rigidbody.isKinematic = false;
        m_VertMovementInputValue = 0f;
        m_HorzMovementInputValue = 0f;
        m_VertRotationInputValue = 0f;
        m_HorzRotationInputValue = 0f;
    }


    private void OnDisable()
    {
        m_Rigidbody.isKinematic = true;
    }


    private void Start()
    {
        m_VertMovementAxisName = "Vertical" + m_PlayerNumber;
        m_HorzMovementAxisName = "Horizontal" + m_PlayerNumber;
        m_VertRotationAxisName = "VerticalRotation" + m_PlayerNumber;
        m_HorzRotationAxisName = "HorizontalRotation" + m_PlayerNumber;
    }

    private void Update()
    {
        // Store the value of both input axes.
        m_VertMovementInputValue = Input.GetAxis(m_VertMovementAxisName);
        m_HorzMovementInputValue = Input.GetAxis(m_HorzMovementAxisName);
        m_VertRotationInputValue = Input.GetAxis(m_VertRotationAxisName);
        m_HorzRotationInputValue = Input.GetAxis(m_HorzRotationAxisName);
    }

    private void FixedUpdate()
    {
        Move();
        Turn();
    }

    private void Move()
    {
        // Create a vector in the direction the tank is facing with a magnitude based on the input, speed and the time between frames.
        Vector3 direction = (Vector3.forward * m_VertMovementInputValue + Vector3.right * m_HorzMovementInputValue);

        // Apply this movement to the rigidbody's position.
        m_Rigidbody.MovePosition(m_Rigidbody.position + Time.deltaTime * m_Speed * direction);
    }

    private void Turn()
    {
        if (Math.Abs(m_VertRotationInputValue) > 0.2 || Math.Abs(m_HorzRotationInputValue) > 0.2)
        {
            Vector3 direction = (Vector3.left * m_VertRotationInputValue + Vector3.forward * m_HorzRotationInputValue);

            transform.rotation = Quaternion.LookRotation(direction);
        }
        else if (Math.Abs(m_VertMovementInputValue) > 0.2 || Math.Abs(m_HorzMovementInputValue) > 0.2)
        {
            Vector3 direction = (Vector3.left * m_VertMovementInputValue + Vector3.forward * m_HorzMovementInputValue);

            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}