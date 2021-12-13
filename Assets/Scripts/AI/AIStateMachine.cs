using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum AIState_Deprecated
{
    Idle,
    Follow,
    Return,
    Attack
}
public abstract class AIStateMachine : MonoBehaviour
{
    [Tooltip("green")]
    [SerializeField] public float PerceptionRadius = 1;
    [Tooltip("red")]
    [SerializeField] public float AttackRadius = 1;
    [Tooltip("blue")]
    [SerializeField] public float ReturnRadius = 1;

    
     public AIState IdleState;
    
    public AIState FollowState;
 
    public AIState AttackState;

    public AIState ReturnState;
     


    protected AIState_Deprecated _aIState = AIState_Deprecated.Idle;
    public bool CanChangeState { get; set; } = true;
    public Transform Player { get; private set; }
    public Animator Animator { get; private set; }
    public NavMeshAgent Agent { get; private set; }
    public Vector3 SpawnPosition { get; private set; }
    public AIState CurrentState { get; private set; }

    public float AttackDelayEndTime { get; set; } = 0;


    protected virtual void Initialize()
    {
        Animator = GetComponent<Animator>();
        Agent = GetComponent<NavMeshAgent>();
        Player = GameplayStatics.GetPlayer();
        SpawnPosition = transform.position;

       // IdleState = GetComponent<IdleState>();
      //  FollowState = GetComponent<FollowState>();
       // AttackState = GetComponent<AttackState>();
       // ReturnState = GetComponent<ReturnState>();

        ChangeState(IdleState);

    }


    private void Start()
    {
        Initialize();
    }
    protected virtual void Update()
    {
        
        if (!Player) return;
        if (!Agent) return;
        if (!Animator) return;    
        CurrentState.OnStateTick();
        CurrentState.OnStateTransistionCheck();
        //TransitionCheck();

    }

    public void ChangeState(AIState NewState)
    {
        if (CurrentState != null)
        {
            CurrentState.OnStateExit();
        }

        CurrentState = NewState;
        CurrentState.OnStateEnter(this);
    }
    
    protected virtual void TransitionCheck()
    {
        if (!CanChangeState) return;

        var distance = Vector3.Distance(Player.position, transform.position);
        if (distance <= PerceptionRadius)//should follow player?
        {
            ChangeState(FollowState);
        }

        if (distance <= AttackRadius) // in attacking disntace?
        {
            _aIState = AIState_Deprecated.Attack;
            ChangeState(AttackState);
        }

        distance = Vector3.Distance(transform.position, SpawnPosition);
        if (distance <= 1) //has agent gone back to spawn location?
        {
            _aIState = AIState_Deprecated.Return;
            ChangeState(IdleState);
        }

        distance = Vector3.Distance(Player.position, SpawnPosition);
        if ( distance >= ReturnRadius) //has player gone out of range and should return?
        {
            _aIState = AIState_Deprecated.Return;
            ChangeState(ReturnState);
        }
    }



    protected virtual void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, AttackRadius);

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, PerceptionRadius);

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, ReturnRadius);
    }
}
