using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public delegate void  ChangedAnimationHandle(string state);
    public static event ChangedAnimationHandle changedAnimation;
    
    public Rigidbody2D rig;
    public Idle idle;
    public Jumper jumper;
    public Runnig runnig;
    public StateMachine stateMachine;
    
    public float dir;
    public float speed = 3f;
    
    public Vector3 origin;
    public Vector2 rayDirection = Vector2.down;
    public float maxDistance;

    public float jumpForce;
    public bool isGrounded;
    public bool hasJumpInput;

    SpriteRenderer cucaSprite;

    void Awake()
    {
        rig = GetComponent<Rigidbody2D>();
        stateMachine = new StateMachine();
        cucaSprite = GetComponentInChildren<SpriteRenderer>();
        
        idle = new Idle(this);
        runnig = new Runnig(this);
        jumper = new Jumper(this);
        stateMachine.ChangeState(idle);

    }

    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        inputCheck();
        changedAnimation?.Invoke(stateMachine.currentStateName);
        origin = transform.position;
        maxDistance = 1;
        DetectGround();
        Flip();
        stateMachine.Update();
    }


    void FixedUpdate()
    {
        stateMachine.FixedUpdate();
    }

    void inputCheck()
    {
        dir = Input.GetAxisRaw("Horizontal");
        hasJumpInput = Input.GetButtonDown("Jump");
        
    }

    void DetectGround()
    {
        isGrounded = false;
        
        origin = transform.position;
        rayDirection = Vector2.down;
        maxDistance = 1f;

        LayerMask groundLayer = GameManager.inst.GroundLayer;

        if (Physics2D.Raycast(origin, rayDirection, maxDistance, groundLayer))
        {
            isGrounded = true;
        }
    }

    void Flip()
    {
        if (dir != 0) 
        {
            cucaSprite.flipX = dir < 0; 
        }
    }



    void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawRay(origin, rayDirection);
    }
}
