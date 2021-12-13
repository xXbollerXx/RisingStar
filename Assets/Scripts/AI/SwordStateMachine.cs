using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordStateMachine : AIStateMachine
{
    [SerializeField] public float LungeResetRadius = 1;
    [SerializeField] public float LungeAttackRadius = 1;

    [HideInInspector]
    public bool _canLunge = true;

    public AIState LungeState;

    protected override void Initialize()
    {
        base.Initialize();
       // LungeState = GetComponent<SwordLungeState>();
    }

    protected override void Update()
    {
        base.Update();
        var distance = Vector3.Distance(Player.position, transform.position);

        if (distance > LungeResetRadius)
        {
            _canLunge = true;
        }
        
    }

    protected override void OnDrawGizmosSelected()
    {
        base.OnDrawGizmosSelected();
        // Display the explosion radius when selected
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, LungeResetRadius);

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, LungeAttackRadius);
    }
}
