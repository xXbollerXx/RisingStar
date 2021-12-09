using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIState
{
    protected AIStateMachine _stateMachine;

    public AIState(AIStateMachine stateMachine)
    {
        _stateMachine = stateMachine;

    }

    public virtual void OnStateEnter()
    {     
    }
    public abstract void OnStateTick();
    public abstract void OnStateExit();
    public abstract void OnStateTransistionCheck();

}
