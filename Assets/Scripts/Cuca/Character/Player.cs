using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] public CharacterStatisticSO characteSO;
    
    public static bool playerInstatiated = false;

    
    public delegate void  ChangedAnimationHandle(string state, float speed, bool hasJumpInput);
    public static event ChangedAnimationHandle changedAnimation;

    public delegate void OpenedDoorHandle(bool isOpen);
    public static event OpenedDoorHandle openDoor;
    
    
    public delegate Vector3 LoadedPositionHandle();
    public static event LoadedPositionHandle loadPosition;

    public delegate void SetPositionPlayerHandle(Vector3 pos);
    public static event SetPositionPlayerHandle setPosition;
    
    public delegate void GameOverHandle();
    public static event GameOverHandle gameOver;
    
    public Rigidbody2D rig;
    public Idle idle;
    public Jumping jumping;
    public Runnig runnig;
    public Stagger stagger;
    public StateMachine stateMachine;

    public FloatValue currentHealth;
    public Signal playerHealthSignal;
    
    [SerializeField] Vector2 sizeBox;
    [SerializeField] GameObject feetPosition; 
    [SerializeField] GameObject spwanPosition;
    public SpriteRenderer cucaSprite;

    
    public float dir;
    public float speed = 3f;
    
    public Vector3 origin;
    public Vector2 rayDirection = Vector2.down;
    public float maxDistance;

    public float jumpForce;
    public bool isGrounded;
    public bool hasJumpInput;

    bool alterMaxJumper = false;


    [SerializeField] bool isDoor = false;

    bool isMovement = true; 
    [SerializeField] bool facingRight = true;
    
    void Awake()
    {
        
        if (characteSO.loadLevel == 0)
        {
            setPosition?.Invoke(spwanPosition.transform.position);
            characteSO.loadLevel++;
        }
        
        feetPosition = transform.GetChild(1).gameObject;

        rig = GetComponent<Rigidbody2D>();
        stateMachine = new StateMachine();
        cucaSprite = GetComponentInChildren<SpriteRenderer>();
        
        idle = new Idle(this);
        runnig = new Runnig(this);
        jumping = new Jumping(this);
        stagger = new Stagger(this);
        stateMachine.ChangeState(idle);
    }

    void OnEnable()
    {
        PlayerCollider.playerEnteredToDoor += door =>
        {
            isDoor = door;
        };
        ScenesManager.InstantiededPlayer += OnLoadPositionPlayer;
        

        Sing.enableMovPlayer += OnDisableMoviment;

    }

    void OnDisable()
    {
        ScenesManager.InstantiededPlayer -= OnLoadPositionPlayer;

        Sing.enableMovPlayer -= OnDisableMoviment;

    }



    void OnDisableMoviment(bool actived)
    {
        isMovement = actived;
        rig.velocity = Vector2.zero;
        stateMachine.ChangeState(idle);
    }

    void Start()
    {
        facingRight = true;
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
        
        if(currentHealth.RuntimeValue <= 0 )
        {
            Debug.Log("game Over");
            currentHealth.RuntimeValue = currentHealth.initialValue;
            gameOver?.Invoke();
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
            setPosition?.Invoke(transform.position);
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
    }
    
    void Flip (){
        facingRight = !facingRight;
        transform.localScale = new Vector3(facingRight ? -1 : 1 , 1 , 1 );
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(feetPosition.transform.position, sizeBox);
    }

    public void Knock(float knockTime, float damage)
    {
        
        if (stateMachine.currentStateName != "Stagger")
        {
            stateMachine.ChangeState(stagger);
            currentHealth.RuntimeValue -= damage;
            playerHealthSignal.Raise();
            if (currentHealth.RuntimeValue >= 0)
            {
                StartCoroutine(KnockCO(knockTime));
            }
         
        }
      
    }

    IEnumerator KnockCO( float knockTime)
    {
        if (rig != null)
        {
            yield return new WaitForSeconds(knockTime);
            rig.velocity = Vector2.zero;
        }
    }
    
    void OnLoadPositionPlayer()
    {
       
        if (loadPosition != null)
        {
            if(characteSO.loadLevel != 0){
                foreach (LoadedPositionHandle handle in loadPosition.GetInvocationList())
                {
                    Vector3 aux = handle();
                    transform.position = aux;
                }
            }
        } 
    }
}