using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.ProBuilder;

public class BossController : MonoBehaviour
{
    [Header("References")]
    public Transform player;
    [SerializeField] private Transform firePoint;

    [Header("Fireball Settings")]
    // this would be modified by the actual attack state triggering it
    public GameObject fireballPrefab;
    public float fireballSpeed = 30f;
    public float fireballDamage = 20f;

    [Header("Config")]
    public float thinkTime = 1f;
    

    private BossState currentState;

    public IdleState idleState;
    public ChooseAttackState chooseAttackState;
    public DashAttackState dashAttackState;
    public SlamAttackState slamAttackState;
    public FireballAttackState fireballAttackState;

    void Awake()
    {
        idleState = new IdleState(this);
        chooseAttackState = new ChooseAttackState(this);
        dashAttackState = new DashAttackState(this);
        slamAttackState = new SlamAttackState(this);
        fireballAttackState = new FireballAttackState(this);
    }

    void Start()
    {
        ChangeState(idleState);
    }

    void Update()
    {
        currentState?.Update();
    }

    public void ChangeState(BossState newState)
    {
        currentState?.Exit();

        currentState = newState;

        currentState?.Enter();
    }

    public void ShootFireball()
    {
        if (fireballPrefab == null || firePoint == null)
        {
            Debug.LogWarning("Fireball Prefab or Fire Point not assigned!");
            return;
        }

        // Spawn the fireball
        GameObject fireball = Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);

        // Make it fly toward the player
        Vector3 direction = (player.position - firePoint.position).normalized;

        // Try to use a Projectile script if it exists
        Projectile proj = fireball.GetComponent<Projectile>();
        if (proj != null)
        {
            proj.Initialize(direction, fireballSpeed, fireballDamage);
        }
        else
        {
            // Fallback if you don't have a Projectile script yet
            Rigidbody rb = fireball.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = direction * fireballSpeed;
            }
        }
    }
}
