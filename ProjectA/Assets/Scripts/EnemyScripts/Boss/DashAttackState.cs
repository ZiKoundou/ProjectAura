// DashAttackState.cs
using UnityEngine;

public class DashAttackState : BossState
{
    private Vector3 dashDirection;
    private float dashSpeed = 50f;
    private float dashDuration = 0.4f;
    private float timer;

    public DashAttackState(BossController boss) : base(boss) { }

    public override void Enter()
    {
        timer = dashDuration;

        // Face and dash toward player
        Vector3 dir = (boss.player.position - boss.transform.position).normalized;
        dir.y = 0f;
        dashDirection = dir;

        // Optional: play dash animation / VFX / sound
        Debug.Log("Boss Dash Attack!");
    }

    public override void Update()
    {
        boss.transform.position += dashDirection * dashSpeed * Time.deltaTime;

        timer -= Time.deltaTime;
        if (timer <= 0f)
        {
            boss.ChangeState(boss.idleState); // or recovery state later
        }
    }

    public override void Exit() { }
}