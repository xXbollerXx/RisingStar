using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordLungeState : AttackState
{
    const string AttackAnimName = "SwordEnemy_Lunge";
    [SerializeField] public float LungeSpeed = 1;


    private Vector3 _direction;
    private bool _isLunging = false;

    public override void OnStateEnter(AIStateMachine stateMachine)
    {
        base.OnStateEnter(stateMachine);
        _stateMachine.Animator.Play(AttackAnimName);

    }

    public override void OnStateTick()
    {
        base.OnStateTick();



        if (_isLunging)
        {
            _stateMachine.Agent.Move(_direction * LungeSpeed * Time.deltaTime);
        }

        if (!_LockMovement)
        {

            _stateMachine.Agent.SetDestination(_stateMachine.Player.position);
            _stateMachine.transform.LookAt(_stateMachine.Player);
        }
    }

    public void OnStartLunge()
    {
        _direction = transform.forward;
        _isLunging = true;
    }

    public void OnStopLunge()
    {
        _isLunging = false;
    }

}
