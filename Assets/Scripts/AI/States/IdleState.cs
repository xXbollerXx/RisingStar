using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : AIState
{

    public override void OnStateExit()
    {
   
    }

    public override void OnStateTick()
    {
        Debug.Log("Idle");
    }

    public override void OnStateTransistionCheck()
    {
        if (CanFollow())
        {
            _stateMachine.ChangeState(_stateMachine.FollowState);
        }
    }

    bool CanFollow()
    {
        var distance = Vector3.Distance(_stateMachine.Player.position, _stateMachine.transform.position);
        if (distance <= _stateMachine.PerceptionRadius)//should follow player?
        {
            return true;
        }
        return false;
    }
}
