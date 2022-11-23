using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMagma : Enemy
{
    

    public override float AttackEnemy()
    {
        return base.enemyDamage * 2;
    }
}
