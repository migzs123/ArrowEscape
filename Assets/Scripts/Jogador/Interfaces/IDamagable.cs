using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamagable
{
    void Damage(int  damage);

    void Die();

    float maxHealth {  get; set; }
    float currHealth { get; set; }

}
