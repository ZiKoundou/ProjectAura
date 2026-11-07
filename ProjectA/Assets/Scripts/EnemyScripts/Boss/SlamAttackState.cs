// SlamAttackState.cs
using UnityEngine;

public class SlamAttackState : BossState
{
    private float windupTime = 0.7f;
    private float timer;
    private bool hasSlammed;

    public SlamAttackState(BossController boss) : base(boss) { }

    public override void Enter()
    {
        timer = windupTime;
        hasSlammed = false;

        // Optional: play windup animation
        Debug.Log("Boss Slam Windup");
    }

    public override void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f && !hasSlammed)
        {
            hasSlammed = true;
            PerformSlam();
        }

        // After slam, wait a bit then go back to idle
        if (hasSlammed && timer <= -0.4f)
        {
            boss.ChangeState(boss.idleState);
        }
    }

    private void PerformSlam()
    {
        Debug.Log("Boss SLAM!");
        // TODO: Spawn AOE hitbox, play impact VFX/sound, camera shake, etc.
    }

    public override void Exit() { }
}