using UnityEngine;

public class CanvasRotationPreventer : MonoBehaviour
{
    private Quaternion m_eRotation; 

    private void Start()
    {
        m_eRotation = transform.rotation;
    }


    private void Update()
    {
            transform.rotation = m_eRotation;
    }
}