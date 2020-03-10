using System;
using UnityEngine;

public class PlayerSlashing : MonoBehaviour
{
    private int m_PlayerNumber = 1; // Used to identify the different players.
    private string m_FireButton; // The input axis that is used for launching shells.
    private PlayerKnightController m_PlayerController;
    private SwordManager m_SwordManager;

    private bool alreadyShoot = false;
    private bool shoot = false;
    private float shootTimer = 0f;


    private void Start()
    {
        m_PlayerController = GetComponentInChildren<PlayerKnightController>();
        m_SwordManager = GetComponentInChildren<SwordManager>();
        
        m_PlayerNumber = m_PlayerController.m_PlayerNumber;
        m_FireButton = "Fire" + m_PlayerNumber;
    }

    private void Update()
    {
        if (Input.GetAxis(m_FireButton) > 0.9f)
        {
            if (!alreadyShoot)
            {
                shoot = true;
                alreadyShoot = true;
            }
            
        } else if (alreadyShoot)
        {
            alreadyShoot = false;
        }
        
    }

    private void FixedUpdate()
    {
        if (shootTimer < 1.15)
        {
            shootTimer += Time.fixedDeltaTime;
        } else if (shoot)
        {
            m_SwordManager.Fire();
            shootTimer = 0f;
            shoot = false;
        }
    }
}