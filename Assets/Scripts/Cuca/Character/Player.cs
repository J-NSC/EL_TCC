using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public CharacterStatisticSO characteSO;
    
    public delegate void  ChangedAnimationHandle(string state, float speed, bool hasJumpInput);
    public static event ChangedAnimationHandle changedAnimation;

    public delegate void OpenedDoorHandle(bool isOpen);
    public static event OpenedDoorHandle openDoor;
    
    public Rigidbody2D rig;
    public Idle idle;
    public Jumping jumping;
    public Runnig runnig;
    public StateMachine stateMachine;

    public FloatValue currentHealth;
    public Signal playerHealthSignal;
    
    public float dir;
    public float speed = 3f;
    
    public Vector3 origin;
    public Vector2 rayDirection = Vector2.down;
    public float maxDistance;

    public float jumpForce;
    public bool isGrounded;
    public bool hasJumpInput;

    bool alterMaxJumper = false;

    [SerializeField] Vector2 sizeBox;
    [SerializeField] GameObject feetPosition; 
    

    [SerializeField] GameObject spwanPosition;

    public static bool playerInstatiated = false;
    [SerializeField] bool isDoor = false;

    bool isMovement = true; 
    SpriteRenderer cucaSprite;
    bool facingRight;
    
    void Awake()
    {
        feetPosition = transform.GetChild(1).gameObject;
        DontDestroyOnLoad(gameObject);
        if (playerInstatiated)
        {
            Destroy(gameObject);
            return;
        }
        
        if (characteSO.loadLevel == 0)
        {
            characteSO.SpwanPoint = spwanPosition.transform.position;
            characteSO.loadLevel++;
        }


        playerInstatiated = true;
        
        rig = GetComponent<Rigidbody2D>();
        stateMachine = new StateMachine();
        cucaSprite = GetComponentInChildren<SpriteRenderer>();
        
        idle = new Idle(this);
        runnig = new Runnig(this);
        jumping = new Jumping(this);
        stateMachine.ChangeState(idle);
    }

    void OnEnable()
    {
        PlayerCollider.playerEnteredToDoor += door =>
        {
            isDoor = door;
        };
        ScenesManager.InstantiededPlayer += () =>
        {
            transform.position = characteSO.SpwanPoint;
        };

        Sing.enableMovPlayer += OnDisableMoviment;

    }

    void OnDisableMoviment(bool actived)
    {
        isMovement = actived;
        rig.velocity = Vector2.zero;
        stateMachine.ChangeState(idle);
    }

    void Start()
    {
        characteSO.maxJump = 1;
        characteSO.CountJump = characteSO.maxJump;
    }

    void Update()
    {
        inputCheck();
        changedAnimation?.Invoke(stateMachine.currentStateName, rig.velocity.y, hasJumpInput);

        origin = transform.position;
        maxDistance = 1;
        DetectGround();

        if (hasJumpInput)
        {
            characteSO.CountJump--;
        }

        if (characteSO.powerUps[0].actived && !alterMaxJumper)
        {
            characteSO.maxJump = 2;
            alterMaxJumper = true;
        }
        if (isMovement)
        {
            stateMachine.Update();
        }
        
        if(dir > 0 && !facingRight || dir < 0 && facingRight)
        {
            Flip();
        }
    }

    void FixedUpdate()
    {
        if (isMovement)
            stateMachine.FixedUpdate();
    }

    void inputCheck()
    {
        dir = Input.GetAxisRaw("Horizontal");
        hasJumpInput = Input.GetButtonDown("Jump");
        if (isDoor && (Input.GetKeyDown(KeyCode.E) || Input.GetKeyDown(KeyCode.Return)))
        {
            openDoor?.Invoke(isDoor);
            characteSO.SpwanPoint = transform.position;
        }else if(!isDoor) openDoor?.Invoke(false);
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
            characteSO.CountJump = characteSO.maxJump;
        }

        //
        // isGrounded = Physics2D.OverlapBox(feetPosition.transform.position, sizeBox, 0, groundLayer);
        // if (isGrounded)
        // {
        //     characteSO.CountJump = characteSO.maxJump;
        // }

    
    }

    // void Flip()
    // {
    //     if (dir != 0) 
    //     {
    //         cucaSprite.flipX = dir < 0; 
    //     }
    // }
    
    void Flip (){
        facingRight = !facingRight;
        transform.localScale = new Vector3(facingRight ? -1 : 1 , 1 , 1 );
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(feetPosition.transform.position, sizeBox);
    }

    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.gameObject.CompareTag("Enemy"))
    //     {
    //         currentHealth.RuntimeValue -= 1;
    //         playerHealthSignal.Raise();
    //     }
    // }
}