using UnityEngine;

public class ChooseAttackState : BossState
{
    public ChooseAttackState(BossController boss) : base(boss)
    {
    }

    public override void Enter()
    {
        int roll = Random.Range(0, 3);

        switch (roll)
        {
            case 0:
                boss.ChangeState(boss.dashAttackState);
                break;

            case 1:
                boss.ChangeState(boss.slamAttackState);
                break;

            case 2:
                boss.ChangeState(boss.fireballAttackState);
                break;
        }
    }
}