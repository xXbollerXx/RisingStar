using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnState : AIState
{
    public ReturnState(AIStateMachine stateMachine) : base(stateMachine)
    {

    }
    public override void OnStateExit()
    {
       
    }

    public override void OnStateTick()
    {
        _stateMachine.Agent.SetDestination(_stateMachine.SpawnPosition);
        _stateMachine.transform.LookAt(_stateMachine.Player);
        Debug.Log("Returning to spawn location");
    }

    public override void OnStateTransistionCheck()
    {
       
    }
}
