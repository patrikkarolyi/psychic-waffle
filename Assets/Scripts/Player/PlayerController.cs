using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int m_PlayerNumber = 1;
    public float m_Speed = 12f;
    private Animator m_Animator;
    private Rigidbody m_Rigidbody;

    private string m_VertMovementAxisName;
    private string m_HorzMovementAxisName;
    private float m_VertMovementInputValue;
    private float m_HorzMovementInputValue;

    private string m_VertRotationAxisName;
    private string m_HorzRotationAxisName;
    private float m_VertRotationInputValue;
    private float m_HorzRotationInputValue;

    private Vector3 movementDirection;
    private Vector3 rotationDirection;

    private float animatorSpeed = 0;
    private bool animatorAmingPrev = false;
    private bool animatorAming = false;

    private void Awake()
    {
        m_Animator = GetComponentInChildren<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
    }


    private void OnEnable()
    {
        m_Animator.enabled = true;
        m_Rigidbody.isKinematic = false;
        m_VertMovementInputValue = 0f;
        m_HorzMovementInputValue = 0f;
        m_VertRotationInputValue = 0f;
        m_HorzRotationInputValue = 0f;
    }


    private void OnDisable()
    {
        m_Animator.enabled = false;
        //m_Rigidbody.isKinematic = true; when remains a corpse
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
        SetAnimator();
    }

    private void Move()
    {
        movementDirection = (Vector3.forward * m_VertMovementInputValue + Vector3.right * m_HorzMovementInputValue);

        float mod = 1f;

        if (animatorAming) mod = 0.5f;

        m_Rigidbody.MovePosition(m_Rigidbody.position + Time.deltaTime * m_Speed * mod * movementDirection);

        animatorSpeed = movementDirection.sqrMagnitude;
    }

    private void Turn()
    {
        animatorAming = false;

        if (Math.Abs(m_VertRotationInputValue) > 0.2 || Math.Abs(m_HorzRotationInputValue) > 0.2)
        {
            rotationDirection = (Vector3.left * m_VertRotationInputValue + Vector3.forward * m_HorzRotationInputValue);

            animatorAming = true;
        }
        else if (Math.Abs(m_VertMovementInputValue) > 0.2 || Math.Abs(m_HorzMovementInputValue) > 0.2)
        {
            rotationDirection = (Vector3.left * m_VertMovementInputValue + Vector3.forward * m_HorzMovementInputValue);
        }

        if (rotationDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(rotationDirection);
        }
    }

    private void SetAnimator()
    {
        m_Animator.SetFloat("speed", animatorSpeed);

        if (animatorAmingPrev != animatorAming)
        {
            m_Animator.SetBool("isAming", animatorAming);
            animatorAmingPrev = animatorAming;
        }

        if (animatorAming)
        {
            Vector3 relativeDirection = Quaternion.AngleAxis(90, Vector3.up) * rotationDirection;

            float x = -Vector3.Dot(movementDirection.normalized, rotationDirection.normalized);
            float z = Vector3.Dot(movementDirection.normalized, relativeDirection.normalized);

            if (Math.Abs(z) < 0.1) z = 0;

            m_Animator.SetFloat("amingMovingForward", x);
            m_Animator.SetFloat("amingMovingRight", z);
        }
    }
}