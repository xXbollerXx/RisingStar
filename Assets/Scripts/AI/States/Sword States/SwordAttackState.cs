using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttackState : AttackState
{

    const string AttackAnimName = "SwordEnemyAttack";

   

    public override void OnStateEnter(AIStateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);
        _stateMachine.Animator.Play(AttackAnimName);
    }

    public override void OnStateTick()
    {
        base.OnStateTick();
        if (_LockMovement) return;
        _stateMachine.Agent.SetDestination(_stateMachine.Player.position);
        _stateMachine.transform.LookAt(_stateMachine.Player);
    }
  
}
