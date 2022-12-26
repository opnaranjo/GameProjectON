using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;
using UnityEngine.AI;
//using GameManager;

public class EnemyInteraction : MonoBehaviour
{
    //public PlayerInteraction playerInteraction;
    
    private enum EnemyType
    {
        EnemyOne,
        EnemyTwo,
    };

    [SerializeField] private EnemyType tipoEnemigo;

    public Transform posPlayer;

    
    //public float speedMov = 3f;
    

    public float enemyDamage = 0.1f;
    //public float raycastRange = 60f;


    //private bool flagMovEnemy = false;
    private bool flagLookEnemy = false;
    private bool flagLigthDamage = false;
    
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        SetEnemyBehaviour(tipoEnemigo);
        Debug.Log("Attack enemy: " + enemyDamage);
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.isDead)
        {
            if (flagLookEnemy)
            {
                //LookAtPlayerLerp(this.gameObject);
                //LookAtPlayerLerp(gameObject);
            }

            if (flagLigthDamage)
            {
                //LightDamage();
            }
            
        }

    }

    void SetEnemyBehaviour(EnemyType tipoEnemigo)
    {
        switch (tipoEnemigo)
        {

            case EnemyType.EnemyOne:
                flagLookEnemy = true;
                flagLigthDamage = true;
                break;
            case EnemyType.EnemyTwo:
                flagLookEnemy = false;
                break;
            default:
                flagLookEnemy = false;
                break;
        }
    }

    

    

}
