using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    void TakeDamage(int  damage);

    void Die();

    float currHealth { get; set; }

}
