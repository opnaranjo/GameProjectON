using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMagma : Enemy
{
    protected override void Introduction()
    {
        //base.Introduction();
        Debug.Log("Hi This is Mr. EnemyMagma!");
    }

    protected override void Move()
    {
        base.Move();
    }

    protected override void Attack()
    {

    }

}