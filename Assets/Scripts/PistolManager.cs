using UnityEngine;

public class PistolManager : MonoBehaviour
{
    public Rigidbody m_Bullet; // Prefab of the shell.
    public Transform m_FireTransform; // A child of the tank where the shells are 
    public float m_CurrentLaunchForce; // The force that will be given to the shell when the fire button is released.

    public void Fire()
    {
        Rigidbody bulletInstance = Instantiate(m_Bullet, m_FireTransform.position, m_FireTransform.rotation) as Rigidbody;
        bulletInstance.velocity = m_CurrentLaunchForce * m_FireTransform.forward;
        Destroy(bulletInstance.gameObject,4);
    }
}