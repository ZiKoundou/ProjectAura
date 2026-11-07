// FireballAttackState.cs
using UnityEngine;
using UnityEngine.UIElements;

public class FireballAttackState : BossState
{
    private float windupTime = 0.5f;
    private float timer;
    private bool hasFired;

    public FireballAttackState(BossController boss) : base(boss) { }

    public override void Enter()
    {
        timer = windupTime;
        hasFired = false;

        // Optional: play cast animation
        Debug.Log("Boss Fireball Windup");
    }

    public override void Update()
    {
        // Keep looking at player while casting
        Vector3 lookDir = boss.player.position - boss.transform.position;
        lookDir.y = 0f;
        if (lookDir != Vector3.zero)
            boss.transform.rotation = Quaternion.LookRotation(lookDir);

        timer -= Time.deltaTime;

        if (timer <= 0f && !hasFired)
        {
            hasFired = true;
            boss.ShootFireball();
        }

        if (hasFired && timer <= -0.3f)
        {
            boss.ChangeState(boss.idleState);
        }
    }

    public override void Exit() { }
}