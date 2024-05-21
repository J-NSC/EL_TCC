using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stick : MonoBehaviour
{
    float horizontalInput;
    public int maxPower = 3;
    
    [SerializeField] GameObject stick; 
    [SerializeField] PowerBar powerBar;
    
    [SerializeField] Transform cueBall;
    [SerializeField] float rotationSpeed = 3f; 
    [SerializeField] Vector3 offset;
    [SerializeField] float downAngle;
    [SerializeField] float power;
    [SerializeField] MeshRenderer cueStick;
    [SerializeField] float speed = 2.0f;
    [SerializeField] LayerMask layerMask ;

    [SerializeField] bool IsEnableStick = true;
    LineRenderer trajectory;

    public delegate void BallAppliedForceHandle();
    public static event BallAppliedForceHandle BallAppliedForce;

    public delegate void TrajectoryDrawHandle(Vector3 direction);
    public static event TrajectoryDrawHandle trajectoryLine;

    void Awake()
    {
        trajectory = GetComponent<LineRenderer>();
    }

    void Start()
    {
        OnResetStick();
        powerBar.SetMaxPowerBar(maxPower);
        
    }

    void Update()
    {
        
        if (IsEnableStick)
        {
            horizontalInput =  Input.GetAxis("Horizontal") * rotationSpeed * Time.deltaTime;
            transform.RotateAround(cueBall.position, Vector3.up , horizontalInput);
        }
    
        Shoot();
    }

    void LateUpdate()
    {
        Vector3? direction = RayTrajectory();
        
        if (!direction.HasValue)
            return;
        
        OnRenderLine(direction.Value);
    }

    public void OnResetStick()
    {
        cueBall =  GameObject.FindGameObjectWithTag("Ball").transform;
        cueStick.enabled = true;
        transform.position = cueBall.position + offset;
        transform.LookAt(cueBall.position);
        transform.localEulerAngles = new Vector3(downAngle, transform.localEulerAngles.y, 0);
    }

    public void Shoot()
    {
        if (Input.GetButton("Jump"))
        {
            powerBar.ResetPowerBar();
            power = Mathf.PingPong(Time.time * speed, maxPower);
            powerBar.SetPowerBar(power);
        }
        
        if (Input.GetButtonUp("Jump"))
        {
            
            Vector3 hitDirection = transform.up;
            hitDirection = new Vector3(hitDirection.x, 0, hitDirection.z).normalized;
            cueBall.gameObject.GetComponent<Rigidbody>().AddForce(hitDirection * power , ForceMode.Impulse);
            Invoke("ChamarBallAppliedForce", .3f);
            power = 0f;
            OnEnabledStick(false);
            powerBar.ResetPowerBar();
        }
    }
    
    void ChamarBallAppliedForce()
    {
        BallAppliedForce?.Invoke();
    }

    Vector3? RayTrajectory()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.up, out hit, float.PositiveInfinity, ~layerMask))
        {
            // GameObject colliderName = hit.collider.gameObject;
            return hit.point;
        }
        return null;
    }
    
    public void OnRenderLine(Vector3 endPosition)
    {
        trajectory.SetPosition(0 ,cueBall.position);
        trajectory.SetPosition(1, endPosition);
    }

    public void OnEnabledStick(bool IsEnableStick)
    {
        stick.SetActive(IsEnableStick);
    }
    
    
  
}
