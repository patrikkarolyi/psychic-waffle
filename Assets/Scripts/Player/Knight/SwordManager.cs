using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordManager : MonoBehaviour
{
    public Transform m_FirePoint;
    public Animator m_Animator;
    public float attackRange = 0.5f;
    public LayerMask m_LayerMasks;
    public float damage = 50f;
    public float damageDelay = 0.5f;
    private List<PlayerHealth> hitPlayerHealth;

    //public ParticleSystem m_ExplosionParticles;         // Reference to the particles that will play on explosion.
    //public AudioSource m_ExplosionAudio;                // Reference to the audio that will play on explosion.

    private static readonly int attack = Animator.StringToHash("attack");

    private PlayerHealth self;

    private void Start()
    {
        self = GetComponentInParent<PlayerHealth>();
        hitPlayerHealth = new List<PlayerHealth>();
    }

    public void Fire()
    {
        m_Animator.SetTrigger(attack);
        StartCoroutine(HitAfterTime(damageDelay));
    }

    IEnumerator HitAfterTime(float time)
    {
        yield return new WaitForSeconds(time);

        // Code to execute after the delay
        // Collect all the colliders in a sphere from the shell's current position to a radius of the explosion radius.
        Collider[] colliders = Physics.OverlapSphere(m_FirePoint.position, attackRange, m_LayerMasks);

        // Go through all the colliders...
        foreach (Collider collider in colliders)
        {
            // Find the TankHealth script associated with the rigidbody.
            PlayerHealth targetHealth = collider.GetComponentInParent<PlayerHealth>();

            // If there is no TankHealth script attached to the gameobject, go on to the next collider.
            if (!targetHealth)
            {
                continue;
            }

            if (targetHealth.Equals(self))
            {
                continue;
            }

            if (hitPlayerHealth.Contains(targetHealth))
            {
                continue;
            }

            // Deal this damage to the tank.
            targetHealth.TakeDamage(damage);
            hitPlayerHealth.Add(targetHealth);
        }

        hitPlayerHealth.Clear();
    }
}
