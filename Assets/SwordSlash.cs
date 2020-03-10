using UnityEngine;

public class SwordSlash : MonoBehaviour
{
    public LayerMask m_TankMask; // Used to filter what the explosion affects, this should be set to "Players".
    //public ParticleSystem m_ExplosionParticles;         // Reference to the particles that will play on explosion.
    //public AudioSource m_ExplosionAudio;                // Reference to the audio that will play on explosion.
    public float damage = 50f; // The amount of damage done if the explosion is centred on a tank.
    public float m_MaxLifeTime = 1f; // The time in seconds before the shell is removed.
    public float m_ExplosionRadius = 5f;


    private void Start()
    {
        // If it isn't destroyed by then, destroy the shell after it's lifetime.
        Destroy(gameObject, m_MaxLifeTime);
    }


    private void OnTriggerEnter(Collider other)
    {
        // Collect all the colliders in a sphere from the shell's current position to a radius of the explosion radius.
        Collider[] colliders = Physics.OverlapSphere(transform.position, m_ExplosionRadius, m_TankMask);

        // Go through all the colliders...
        for (int i = 0; i < colliders.Length; i++)
        {
            // ... and find their rigidbody.
            Rigidbody targetRigidbody = colliders[i].GetComponent<Rigidbody>();

            // If they don't have a rigidbody, go on to the next collider.
            if (!targetRigidbody)
                continue;


            // Find the TankHealth script associated with the rigidbody.
            PlayerHealth targetHealth = targetRigidbody.GetComponent<PlayerHealth>();

            // If there is no TankHealth script attached to the gameobject, go on to the next collider.
            if (!targetHealth)
                continue;

            // Deal this damage to the tank.
            targetHealth.TakeDamage(damage);
        }

        // Play the particle system.
        //m_ExplosionParticles.Play();

        // Play the explosion sound effect.
        //m_ExplosionAudio.Play();

        // Once the particles have finished, destroy the gameobject they are on.
        //Destroy (m_ExplosionParticles.gameObject, m_ExplosionParticles.duration);

        // Destroy the shell.
        Destroy(gameObject);
    }
}