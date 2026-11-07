using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Settings")]
    public float lifetime = 5f;
    public float hitRadius = 0.4f;          // For spherecast / overlap
    public LayerMask hitLayers;             // Set to Player + Environment
    public GameObject impactEffect;         // Optional VFX prefab

    private Vector3 direction;
    private float speed;
    private float damage;
    private bool hasHit = false;

    // Called by BossController
    public void Initialize(Vector3 dir, float spd, float dmg)
    {
        direction = dir.normalized;
        speed = spd;
        damage = dmg;

        // Optional: make the projectile face the direction it's flying
        if (direction != Vector3.zero)
            transform.rotation = Quaternion.LookRotation(direction);

        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        if (hasHit) return;

        // Move
        transform.position += direction * speed * Time.deltaTime;

        // Simple hit detection
        Collider[] hits = Physics.OverlapSphere(transform.position, hitRadius, hitLayers);

        foreach (var hit in hits)
        {
            // Example: damage player
            // You can replace this with your own health system
            if (hit.CompareTag("Player"))
            {
                // hit.GetComponent<Health>()?.TakeDamage(damage);
                Debug.Log($"Fireball hit player for {damage} damage!");
            }

            // Hit something → explode
            Impact();
            break;
        }
    }

    void Impact()
    {
        if (hasHit) return;
        hasHit = true;

        if (impactEffect != null)
        {
            Instantiate(impactEffect, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }

    // Optional: visualize the hit radius in editor
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, hitRadius);
    }
}