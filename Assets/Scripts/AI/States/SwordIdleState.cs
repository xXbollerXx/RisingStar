using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordIdleState : IdleState
{
    public override void OnStateTransistionCheck()
    {
        var Temp = (SwordStateMachine)_stateMachine;
        var distance = Vector3.Distance(_stateMachine.Player.position, transform.position);

        if (Temp._canLunge && distance < Temp.LungeAttackRadius )
        {
            Temp._canLunge = false;
            _stateMachine.ChangeState(Temp.LungeState);
            return;
        }

        base.OnStateTransistionCheck();
    }
}
