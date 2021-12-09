using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private CharacterController Controller; 
    [SerializeField] private float WalkSpeed = 1;
    [SerializeField] private float SprintSpeed = 1;
    [SerializeField] private float GravityValue = 1;
    [SerializeField] private float JumpHeight = 1;
    [SerializeField] private float DodgeCoolDown = 1;
    [SerializeField] private float DodgeSpeed = 1;
    [SerializeField] private float RotateSpeed = 1;
    [SerializeField] private CameraComponent Camera;
    [SerializeField] private float Radius;

    private Vector3 _playerVelocity;
    private bool _groundedPlayer;
    private float _dodgeCoolDownEnd = 0;
    private Vector3 _desiredDirection;
    private bool _isLockedOn = false;
    private Transform _LockedOnTarget;
    private float _CurrentSpeed;

    public bool IsSprinting { get; private set; } = false;

// Start is called before the first frame update
    void Start()
    {
        _CurrentSpeed = WalkSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        _groundedPlayer = Controller.isGrounded;

        UpdateMovement();
        TryToDodge();



        ApplyGravity();
        //TryToJump();

        Controller.Move(_playerVelocity * Time.deltaTime);
        TryToLockOn();

        UpdateMovementOrientation();
    }

    private void TryToLockOn()
    {
        if (Input.GetButtonDown("LockOn"))
        {
            if (_isLockedOn)
            {
                UnlockFromEnemy();

                
            }
            else
            {
                Collider[] hitColliders = Physics.OverlapSphere(transform.position, Radius);
                foreach (var hitCollider in hitColliders)
                {
                    //dot product to lock onto facing enemy
                    //need to tab through enemies? 
                    EnemyBase enemyBase;
                    if (hitCollider.TryGetComponent<EnemyBase>(out enemyBase))
                    {
                        Debug.Log("is enemy");
                        LockOnEnemy(hitCollider.transform);
                        return;
                    }
                }

            }
        }

        //if hit tab to cycle enemies
       
    }

    private void LockOnEnemy(Transform enemy)
    {
        _isLockedOn = true;
        _LockedOnTarget = enemy;
        Camera.LockOnTarget(enemy);
    }

    private void UnlockFromEnemy()
    {
        _isLockedOn = false;
        _LockedOnTarget = null;
        Camera.ResetLockOn();
    }

    private void UpdateMovement()
    {
        CheckWantsToSprint();

        Vector3 moveRight = Camera.transform.right * Input.GetAxisRaw("Horizontal");

        var CrossPro = Vector3.Cross(Camera.transform.right, Vector3.up);

        Vector3 moveForward = CrossPro * Input.GetAxisRaw("Vertical");
        Vector3 moveDelta = moveRight + moveForward;
        moveDelta.Normalize();
        Controller.Move(moveDelta * Time.deltaTime * _CurrentSpeed);


        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            _desiredDirection = moveDelta;
            //Debug.Log(Input.GetAxis("Horizontal"));
        }

        //UpdateMovementOrientation();

    }

    private void UpdateMovementOrientation()
    {
        if (_isLockedOn && !IsSprinting)
        {
            _desiredDirection = (_LockedOnTarget.position - transform.position).normalized;
        }
    

        // The step size is equal to speed times frame time.
        float singleStep = RotateSpeed * Time.deltaTime;
        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(transform.forward, _desiredDirection, singleStep, 0.0f);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        transform.rotation = Quaternion.LookRotation(newDirection);
    }

    private void CheckWantsToSprint()
    {
        if (Input.GetButtonDown("Sprint"))
        {
            _CurrentSpeed = SprintSpeed;
            IsSprinting = true;
        }
        else if(Input.GetButtonUp("Sprint"))
        {
            _CurrentSpeed = WalkSpeed;
            IsSprinting = false;

        }
    }

    private void TryToJump()
    {

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && _groundedPlayer)
        {

            _playerVelocity.y += JumpHeight;
        }
    }

    private void ApplyGravity()
    {
        _playerVelocity.y += GravityValue * Time.deltaTime;
        if (_groundedPlayer && _playerVelocity.y < 0)
        {
            _playerVelocity.y = -1f;
        }
    }

    private void TryToDodge()
    {
        bool canDodge = false;
        if (Time.time >= _dodgeCoolDownEnd)
        {
            canDodge = true;
        }

        

        if (canDodge && Input.GetButtonDown("Jump") )
        {
            _dodgeCoolDownEnd = Time.time + DodgeCoolDown;

            Vector3 moveRight = Camera.transform.right * Input.GetAxisRaw("Horizontal");

            var CrossPro = Vector3.Cross(Camera.transform.right, Vector3.up);

            Vector3 moveForward = CrossPro * Input.GetAxisRaw("Vertical");
            Vector3 moveDelta = moveRight + moveForward;
            moveDelta.Normalize();


            Controller.Move(moveDelta * DodgeSpeed);
            
        }
    }

    void OnDrawGizmosSelected()
    {
        // Display the explosion radius when selected
        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(transform.position, Radius);
    }

}
