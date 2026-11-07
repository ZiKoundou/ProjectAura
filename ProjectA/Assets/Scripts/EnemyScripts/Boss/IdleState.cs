using UnityEngine;
using System.Collections;

public class IdleState : BossState
{
    public IdleState(BossController boss) : base(boss)
    {
    }

    public override void Enter()
    {
        boss.StartCoroutine(WaitAndChoose());
    }

    IEnumerator WaitAndChoose()
    {
        yield return new WaitForSeconds(boss.thinkTime);

        boss.ChangeState(boss.chooseAttackState);
    }
}