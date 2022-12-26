using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkull : Enemy
{
    //public float maxDistance;

    //public GameObject ligthVision;

    /*
    private void Start()
    {
        Introduction();
    }

    private void Update()
    {
        if (!gameManager.isDead)
        {
            //LookAtPlayerLerp(gameObject);
            Introduction();
        }
    }

    */

    protected override void Introduction()
    {
        //base.Introduction();
        Debug.Log("Hi This is Mr. Enemy Skull!");
    }

    protected override void Move()
    {
        base.Move();
    }

    protected override void Attack()
    {
        //base.Attack();
        Debug.Log("Enemy Skull Attcking!");
    }

}