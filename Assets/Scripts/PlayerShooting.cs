using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public int m_PlayerNumber = 1; // Used to identify the different players.
    public PistolManager m_PistolManager;

    private bool alreadyShoot = false;
    private string m_FireButton; // The input axis that is used for launching shells.


    private void Start()
    {
        m_FireButton = "Fire" + m_PlayerNumber;
    }

    private void Update()
    {
        if (Input.GetAxis(m_FireButton) > 0.5f)
        {
            if (!alreadyShoot)
            {
                m_PistolManager.Fire();
                alreadyShoot = true;
            }
            
        } else if (alreadyShoot)
        {
            alreadyShoot = false;
        }
        
    }
}