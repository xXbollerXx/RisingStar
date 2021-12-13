using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : AIState
{
    protected bool _isAttackComplete = false;
    protected bool _LockMovement = false;


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

    public virtual void OnWindUpComplete()
    {
        _LockMovement = true;
        _stateMachine.Agent.isStopped = true;
    }

    public virtual void OnAttackComplete()
    {
        _isAttackComplete = true;
        _LockMovement = false;
        _stateMachine.Agent.isStopped = false;
    }
}
