using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, IDamagable
{
   [field:SerializeField] public float maxHealth { get; set; }
   [field: SerializeField] public float currHealth { get; set; }

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
