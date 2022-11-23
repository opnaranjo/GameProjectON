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
    public PlayerInteraction playerInteraction;
    
    private enum EnemyType
    {
        EnemyOne,
        EnemyTwo,
    };

    [SerializeField] private EnemyType tipoEnemigo;

    public Transform posPlayer;
    public GameObject ligthVision;
    public float enemyDamage = 0.01f;
    //public float speedMov = 3f;
    public float speedLook = 3;
    public float maxDistance = 25f;
    //public float raycastRange = 60f;


    //private bool flagMovEnemy = false;
    private bool flagLookEnemy = false;
    private bool flagLigthDamage = false;
    private GameManager gameManager;
    private EnemyMagma enemyMagma;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        SetEnemyBehaviour(tipoEnemigo);
    }

    // Update is called once per frame
    void Update()
    {
        if (!gameManager.isDead)
        {
            if (flagLookEnemy)
            {
                LookAtPlayerLerp(this.gameObject);
            }

            if (flagLigthDamage)
            {
                LightDamage();
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
                enemyDamage = enemyMagma.AttackEnemy();
                //Debug.Log("Attack: " + enemyMagma.AttackEnemy());
                break;
            case EnemyType.EnemyTwo:
                flagLookEnemy = false;
                break;
            default:
                flagLookEnemy = false;
                break;
        }
    }

    void LightDamage()
    {
        

        float dist = Vector3.Distance(posPlayer.position, ligthVision.transform.position);

        if (dist <= maxDistance)
        {
            ligthVision.SetActive(true);
            RaycastHit hit;
            Debug.Log("LightDamage - raycast ");
            //if (Physics.Raycast(ligthVision.transform.position, ligthVision.transform.forward, out hit, raycastRange))
            //if (Physics.Raycast(transform.position, transform.forward, out hit, range))
            if(Physics.Linecast(ligthVision.transform.position, posPlayer.transform.position, out hit))
            {
                //Debug.Log("Entra - Raycast: ");
                Debug.Log("LightDamage - Raycast: " + hit.transform.gameObject);
                if (hit.transform.gameObject.tag == "Player")
                {
                    playerInteraction.DamagePlayer(enemyDamage);

                }
            }
        }
        else
        {
            ligthVision.SetActive(false);

        }

       
    }

    void LookAtPlayerLerp(GameObject gameObject)
    {
        Quaternion nuevaRotacion = Quaternion.LookRotation(posPlayer.position - gameObject.transform.position);
        gameObject.transform.rotation = Quaternion.Lerp(gameObject.transform.rotation, nuevaRotacion, speedLook * Time.deltaTime);
    }

}
