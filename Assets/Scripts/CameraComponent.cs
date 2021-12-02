using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraComponent : MonoBehaviour
{
    [SerializeField] private float sensitivity;
    [SerializeField] private Vector3 PlayerOffset;
    [SerializeField] private Transform TrackedPlayer;
    [SerializeField] private float MaxDistance = 10;
    [SerializeField] private float MoveSpeed = 20;
    [SerializeField] private float UpdateSpeed = 10;
    [SerializeField] private float CurrentDistance = 5;
    [SerializeField] private Transform BoomArm;
    [SerializeField] private float RotateSpeed;
    [SerializeField] private Transform Dumby;


    private bool _isLockedOn = false;
    private Transform _LockedOnTarget;
    private Vector3 _desiredDirection;

    // save the original settings(assuming model is the model transform)
    private Quaternion _localRotation;
    private Vector3 _localPosition;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Start()
    {
        transform.position = BoomArm.position + PlayerOffset;

        // LockOnTarget(Dumby);
        _localPosition = transform.localPosition;
        _localRotation = transform.localRotation;
    }

    private void Update()
    {
       // BoomArm.position = TrackedPlayer.position;
        TrackCamera();
        if (_isLockedOn)
        {
            LockedOnUpdate();
            return;
        }
        UpdateCamera();


       
    }

    private void UpdateCamera()
    {


        float rotateHorizontal = Input.GetAxis("Mouse X");
        float rotateVertical = Input.GetAxis("Mouse Y") * - 1;
        //Debug.Log(rotateHorizontal);
        //transform.RotateAround(BoomArm.position, Vector3.up, rotateHorizontal * sensitivity);
        BoomArm.Rotate(Vector3.up * rotateHorizontal * sensitivity); //instead if you dont want the camera to rotate around the player
         transform.RotateAround(BoomArm.position, transform.right, rotateVertical * sensitivity);
        //transform.Rotate(Vector3.right * rotateVertical * sensitivity); // if you don't want the camera to rotate around the player

    }

    private void TrackCamera()
    {
        //CurrentDistance = Mathf.Clamp(CurrentDistance, 0, MaxDistance);
        //transform.position = Vector3.MoveTowards(transform.position, TrackedPlayer.position + Vector3.up * CurrentDistance - TrackedPlayer.forward * (CurrentDistance + MaxDistance * 0.5f), UpdateSpeed * Time.deltaTime);
        // transform.position = Vector3.MoveTowards(transform.position, TrackedPlayer.position + Vector3.up * CurrentDistance - Vector3.forward * (CurrentDistance + MaxDistance * 0.5f), UpdateSpeed * Time.deltaTime);
        //transform.LookAt(TrackedPlayer.position);

        //BoomArm.position = Vector3.MoveTowards(BoomArm.position, TrackedPlayer.position, UpdateSpeed * Time.deltaTime);
        BoomArm.position = Vector3.Lerp(BoomArm.position, TrackedPlayer.position, UpdateSpeed * Time.deltaTime);

        
    }

    public void LockOnTarget(Transform target)
    {
        _isLockedOn = true;
        _LockedOnTarget = target;
    }

    public void ResetLockOn()
    {
        _isLockedOn = false;
        _LockedOnTarget = null;
    }

    private void LockedOnUpdate()
    {
        //Transform NormalizedTarget = _LockedOnTarget;
        //NormalizedTarget.position = new Vector3(_LockedOnTarget.position.x, BoomArm.position.y, _LockedOnTarget.position.z);
        // BoomArm.LookAt(NormalizedTarget);

        
        Vector3 Temp = new Vector3(_LockedOnTarget.position.x, BoomArm.position.y, _LockedOnTarget.position.z);

        // Determine which direction to rotate towards
        Vector3 targetDirection = Temp - BoomArm.position;

        // The step size is equal to speed times frame time.
        float singleStep = RotateSpeed * Time.deltaTime;

        // Rotate the forward vector towards the target direction by one step
        Vector3 newDirection = Vector3.RotateTowards(BoomArm.forward, targetDirection, singleStep, 0.0f);

        // Draw a ray pointing at our target in
        // Debug.DrawRay(transform.position, newDirection, Color.red);

        // Calculate a rotation a step closer to the target and applies rotation to this object
        BoomArm.rotation = Quaternion.LookRotation(newDirection);



        // do the movement/rotation as you're already doing
        // then restore them using Lerp (assuming this is in Update):
        float dt = (float)((float)Time.deltaTime * 2.3); // 2 to 5 give reasonable results
        Debug.Log(dt);

        if (dt >= 0.95)
        {
            Debug.Log("done");
        }
        transform.localRotation = Quaternion.Slerp(transform.localRotation, _localRotation, dt);
        transform.localPosition = Vector3.Lerp(transform.localPosition, _localPosition, dt);
    }
}
