using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float enemyDamage = 0.1f;

    public virtual float AttackEnemy()
    {
        return enemyDamage;
    }
   
}
