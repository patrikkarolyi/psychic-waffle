using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    void OnTriggerEnter(Collider col)
    {
        
        Debug.Log(col.name);
        PlayerHealth ph = col.GetComponentInParent<PlayerHealth>();
        
        if (ph != null)
        {
            ph.TakeDamage(25);
        }
        Destroy(gameObject);
        
    }
}
