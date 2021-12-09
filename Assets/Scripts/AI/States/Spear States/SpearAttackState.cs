using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpearAttackState : AttackState
{
    SpearStateMachine _spearStateMachine;
    private Vector3 _chargeDirection;
    private bool _isCharging = false;
    private bool _isCoolingDown = false;
    private float _EndTimeCache;

    public SpearAttackState(AIStateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void OnStateEnter()
    {
        _spearStateMachine = (SpearStateMachine)_stateMachine;

        _spearStateMachine.CanChangeState = false;
        _chargeDirection = (_spearStateMachine.Player.position - _stateMachine.transform.position).normalized;
        _EndTimeCache = Time.time + _spearStateMachine.ChargeDuration;
        _spearStateMachine.Agent.isStopped = true;
    }

    public override void OnStateTick()
    {
        base.OnStateTick();

        if (!_isCoolingDown)
        {
            if (_EndTimeCache > Time.time)
            {
                _spearStateMachine.Agent.Move(_chargeDirection * _spearStateMachine.ChargeSpeed * Time.deltaTime);
                _spearStateMachine.transform.rotation = Quaternion.LookRotation(_chargeDirection);
                // look in direction 
            }
            else
            {
                _spearStateMachine.AttackDelayEndTime = Time.time + _spearStateMachine.ChargeCoolDown;
                _isCoolingDown = true;
            }
        }
        else
        {
           
            // _spearStateMachine.CanChangeState = true;
            _isAttackComplete = true;
            
        }

    }

    public override void OnStateExit()
    {
        base.OnStateExit();
        _isCharging = false;
        _isCoolingDown = false;
        _spearStateMachine.Agent.isStopped = false;

    }

}
