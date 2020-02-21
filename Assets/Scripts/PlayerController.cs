using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int m_PlayerNumber = 1;
    public float m_Speed = 12f;
    public Animator animator;
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
        SetAnimator();
    }

    private void Move()
    {

        movementDirection = (Vector3.forward * m_VertMovementInputValue + Vector3.right * m_HorzMovementInputValue);

        m_Rigidbody.MovePosition(m_Rigidbody.position + Time.deltaTime * m_Speed * movementDirection);

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

        transform.rotation = Quaternion.LookRotation(rotationDirection);
    }

    private void SetAnimator()
    {
        animator.SetFloat("speed", animatorSpeed);

        if (animatorAmingPrev != animatorAming)
        {
            animator.SetBool("isAming", animatorAming);
            animatorAmingPrev = animatorAming;
        }
        
        if (animatorAming)
        {
            Vector3 relativeDirection = Quaternion.AngleAxis(90, Vector3.up) * rotationDirection;

            float x = -Vector3.Dot(movementDirection.normalized, rotationDirection.normalized);
            float z = Vector3.Dot(movementDirection.normalized, relativeDirection.normalized);

            animator.SetFloat("amingMovingForward",  Math.Sign(x));
            animator.SetFloat("amingMovingRight", Math.Sign(z));
        }
    }
}