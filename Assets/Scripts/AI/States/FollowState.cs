using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowState : AIState
{

  

    public override void OnStateExit()
    {


    }

    public override void OnStateTick()
    {
        _stateMachine.Agent.SetDestination(_stateMachine.Player.position);
        _stateMachine.transform.LookAt(_stateMachine.Player);
        Debug.Log("following player");
    }

    public override void OnStateTransistionCheck()
    {
        if (CanAttack())
        {
            _stateMachine.ChangeState(_stateMachine.AttackState);
        }
        else if (ShouldIdle())
        {
            _stateMachine.ChangeState(_stateMachine.IdleState);
        }
    }


    bool CanAttack()
    {

        if (_stateMachine.AttackDelayEndTime > Time.time) return false; 
      

        var distance = Vector3.Distance(_stateMachine.Player.position, _stateMachine.transform.position);
        if (distance <= _stateMachine.AttackRadius)//should follow player?
        {
            return true;
        }
        return false;
    }

    bool ShouldIdle()
    {
        var distance = Vector3.Distance(_stateMachine.transform.position, _stateMachine.SpawnPosition);
        if (distance <= 1)//should follow player?
        {
            return true;
        }
        return false;
    }
}
