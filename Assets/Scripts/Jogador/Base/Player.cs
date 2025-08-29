using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
    #region Health Variables
    [field:SerializeField] public float maxHealth { get; set; }
    [field: SerializeField] public float currHealth { get; set; }

    #endregion

    #region State Machine Variables
    private PlayerStateMachine stateMachine { get; set; }

    #endregion

    void Awake()
    {
        stateMachine = new PlayerStateMachine();
    }

    void Start()
    {
        currHealth = maxHealth;
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

    void Update()
    {
        
    }
}
