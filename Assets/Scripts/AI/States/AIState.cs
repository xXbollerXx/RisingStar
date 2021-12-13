using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AIState : MonoBehaviour
{
    protected AIStateMachine _stateMachine;


    public virtual void OnStateEnter(AIStateMachine stateMachine)
    {     
        _stateMachine = stateMachine;
    }
    public abstract void OnStateTick();
    public abstract void OnStateExit();
    public abstract void OnStateTransistionCheck();

}
