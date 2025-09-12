using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour, IDamagable
{
    #region Health Variables
    [Header("Health Variables")]
    [field: SerializeField] public float maxHealth;
    [HideInInspector] public float currHealth { get; set; }

    #endregion

    #region State Machine Variables
    private PlayerStateMachine stateMachine { get; set; }
    [HideInInspector] public IdleState idleState { get; set; }
    [HideInInspector] public MoveState moveState { get; set; }
    [HideInInspector] public FallState fallState { get; set; }
    [HideInInspector] public JumpState jumpState { get; set; }
    [HideInInspector] public bool isGrounded=true;

    #endregion

    #region Player Components
    [HideInInspector] public Rigidbody2D rb { get; set; }
    [HideInInspector] public Animator animator { get; set; }
    #endregion

    #region MoveState Variables
    [Header("Move State")]
    [field: SerializeField] public float moveSpeed;
    #endregion

    #region JumpState Variables
    [Header("Jump State")]
    [field: SerializeField] public float jumpPower;
    [field: SerializeField] public float lowJumpMult;
    #endregion

    #region FallState Variables
    [Header("Fall State")]
    [field: SerializeField] public float fallMult;
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
        currHealth = maxHealth;
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
       Debug.Log(currHealth);
       animator.SetTrigger("Damage");
    }

    public void Die()
    {
        animator.SetTrigger("Die");
        rb.simulated = false;
    }

    #endregion

    private void Update()
    {
        stateMachine.FrameUpdate();
        //Debug.Log(isGrounded);
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }
}
