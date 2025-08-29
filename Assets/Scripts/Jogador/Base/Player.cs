using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    #region Health Variables
    [field:SerializeField] public float maxHealth { get; set; }
    [HideInInspector] public float currHealth { get; set; }

    #endregion

    #region State Machine Variables
    private PlayerStateMachine stateMachine { get; set; }
    [HideInInspector] public IdleState idleState { get; set; }
    [HideInInspector] public MoveState moveState { get; set; }
    [HideInInspector] public bool isGrounded=true;

    #endregion

    #region Player Components
    [HideInInspector] public Rigidbody2D rb { get; set; }
    [HideInInspector] public Animator animator { get; set; }
    #endregion

    #region MoveState Variables
    [field: SerializeField] public float moveSpeed { get; set; }
    #endregion

    void Awake()
    {
        stateMachine = new PlayerStateMachine();

        rb = GetComponent<Rigidbody2D>();
        idleState = new IdleState(this, stateMachine);
        moveState = new MoveState(this, stateMachine);

        animator = GetComponent<Animator>();
    }

    void Start()
    {
        currHealth = maxHealth;
        stateMachine.Start(idleState);
    }

    #region Health/Die
    public void Damage(int damage)
    {
        if(currHealth-damage <= 0)
        {
            currHealth = 0;
            Die();
            return;
        }
       currHealth-=damage;
    }

    public void Die()
    {
        Destroy(this);
    }

    #endregion

    private void Update()
    {
        stateMachine.FrameUpdate();
    }

    private void FixedUpdate()
    {
        stateMachine.PhysicsUpdate();
    }
}
