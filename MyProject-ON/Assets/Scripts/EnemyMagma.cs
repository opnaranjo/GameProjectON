using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMagma : Enemy
{
    public float maxDistance = 25f;
    public GameObject ligthVision;

    /*
    private void Update()
    {
        if (!gameManager.isDead)
        {
            //LookAtPlayerLerp(gameObject);
            LightDamage();

        }

    }
    */

    protected override void Introduction()
    {
        base.Introduction();
        Debug.Log("Hi This is Mr. EnemyMagma!");
    }

    protected override void Move()
    {
        base.Move();
    }

    protected override void Attack()
    {
        base.Attack();
        LightDamage();
    }


    public void LightDamage()
    {


        float dist = Vector3.Distance(player.transform.position, ligthVision.transform.position);

        if (dist <= maxDistance)
        {
            ligthVision.SetActive(true);
            RaycastHit hit;
            if (Physics.Linecast(ligthVision.transform.position, player.transform.transform.position, out hit))
            {
                if (hit.transform.gameObject.CompareTag("Player"))
                {
                    gameManager.DamagePlayer(damage);

                }
            }
        }
        else
        {
            ligthVision.SetActive(false);

        }


    }
}