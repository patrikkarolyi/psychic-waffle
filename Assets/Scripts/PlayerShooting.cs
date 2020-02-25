using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int m_PlayerNumber = 1; // Used to identify the different players.
    public Rigidbody m_Bullet; // Prefab of the shell.
    public Transform m_FireTransform; // A child of the tank where the shells are spawned.

    private string m_FireButton; // The input axis that is used for launching shells.
    private float m_CurrentLaunchForce; // The force that will be given to the shell when the fire button is released.


    private void Start()
    {
        m_FireButton = "Fire" + m_PlayerNumber;
    }

    private void Update()
    {
        if (Input.GetButtonDown(m_FireButton))
        {
            Fire();
        }
    }


    private void Fire()
    {
        Rigidbody bulletInstance = Instantiate(m_Bullet, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;

        bulletInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward;
        ;
    }
}