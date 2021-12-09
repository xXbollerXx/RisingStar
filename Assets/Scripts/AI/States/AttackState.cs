using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : AIState
{
    protected bool _isAttackComplete = false;

    public AttackState(AIStateMachine stateMachine) : base(stateMachine)
    {

    }
    public override void OnStateExit()
    {
        _isAttackComplete = false;
    }

    public override void OnStateTick()
    {
        Debug.Log("attacking!!!");

    }

    public override void OnStateTransistionCheck()
    {
        if (_isAttackComplete)
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);

        }
    }
}
