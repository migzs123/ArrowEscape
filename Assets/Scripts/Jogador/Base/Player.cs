using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IDamagable
{
    #region Health Variables
    [Header("Health Variables")]
    public float maxHealth;
    public HeartUI hearts;
    [HideInInspector] public float currHealth { get; set; }

    #endregion

    #region State Machine Variables
    private PlayerStateMachine stateMachine { get; set; }
    [HideInInspector] public IdleState idleState { get; set; }
    [HideInInspector] public MoveState moveState { get; set; }
    [HideInInspector] public FallState fallState { get; set; }
    [HideInInspector] public JumpState jumpState { get; set; }
    [HideInInspector] public bool isGrounded= false;

    #endregion

    #region Player Components
    [HideInInspector] public Rigidbody2D rb { get; set; }
    [HideInInspector] public Animator animator { get; set; }
    #endregion

    #region IdleState Variables
    [Header("Idle State")]
    public float deceleration;
    #endregion

    #region MoveState Variables
    [Header("Move State")]
    public float moveSpeed =7f;
    public float accel = 30f;
    public float decel = 50f;
    #endregion

    #region JumpState Variables
    [Header("Jump State")]
    public float jumpPower;
    public float lowJumpMult;
    public float coyoteTime = 0.1f;
    [HideInInspector] public float coyoteTimeCounter;
    public float jumpBufferTime = 0.1f;
    [HideInInspector] public float jumpBufferCounter;
    #endregion

    #region FallState Variables
    [Header("Fall State")]
    public float fallMult;
    public float airAccel = 8f;
    public float airDecel = 6f;
    #endregion

    void Awake()
    {
        stateMachine = new PlayerStateMachine();

        rb = GetComponent<Rigidbody2D>();
        idleState = new IdleState(this, stateMachine);
        moveState = new MoveState(this, stateMachine);
        fallState = new FallState(this, stateMachine);
        jumpState = new JumpState(this, stateMachine);

        animator = GetComponent<Animator>();
    }

    void Start()
    {
        if (GameManager.instance !=null && GameManager.instance.playerHealth >= 0)
        {
            // vida persistente entre fases
            currHealth = GameManager.instance.playerHealth;
        }
        else
        {
            currHealth = maxHealth;
        }

        // salva a vida no início da fase
       if(GameManager.instance != null) GameManager.instance.playerHealthAtLevelStart = currHealth;
        hearts.UpdateHearts();

        stateMachine.Start(idleState);
    }

    #region Health/Die

    public void OnDeathAnimationEnd()
    {
        //Destroy(gameObject);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Damage(int damage)
    {
        if(currHealth-damage <= 0)
        {
            currHealth = 0;
            Die();
            return;
        }
       currHealth-=damage;
        //Debug.Log(currHealth);
       hearts.UpdateHearts();
       animator.SetTrigger("Damage");
    }

    public void Cure(int amount)
    {
        currHealth += amount;

        if (currHealth > maxHealth)
        {
            currHealth = maxHealth;
        }

        hearts.UpdateHearts();
    }

    public void Die()
    {
        animator.SetTrigger("Die");
        rb.simulated = false;
        currHealth = 0;
        if(GameManager.instance != null) currHealth = GameManager.instance.playerHealthAtLevelStart;
        hearts.UpdateHearts();
    }

    private void OnDestroy()
    {
        if (GameManager.instance != null)
        {
            GameManager.instance.playerHealth = currHealth;
        }
    }

    #endregion

    private void Update()
    {
        if (isGrounded)
            coyoteTimeCounter = coyoteTime;
        else
            coyoteTimeCounter -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space))
            jumpBufferCounter = jumpBufferTime;
        else
            jumpBufferCounter -= Time.deltaTime;

        stateMachine.FrameUpdate();
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }
}
