using UnityEngine;

public class ShieldHealth : Health
{
    public void TakeDamage(float damage)
    {
        Debug.Log("Abstract component was called : Health !");
    }
}